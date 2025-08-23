import { useState } from "react"

import {
	Alert,
	Button,
	Card,
	CardBody,
	CardHeader,
	Input,
	Textarea,
} from "@heroui/react"
import { z } from "zod"

import { useAppForm } from "../hooks/create-form.ts"
import {
	useCreateWorkoutProgram,
	useWorkoutPrograms,
} from "../hooks/use-workout-programs.ts"
import type { CreateWorkoutProgramRequest } from "../types/workout-program.ts"

const schema = z.object({
	name: z.string().min(3, "Name must be at least 3 characters long"),
	description: z.string().optional(),
	programDuration: z.string().min(1, "Program duration is required"),
})

export default function Programs() {
	const [errorMessage, setErrorMessage] = useState<string | null>(null)

	const defaultValues: z.infer<typeof schema> = {
		name: "",
		programDuration: "",
		description: undefined as string | undefined,
	}

	const createWorkoutProgram = useCreateWorkoutProgram()
	const { data: workoutProgramData, isLoading, error } = useWorkoutPrograms()

	const form = useAppForm({
		defaultValues,
		validators: {
			onBlur: schema,
		},
		onSubmit: async ({ value }) => {
			setErrorMessage(null)

			const request: CreateWorkoutProgramRequest = {
				name: value.name,
				description: value.description,
				durationWeeks: parseInt(value.programDuration),
				isPublic: true,
			}

			try {
				await createWorkoutProgram.mutateAsync(request)
			} catch (error) {
				const errorMsg =
					error instanceof Error
						? error.message
						: "An unexpected error occurred"
				setErrorMessage(errorMsg)
				console.error("Failed to create program:", error)
			}
		},
	})

	return (
		<div className="p-8 max-w-4xl mx-auto">
			<div className="flex justify-between items-center mb-6">
				<h1 className="text-3xl font-bold">Workout Programs</h1>
				{isLoading && <div className="text-gray-500">Loading...</div>}

				{workoutProgramData?.items.map((program) => (
					<Card key={program.id}>
						<CardHeader>
							<h2 className="text-xl font-semibold">{program.name}</h2>
						</CardHeader>
						<CardBody>
							<p className="text-sm">{program.description}</p>
							<p className="text-sm">{program.durationWeeks} weeks</p>
						</CardBody>
					</Card>
				))}
			</div>

			<div className="flex justify-between items-center mb-6">
				<h1 className="text-3xl font-bold">Create New Program</h1>
			</div>

			<Card>
				<CardHeader>
					<h2 className="text-xl font-semibold">Program Details</h2>
				</CardHeader>
				<CardBody>
					{errorMessage && (
						<Alert
							color="danger"
							title="Error creating program"
							description={errorMessage}
						/>
					)}

					<form
						onSubmit={(e) => {
							e.preventDefault()
							e.stopPropagation()
							void form.handleSubmit()
						}}
						className="space-y-4"
					>
						<form.Field name="name">
							{(field) => (
								<Input
									label="Program Name"
									placeholder="Enter program name"
									value={field.state.value}
									onChange={(e) => field.handleChange(e.target.value)}
									onBlur={field.handleBlur}
									errorMessage={field.state.meta.errors
										.map((error) => error?.message)
										.join(", ")}
									isInvalid={field.state.meta.errors.length > 0}
									isRequired
								/>
							)}
						</form.Field>

						<form.Field name="description">
							{(field) => (
								<Textarea
									label="Description"
									placeholder="Describe your program goals and approach"
									value={field.state.value}
									onChange={(e) => field.handleChange(e.target.value)}
									onBlur={field.handleBlur}
									maxRows={4}
									errorMessage={field.state.meta.errors
										.map((error) => error?.message)
										.join(", ")}
								/>
							)}
						</form.Field>

						<form.Field name="programDuration">
							{(field) => (
								<Input
									label="Program Duration"
									placeholder="Program duration in weeks"
									type="number"
									value={field.state.value}
									onChange={(e) => field.handleChange(e.target.value)}
									onBlur={field.handleBlur}
									isInvalid={field.state.meta.errors.length > 0}
									isRequired
									errorMessage={field.state.meta.errors
										.map((error) => error?.message)
										.join(", ")}
								/>
							)}
						</form.Field>

						<div className="flex gap-4 pt-4">
							<form.Subscribe
								selector={(state) => [state.canSubmit, state.isSubmitting]}
							>
								{([canSubmit, isSubmitting]) => (
									<Button
										type="submit"
										color="primary"
										isDisabled={!canSubmit || createWorkoutProgram.isPending}
										isLoading={isSubmitting || createWorkoutProgram.isPending}
									>
										{isSubmitting || createWorkoutProgram.isPending
											? "Creating Program..."
											: "Create Program"}
									</Button>
								)}
							</form.Subscribe>
							<Button type="button" variant="flat" onPress={() => form.reset()}>
								Reset Form
							</Button>
						</div>
					</form>
				</CardBody>
			</Card>
		</div>
	)
}
