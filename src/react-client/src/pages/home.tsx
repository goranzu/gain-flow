import { Card, CardBody, CardFooter, CardHeader, Link } from "@heroui/react"

export default function Home() {
	return (
		<div className="p-8">
			<h1 className="text-3xl font-bold mb-6">Welcome to GainFlow</h1>
			<p className="text-lg mb-6">
				Your fitness journey starts here! Track your exercises, monitor your
				progress, and achieve your goals.
			</p>

			<div className="grid grid-cols-1 md:grid-cols-3 gap-6">
				<Card>
					<CardHeader>Exercises</CardHeader>
					<CardBody>Browse and manage your exercise database</CardBody>
					<CardFooter>
						<Link href="/exercises">View Exercises →</Link>
					</CardFooter>
				</Card>

				<Card>
					<CardHeader>Workouts</CardHeader>
					<CardBody>Create and track your workout sessions</CardBody>
					<CardFooter>
						<Link href="/exercises">View Workouts →</Link>
					</CardFooter>
				</Card>

				<Card>
					<CardHeader>Progress</CardHeader>
					<CardBody>Monitor your fitness progress over time</CardBody>
					<CardFooter>
						<Link href="/exercises">View Progress →</Link>
					</CardFooter>
				</Card>
			</div>
		</div>
	)
}
