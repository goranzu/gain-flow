import { useState } from "react"
import { Link, useNavigate } from "react-router-dom"

export default function CreateExercise() {
	const navigate = useNavigate()
	const [formData, setFormData] = useState({
		name: "",
		category: "",
		muscleGroup: "",
		description: "",
		difficulty: "Beginner",
	})

	const handleSubmit = (e: React.FormEvent) => {
		e.preventDefault()
		// In real app, this would call an API
		console.log("Creating exercise:", formData)
		alert("Exercise created successfully!")
		void navigate("/exercises")
	}

	const handleChange = (
		e: React.ChangeEvent<
			HTMLInputElement | HTMLSelectElement | HTMLTextAreaElement
		>
	) => {
		setFormData((prev) => ({
			...prev,
			[e.target.name]: e.target.value,
		}))
	}

	return (
		<div className="p-8">
			<div className="flex justify-between items-center mb-6">
				<h1 className="text-3xl font-bold">Create New Exercise</h1>
				<Link to="/exercises" className="text-blue-600 hover:text-blue-800">
					← Back to Exercises
				</Link>
			</div>

			<form
				onSubmit={handleSubmit}
				className="bg-white border rounded-lg p-6 max-w-2xl"
			>
				<div className="mb-4">
					<label htmlFor="name" className="block text-sm font-medium mb-2">
						Exercise Name *
					</label>
					<input
						type="text"
						id="name"
						name="name"
						value={formData.name}
						onChange={handleChange}
						required
						className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
					/>
				</div>

				<div className="grid grid-cols-1 md:grid-cols-2 gap-4 mb-4">
					<div>
						<label
							htmlFor="category"
							className="block text-sm font-medium mb-2"
						>
							Category *
						</label>
						<select
							id="category"
							name="category"
							value={formData.category}
							onChange={handleChange}
							required
							className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
						>
							<option value="">Select Category</option>
							<option value="Bodyweight">Bodyweight</option>
							<option value="Weightlifting">Weightlifting</option>
							<option value="Cardio">Cardio</option>
							<option value="Core">Core</option>
						</select>
					</div>

					<div>
						<label
							htmlFor="muscleGroup"
							className="block text-sm font-medium mb-2"
						>
							Muscle Group *
						</label>
						<select
							id="muscleGroup"
							name="muscleGroup"
							value={formData.muscleGroup}
							onChange={handleChange}
							required
							className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
						>
							<option value="">Select Muscle Group</option>
							<option value="Chest">Chest</option>
							<option value="Back">Back</option>
							<option value="Shoulders">Shoulders</option>
							<option value="Arms">Arms</option>
							<option value="Legs">Legs</option>
							<option value="Core">Core</option>
						</select>
					</div>
				</div>

				<div className="mb-4">
					<label
						htmlFor="difficulty"
						className="block text-sm font-medium mb-2"
					>
						Difficulty Level
					</label>
					<select
						id="difficulty"
						name="difficulty"
						value={formData.difficulty}
						onChange={handleChange}
						className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
					>
						<option value="Beginner">Beginner</option>
						<option value="Intermediate">Intermediate</option>
						<option value="Advanced">Advanced</option>
					</select>
				</div>

				<div className="mb-6">
					<label
						htmlFor="description"
						className="block text-sm font-medium mb-2"
					>
						Description
					</label>
					<textarea
						id="description"
						name="description"
						value={formData.description}
						onChange={handleChange}
						rows={4}
						className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
						placeholder="Describe how to perform this exercise..."
					/>
				</div>

				<div className="flex gap-4">
					<button
						type="submit"
						className="bg-blue-500 text-white px-6 py-2 rounded hover:bg-blue-600"
					>
						Create Exercise
					</button>
					<Link
						to="/exercises"
						className="bg-gray-500 text-white px-6 py-2 rounded hover:bg-gray-600"
					>
						Cancel
					</Link>
				</div>
			</form>
		</div>
	)
}
