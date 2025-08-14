import { Link, useParams } from "react-router-dom"

export default function ExerciseDetail() {
	const { id } = useParams<{ id: string }>()

	// Mock exercise data - in real app this would come from API
	const exercise = {
		id: parseInt(id || "1"),
		name: "Push-ups",
		category: "Bodyweight",
		muscleGroup: "Chest",
		description:
			"A classic upper body exercise that targets the chest, shoulders, and triceps.",
		instructions: [
			"Start in a plank position with hands slightly wider than shoulders",
			"Lower your body until your chest nearly touches the floor",
			"Push back up to the starting position",
			"Repeat for desired number of repetitions",
		],
		difficulty: "Beginner",
	}

	return (
		<div className="p-8">
			<div className="flex justify-between items-center mb-6">
				<Link to="/exercises" className="text-blue-600 hover:text-blue-800">
					← Back to Exercises
				</Link>
				<Link
					to={`/exercises/${id}/edit`}
					className="bg-green-500 text-white px-4 py-2 rounded hover:bg-green-600"
				>
					Edit Exercise
				</Link>
			</div>

			<div className="bg-white border rounded-lg p-6">
				<h1 className="text-3xl font-bold mb-4">{exercise.name}</h1>

				<div className="grid grid-cols-1 md:grid-cols-2 gap-6 mb-6">
					<div>
						<h3 className="text-lg font-semibold mb-2">Details</h3>
						<p className="mb-2">
							<strong>Category:</strong> {exercise.category}
						</p>
						<p className="mb-2">
							<strong>Muscle Group:</strong> {exercise.muscleGroup}
						</p>
						<p className="mb-2">
							<strong>Difficulty:</strong> {exercise.difficulty}
						</p>
					</div>

					<div>
						<h3 className="text-lg font-semibold mb-2">Description</h3>
						<p className="text-gray-700">{exercise.description}</p>
					</div>
				</div>

				<div>
					<h3 className="text-lg font-semibold mb-3">Instructions</h3>
					<ol className="list-decimal list-inside space-y-2">
						{exercise.instructions.map((step, index) => (
							<li key={index} className="text-gray-700">
								{step}
							</li>
						))}
					</ol>
				</div>
			</div>
		</div>
	)
}
