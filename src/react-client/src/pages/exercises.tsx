import { Link } from "react-router-dom"

export default function Exercises() {
	const exercises = [
		{ id: 1, name: "Push-ups", category: "Bodyweight", muscleGroup: "Chest" },
		{ id: 2, name: "Squats", category: "Bodyweight", muscleGroup: "Legs" },
		{ id: 3, name: "Pull-ups", category: "Bodyweight", muscleGroup: "Back" },
		{ id: 4, name: "Plank", category: "Core", muscleGroup: "Core" },
	]

	return (
		<div className="p-8">
			<div className="flex justify-between items-center mb-6">
				<h1 className="text-3xl font-bold">Exercises</h1>
				<Link
					to="/exercises/create"
					className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600"
				>
					Add Exercise
				</Link>
			</div>

			<div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
				{exercises.map((exercise) => (
					<div
						key={exercise.id}
						className="bg-white border rounded-lg p-4 shadow-sm"
					>
						<h3 className="text-xl font-semibold mb-2">{exercise.name}</h3>
						<p className="text-gray-600 mb-1">Category: {exercise.category}</p>
						<p className="text-gray-600 mb-3">
							Muscle Group: {exercise.muscleGroup}
						</p>
						<div className="flex gap-2">
							<Link
								to={`/exercises/${exercise.id}`}
								className="text-blue-600 hover:text-blue-800"
							>
								View Details
							</Link>
							<Link
								to={`/exercises/${exercise.id}/edit`}
								className="text-green-600 hover:text-green-800"
							>
								Edit
							</Link>
						</div>
					</div>
				))}
			</div>
		</div>
	)
}
