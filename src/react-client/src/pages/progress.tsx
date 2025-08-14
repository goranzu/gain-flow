import { Link } from "react-router-dom"

export default function Progress() {
	const stats = [
		{ label: "Total Workouts", value: "24", change: "+3 this week" },
		{ label: "Total Exercises", value: "156", change: "+12 this month" },
		{ label: "Average Duration", value: "45 min", change: "+5 min this month" },
		{ label: "Streak", value: "7 days", change: "Keep it up!" },
	]

	const recentAchievements = [
		{
			id: 1,
			title: "First Week Complete!",
			date: "2024-01-15",
			description: "Completed your first week of workouts",
		},
		{
			id: 2,
			title: "Push-up Master",
			date: "2024-01-12",
			description: "Completed 100 push-ups in one workout",
		},
		{
			id: 3,
			title: "Consistency Champion",
			date: "2024-01-10",
			description: "5 days workout streak achieved",
		},
	]

	return (
		<div className="p-8">
			<div className="flex justify-between items-center mb-6">
				<h1 className="text-3xl font-bold">Progress Tracking</h1>
				<Link
					to="/progress/detailed"
					className="bg-purple-500 text-white px-4 py-2 rounded hover:bg-purple-600"
				>
					Detailed Analytics
				</Link>
			</div>

			{/* Stats Overview */}
			<div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-4 mb-8">
				{stats.map((stat, index) => (
					<div
						key={index}
						className="bg-white border rounded-lg p-4 text-center"
					>
						<div className="text-2xl font-bold text-blue-600 mb-2">
							{stat.value}
						</div>
						<div className="text-sm font-medium text-gray-800 mb-1">
							{stat.label}
						</div>
						<div className="text-xs text-green-600">{stat.change}</div>
					</div>
				))}
			</div>

			{/* Progress Chart Placeholder */}
			<div className="bg-white border rounded-lg p-6 mb-8">
				<h2 className="text-xl font-semibold mb-4">Weekly Progress</h2>
				<div className="h-48 bg-gray-100 rounded flex items-center justify-center">
					<p className="text-gray-500">📊 Progress chart would go here</p>
				</div>
			</div>

			{/* Recent Achievements */}
			<div className="bg-white border rounded-lg p-6">
				<h2 className="text-xl font-semibold mb-4">Recent Achievements</h2>
				<div className="space-y-3">
					{recentAchievements.map((achievement) => (
						<div
							key={achievement.id}
							className="flex items-start gap-3 p-3 bg-yellow-50 rounded"
						>
							<div className="text-2xl">🏆</div>
							<div className="flex-1">
								<h3 className="font-medium text-gray-800">
									{achievement.title}
								</h3>
								<p className="text-sm text-gray-600 mb-1">
									{achievement.description}
								</p>
								<p className="text-xs text-gray-500">{achievement.date}</p>
							</div>
						</div>
					))}
				</div>
			</div>
		</div>
	)
}
