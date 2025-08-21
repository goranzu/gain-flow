import { useEffect } from "react"
import { useNavigate } from "react-router-dom"

import {
	Button,
	Card,
	CardBody,
	CardHeader,
	Divider,
	Link,
} from "@heroui/react"
import { IconBrandGoogleFilled } from "@tabler/icons-react"
import { z } from "zod"

import { useAuth } from "../contexts/auth-context"
import { useAppForm } from "../hooks/create-form"

const schema = z
	.object({
		email: z.email("Please enter a valid email address"),
		password: z.string().min(6, "Password must be at least 6 characters"),
		confirmPassword: z.string(),
	})
	.refine((data) => data.password === data.confirmPassword, {
		message: "Passwords must match",
		path: ["confirmPassword"],
	})

export default function Register() {
	const navigate = useNavigate()
	const { register, isLoading, isAuthenticated, error, clearError } = useAuth()

	// Redirect if already authenticated
	useEffect(() => {
		if (isAuthenticated) {
			navigate("/")
		}
	}, [isAuthenticated, navigate])

	const form = useAppForm({
		defaultValues: {
			email: "",
			password: "",
			confirmPassword: "",
		},
		validators: {
			onChange: schema,
		},
		onSubmit: async ({ value }) => {
			try {
				await register(value.email, value.password, value.confirmPassword)
				// Navigation will happen automatically via the useEffect above
			} catch (error) {
				// Error is already handled in auth context
				console.error("Registration failed:", error)
			}
		},
	})

	return (
		<div className="min-h-screen flex items-center justify-center py-12 px-4 sm:px-6 lg:px-8">
			<div className="max-w-md w-full space-y-8">
				<div className="text-center">
					<Link href="/" className="text-3xl font-bold">
						GainFlow
					</Link>
					<h2 className="mt-6 text-3xl font-extrabold text-gray-900">
						Join GainFlow
					</h2>
					<p className="mt-2 text-sm text-gray-600">
						Already have an account?{" "}
						<Link href="/login" className="font-medium">
							Sign in here
						</Link>
					</p>
				</div>

				<Card className="shadow-lg">
					<CardHeader className="pb-0">
						<h3 className="text-lg font-medium">Create your account</h3>
					</CardHeader>
					<CardBody className="flex flex-col space-y-6">
						{error && (
							<div className="p-3 rounded-md bg-danger-50 border border-danger-200">
								{error.includes("\n") ? (
									<ul className="text-sm text-danger-700 space-y-1">
										{error.split("\n").map((errorLine, index) => (
											<li key={index}>• {errorLine}</li>
										))}
									</ul>
								) : (
									<p className="text-sm text-danger-700">{error}</p>
								)}
							</div>
						)}
						<form
							onSubmit={(e) => {
								e.preventDefault()
								void form.handleSubmit()
							}}
							className="flex flex-col space-y-6"
						>
							<div className="flex flex-col space-y-4">
								<form.AppField name="email">
									{(field) => (
										<field.Input
											type="email"
											label="Email address"
											placeholder="Enter your email"
											required
											variant="bordered"
											onChange={(e) => {
												if (error) clearError()
												field.handleChange(e.target.value)
											}}
											onBlur={field.handleBlur}
											value={field.state.value}
											classNames={{
												label: "text-gray-700",
												input: "text-gray-900",
											}}
										/>
									)}
								</form.AppField>

								<form.AppField name="password">
									{(field) => (
										<field.Input
											type="password"
											label="Password"
											placeholder="Enter your password"
											required
											variant="bordered"
											onChange={(e) => {
												if (error) clearError()
												field.handleChange(e.target.value)
											}}
											value={field.state.value}
											onBlur={field.handleBlur}
											classNames={{
												label: "text-gray-700",
												input: "text-gray-900",
											}}
										/>
									)}
								</form.AppField>

								<form.AppField name="confirmPassword">
									{(field) => (
										<field.Input
											type="password"
											label="Confirm Password"
											placeholder="Confirm your password"
											required
											variant="bordered"
											onChange={(e) => {
												if (error) clearError()
												field.handleChange(e.target.value)
											}}
											value={field.state.value}
											onBlur={field.handleBlur}
											classNames={{
												label: "text-gray-700",
												input: "text-gray-900",
											}}
										/>
									)}
								</form.AppField>
							</div>

							<form.Subscribe selector={(state) => [state.canSubmit]}>
								{([canSubmit]) => (
									<form.Button
										isDisabled={!canSubmit || isLoading}
										type="submit"
										color="primary"
										size="lg"
										className="w-full font-medium"
										isLoading={isLoading}
									>
										{isLoading ? "Signing up..." : "Sign up"}
									</form.Button>
								)}
							</form.Subscribe>
						</form>

						<Divider className="my-6" />

						<div className="flex flex-col space-y-3">
							<Button
								variant="bordered"
								size="lg"
								className="w-full"
								startContent={<IconBrandGoogleFilled className="w-5 h-5" />}
							>
								Continue with Google
							</Button>
						</div>
					</CardBody>
				</Card>
			</div>
		</div>
	)
}
