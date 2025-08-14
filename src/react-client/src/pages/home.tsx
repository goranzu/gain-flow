import { Link } from "react-router-dom"

export default function Home() {
	return (
		<div className="p-8">
			<h1 className="text-3xl font-bold mb-6">Welcome to GainFlow</h1>
			<p className="text-lg mb-6">
				Your fitness journey starts here! Track your exercises, monitor your
				progress, and achieve your goals.
			</p>

			<div className="grid grid-cols-1 md:grid-cols-3 gap-6">
				<div className="bg-blue-100 p-6 rounded-lg">
					<h3 className="text-xl font-semibold mb-3">Exercises</h3>
					<p className="mb-4">Browse and manage your exercise database</p>
					<Link to="/exercises" className="text-blue-600 hover:text-blue-800">
						View Exercises →
					</Link>
				</div>

				<div className="bg-green-100 p-6 rounded-lg">
					<h3 className="text-xl font-semibold mb-3">Workouts</h3>
					<p className="mb-4">Create and track your workout sessions</p>
					<Link to="/workouts" className="text-green-600 hover:text-green-800">
						View Workouts →
					</Link>
				</div>

				<div className="bg-purple-100 p-6 rounded-lg">
					<h3 className="text-xl font-semibold mb-3">Progress</h3>
					<p className="mb-4">Monitor your fitness progress over time</p>
					<Link
						to="/progress"
						className="text-purple-600 hover:text-purple-800"
					>
						View Progress →
					</Link>
				</div>
			</div>
		</div>
	)
}
