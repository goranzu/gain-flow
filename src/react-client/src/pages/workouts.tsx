import { Link } from "react-router-dom"

export default function Workouts() {
	const workouts = [
		{
			id: 1,
			name: "Morning Push Workout",
			date: "2024-01-15",
			exercises: 5,
			duration: "45 min",
		},
		{
			id: 2,
			name: "Leg Day Routine",
			date: "2024-01-13",
			exercises: 8,
			duration: "60 min",
		},
		{
			id: 3,
			name: "Upper Body Strength",
			date: "2024-01-11",
			exercises: 6,
			duration: "50 min",
		},
		{
			id: 4,
			name: "Cardio Session",
			date: "2024-01-09",
			exercises: 3,
			duration: "30 min",
		},
	]

	return (
		<div className="p-8">
			<div className="flex justify-between items-center mb-6">
				<h1 className="text-3xl font-bold">My Workouts</h1>
				<Link
					to="/workouts/create"
					className="bg-green-500 text-white px-4 py-2 rounded hover:bg-green-600"
				>
					Start New Workout
				</Link>
			</div>

			<div className="space-y-4">
				{workouts.map((workout) => (
					<div
						key={workout.id}
						className="bg-white border rounded-lg p-4 shadow-sm"
					>
						<div className="flex justify-between items-start">
							<div>
								<h3 className="text-xl font-semibold mb-2">{workout.name}</h3>
								<div className="flex gap-4 text-sm text-gray-600">
									<span>📅 {workout.date}</span>
									<span>🏋️ {workout.exercises} exercises</span>
									<span>⏱️ {workout.duration}</span>
								</div>
							</div>
							<div className="flex gap-2">
								<Link
									to={`/workouts/${workout.id}`}
									className="text-blue-600 hover:text-blue-800 text-sm"
								>
									View Details
								</Link>
								<Link
									to={`/workouts/${workout.id}/edit`}
									className="text-green-600 hover:text-green-800 text-sm"
								>
									Edit
								</Link>
								<button className="text-red-600 hover:text-red-800 text-sm">
									Delete
								</button>
							</div>
						</div>
					</div>
				))}
			</div>

			{workouts.length === 0 && (
				<div className="text-center py-12">
					<p className="text-gray-500 mb-4">No workouts found</p>
					<Link
						to="/workouts/create"
						className="bg-green-500 text-white px-6 py-3 rounded hover:bg-green-600"
					>
						Create Your First Workout
					</Link>
				</div>
			)}
		</div>
	)
}
