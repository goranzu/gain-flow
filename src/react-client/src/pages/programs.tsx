import {
	Button,
	Card,
	CardBody,
	CardHeader,
	Checkbox,
	CheckboxGroup,
	Input,
	Textarea,
} from "@heroui/react"
import { z } from "zod"

import { useAppForm } from "../hooks/create-form.ts"

const schema = z.object({
	name: z.string().min(3, "Name must be at least 3 characters long"),
	description: z.string().optional(),
	programDuration: z.string().min(1, "Program duration is required"),
	timePerWorkout: z.string().optional(),
	programFocus: z
		.array(z.string())
		.min(1, "Please select at least one focus area"),
})

const focusOptions = [
	{ key: "strength", label: "Strength Training" },
	{ key: "hypertrophy", label: "Hypertrophy/Muscle Building" },
	{ key: "powerlifting", label: "Powerlifting" },
	{ key: "bodybuilding", label: "Bodybuilding" },
	{ key: "endurance", label: "Endurance" },
	{ key: "conditioning", label: "Conditioning" },
	{ key: "olympic_lifting", label: "Olympic Lifting" },
	{ key: "functional", label: "Functional Training" },
]

export default function Programs() {
	const defaultValues: z.infer<typeof schema> = {
		name: "",
		programDuration: "",
		programFocus: [] as string[],
		description: undefined as string | undefined,
		timePerWorkout: undefined as string | undefined,
	}

	const form = useAppForm({
		defaultValues,
		validators: {
			onChange: schema,
		},
		onSubmit: async ({ value }) => {
			console.log("Program submitted:", value)
		},
	})

	return (
		<div className="p-8 max-w-4xl mx-auto">
			<div className="flex justify-between items-center mb-6">
				<h1 className="text-3xl font-bold">Create New Program</h1>
			</div>

			<Card>
				<CardHeader>
					<h2 className="text-xl font-semibold">Program Details</h2>
				</CardHeader>
				<CardBody>
					<form
						onSubmit={(e) => {
							e.preventDefault()
							e.stopPropagation()
							form.handleSubmit()
						}}
						className="space-y-12"
					>
						<form.Field name="name">
							{(field) => (
								<Input
									label="Program Name"
									placeholder="Enter program name"
									value={field.state.value}
									onChange={(e) => field.handleChange(e.target.value)}
									onBlur={field.handleBlur}
									errorMessage={field.state.meta.errors.join(", ")}
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
						/>
							)}
						</form.Field>

						<form.Field name="timePerWorkout">
							{(field) => (
								<Input
									label="Time per Workout"
									placeholder="e.g., 60 minutes, 1.5 hours"
									value={field.state.value}
									onChange={(e) => field.handleChange(e.target.value)}
									onBlur={field.handleBlur}
									description="Optional: Estimated time for each workout session"
								/>
							)}
						</form.Field>

						<form.Field name="programFocus">
							{(field) => (
								<div className="space-y-2">
									<label className="text-sm font-medium">
										Program Focus <span className="text-danger">*</span>
									</label>
									<p className="text-xs text-default-500 mb-3">
										Select all focus areas that apply to this program
									</p>
									<CheckboxGroup
										value={field.state.value}
										onValueChange={(value) => field.handleChange(value)}
										onBlur={field.handleBlur}
										isInvalid={field.state.meta.errors.length > 0}
										errorMessage={field.state.meta.errors.join(", ")}
										classNames={{
											wrapper: "grid grid-cols-2 gap-2",
										}}
									>
										{focusOptions.map((option) => (
											<Checkbox key={option.key} value={option.key}>
												{option.label}
											</Checkbox>
										))}
									</CheckboxGroup>
								</div>
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
										isDisabled={!canSubmit}
										isLoading={isSubmitting}
									>
										{isSubmitting ? "Creating Program..." : "Create Program"}
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
