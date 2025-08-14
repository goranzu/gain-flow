import { useState } from "react"
import { Link, useNavigate } from "react-router-dom"
import {
	Button,
	Card,
	CardBody,
	CardHeader,
	Input,
	Divider,
	Checkbox,
	Select,
	SelectItem,
} from "@heroui/react"

export default function Register() {
	const navigate = useNavigate()
	const [formData, setFormData] = useState({
		firstName: "",
		lastName: "",
		email: "",
		password: "",
		confirmPassword: "",
		fitnessLevel: "",
		agreeToTerms: false,
		subscribeNewsletter: false,
	})
	const [isLoading, setIsLoading] = useState(false)
	const [errors, setErrors] = useState<Record<string, string>>({})

	const fitnessLevels = [
		{ key: "beginner", label: "Beginner" },
		{ key: "intermediate", label: "Intermediate" },
		{ key: "advanced", label: "Advanced" },
		{ key: "expert", label: "Expert" },
	]

	const validateForm = () => {
		const newErrors: Record<string, string> = {}

		if (formData.password.length < 6) {
			newErrors.password = "Password must be at least 6 characters"
		}

		if (formData.password !== formData.confirmPassword) {
			newErrors.confirmPassword = "Passwords do not match"
		}

		if (!formData.agreeToTerms) {
			newErrors.agreeToTerms = "You must agree to the terms and conditions"
		}

		setErrors(newErrors)
		return Object.keys(newErrors).length === 0
	}

	const handleSubmit = async (e: React.FormEvent) => {
		e.preventDefault()

		if (!validateForm()) {
			return
		}

		setIsLoading(true)

		try {
			// Simulate API call
			await new Promise((resolve) => setTimeout(resolve, 1500))
			console.log("Registration attempt:", formData)

			// In real app, handle registration here
			alert("Registration successful! Welcome to GainFlow!")
			navigate("/")
		} catch (error) {
			console.error("Registration failed:", error)
		} finally {
			setIsLoading(false)
		}
	}

	const handleChange = (field: string, value: string | boolean) => {
		setFormData((prev) => ({
			...prev,
			[field]: value,
		}))

		// Clear error when user starts typing
		if (errors[field]) {
			setErrors((prev) => ({
				...prev,
				[field]: "",
			}))
		}
	}

	return (
		<div className="min-h-screen flex items-center justify-center bg-gray-50 py-12 px-4 sm:px-6 lg:px-8">
			<div className="max-w-md w-full space-y-8">
				<div className="text-center">
					<Link to="/" className="text-3xl font-bold text-blue-600">
						GainFlow
					</Link>
					<h2 className="mt-6 text-3xl font-extrabold text-gray-900">
						Join GainFlow
					</h2>
					<p className="mt-2 text-sm text-gray-600">
						Already have an account?{" "}
						<Link
							to="/login"
							className="font-medium text-blue-600 hover:text-blue-500"
						>
							Sign in here
						</Link>
					</p>
				</div>

				<Card className="shadow-lg">
					<CardHeader className="pb-0">
						<h3 className="text-lg font-medium">Create your account</h3>
					</CardHeader>
					<CardBody className="flex flex-col space-y-6">
						<form onSubmit={handleSubmit} className="flex flex-col space-y-6">
							<div className="grid grid-cols-2 gap-4">
								<Input
									type="text"
									label="First Name"
									placeholder="Enter first name"
									value={formData.firstName}
									onChange={(e) => handleChange("firstName", e.target.value)}
									required
									variant="bordered"
									classNames={{
										label: "text-gray-700",
										input: "text-gray-900",
									}}
								/>

								<Input
									type="text"
									label="Last Name"
									placeholder="Enter last name"
									value={formData.lastName}
									onChange={(e) => handleChange("lastName", e.target.value)}
									required
									variant="bordered"
									classNames={{
										label: "text-gray-700",
										input: "text-gray-900",
									}}
								/>
							</div>

							<Input
								type="email"
								label="Email address"
								placeholder="Enter your email"
								value={formData.email}
								onChange={(e) => handleChange("email", e.target.value)}
								required
								variant="bordered"
								classNames={{
									label: "text-gray-700",
									input: "text-gray-900",
								}}
							/>

							<Select
								label="Fitness Level"
								placeholder="Select your fitness level"
								selectedKeys={
									formData.fitnessLevel ? [formData.fitnessLevel] : []
								}
								onSelectionChange={(keys) => {
									const selectedKey = Array.from(keys)[0] as string
									handleChange("fitnessLevel", selectedKey || "")
								}}
								variant="bordered"
								classNames={{
									label: "text-gray-700",
									value: "text-gray-900",
								}}
							>
								{fitnessLevels.map((level) => (
									<SelectItem key={level.key}>{level.label}</SelectItem>
								))}
							</Select>

							<Input
								type="password"
								label="Password"
								placeholder="Enter your password"
								value={formData.password}
								onChange={(e) => handleChange("password", e.target.value)}
								required
								variant="bordered"
								isInvalid={!!errors.password}
								errorMessage={errors.password}
								classNames={{
									label: "text-gray-700",
									input: "text-gray-900",
								}}
							/>

							<Input
								type="password"
								label="Confirm Password"
								placeholder="Confirm your password"
								value={formData.confirmPassword}
								onChange={(e) =>
									handleChange("confirmPassword", e.target.value)
								}
								required
								variant="bordered"
								isInvalid={!!errors.confirmPassword}
								errorMessage={errors.confirmPassword}
								classNames={{
									label: "text-gray-700",
									input: "text-gray-900",
								}}
							/>

							<div className="flex flex-col space-y-3">
								<Checkbox
									isSelected={formData.agreeToTerms}
									onValueChange={(checked) =>
										handleChange("agreeToTerms", checked)
									}
									size="sm"
									isInvalid={!!errors.agreeToTerms}
								>
									<span className="text-sm text-gray-600">
										I agree to the{" "}
										<Link
											to="/terms"
											className="text-blue-600 hover:text-blue-500"
										>
											Terms and Conditions
										</Link>{" "}
										and{" "}
										<Link
											to="/privacy"
											className="text-blue-600 hover:text-blue-500"
										>
											Privacy Policy
										</Link>
									</span>
								</Checkbox>
								{errors.agreeToTerms && (
									<p className="text-xs text-red-500">{errors.agreeToTerms}</p>
								)}

								<Checkbox
									isSelected={formData.subscribeNewsletter}
									onValueChange={(checked) =>
										handleChange("subscribeNewsletter", checked)
									}
									size="sm"
								>
									<span className="text-sm text-gray-600">
										Subscribe to our newsletter for fitness tips and updates
									</span>
								</Checkbox>
							</div>

							<Button
								type="submit"
								color="primary"
								size="lg"
								className="w-full font-medium"
								isLoading={isLoading}
								disabled={
									!formData.email ||
									!formData.password ||
									!formData.firstName ||
									!formData.lastName
								}
							>
								{isLoading ? "Creating account..." : "Create account"}
							</Button>
						</form>

						<Divider className="my-6" />

						<div className="flex flex-col space-y-3">
							<Button
								variant="bordered"
								size="lg"
								className="w-full"
								startContent={
									<svg className="w-5 h-5" viewBox="0 0 24 24">
										<path
											fill="#4285F4"
											d="M22.56 12.25c0-.78-.07-1.53-.2-2.25H12v4.26h5.92c-.26 1.37-1.04 2.53-2.21 3.31v2.77h3.57c2.08-1.92 3.28-4.74 3.28-8.09z"
										/>
										<path
											fill="#34A853"
											d="M12 23c2.97 0 5.46-.98 7.28-2.66l-3.57-2.77c-.98.66-2.23 1.06-3.71 1.06-2.86 0-5.29-1.93-6.16-4.53H2.18v2.84C3.99 20.53 7.7 23 12 23z"
										/>
										<path
											fill="#FBBC05"
											d="M5.84 14.09c-.22-.66-.35-1.36-.35-2.09s.13-1.43.35-2.09V7.07H2.18C1.43 8.55 1 10.22 1 12s.43 3.45 1.18 4.93l2.85-2.22.81-.62z"
										/>
										<path
											fill="#EA4335"
											d="M12 5.38c1.62 0 3.06.56 4.21 1.64l3.15-3.15C17.45 2.09 14.97 1 12 1 7.7 1 3.99 3.47 2.18 7.07l3.66 2.84c.87-2.6 3.3-4.53 6.16-4.53z"
										/>
									</svg>
								}
							>
								Sign up with Google
							</Button>

							<Button
								variant="bordered"
								size="lg"
								className="w-full"
								startContent={
									<svg className="w-5 h-5" fill="#000000" viewBox="0 0 24 24">
										<path d="M12 0c-6.626 0-12 5.373-12 12 0 5.302 3.438 9.8 8.207 11.387.599.111.793-.261.793-.577v-2.234c-3.338.726-4.033-1.416-4.033-1.416-.546-1.387-1.333-1.756-1.333-1.756-1.089-.745.083-.729.083-.729 1.205.084 1.839 1.237 1.839 1.237 1.07 1.834 2.807 1.304 3.492.997.107-.775.418-1.305.762-1.604-2.665-.305-5.467-1.334-5.467-5.931 0-1.311.469-2.381 1.236-3.221-.124-.303-.535-1.524.117-3.176 0 0 1.008-.322 3.301 1.23.957-.266 1.983-.399 3.003-.404 1.02.005 2.047.138 3.006.404 2.291-1.552 3.297-1.23 3.297-1.23.653 1.653.242 2.874.118 3.176.77.84 1.235 1.911 1.235 3.221 0 4.609-2.807 5.624-5.479 5.921.43.372.823 1.102.823 2.222v3.293c0 .319.192.694.801.576 4.765-1.589 8.199-6.086 8.199-11.386 0-6.627-5.373-12-12-12z" />
									</svg>
								}
							>
								Sign up with GitHub
							</Button>
						</div>
					</CardBody>
				</Card>
			</div>
		</div>
	)
}
