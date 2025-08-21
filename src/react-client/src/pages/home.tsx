import { Link } from "@heroui/react"

export default function Home() {
	return (
		<div className="p-8">
			<h1 className="text-3xl font-bold mb-6">Welcome to GainFlow</h1>
			<p className="text-lg mb-6">
				Your fitness journey starts here! Track your exercises, monitor your
				progress, and achieve your goals.
			</p>

			<div className="grid grid-cols-1 md:grid-cols-3 gap-6">
				<div className="p-6 rounded-lg">
					<h3 className="text-xl font-semibold mb-3">Exercises</h3>
					<p className="mb-4">Browse and manage your exercise database</p>
					<Link href="/exercises">View Exercises →</Link>
				</div>

				<div className="p-6 rounded-lg">
					<h3 className="text-xl font-semibold mb-3">Workouts</h3>
					<p className="mb-4">Create and track your workout sessions</p>
					<Link href="/workouts">View Workouts →</Link>
				</div>

				<div className="p-6 rounded-lg">
					<h3 className="text-xl font-semibold mb-3">Progress</h3>
					<p className="mb-4">Monitor your fitness progress over time</p>
					<Link href="/progress">View Progress →</Link>
				</div>
			</div>
		</div>
	)
}
