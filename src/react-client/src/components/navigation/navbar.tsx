import { Link, useLocation } from "react-router-dom"

export default function Navbar() {
	const location = useLocation()

	const isActive = (path: string) => {
		return (
			location.pathname === path || location.pathname.startsWith(path + "/")
		)
	}

	const navItems = [
		{ path: "/", label: "Home", exact: true },
		{ path: "/exercises", label: "Exercises" },
		{ path: "/workouts", label: "Workouts" },
		{ path: "/progress", label: "Progress" },
	]

	return (
		<nav className="bg-white shadow-sm border-b">
			<div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
				<div className="flex justify-between items-center h-16">
					{/* Logo */}
					<div className="flex-shrink-0">
						<Link to="/" className="text-2xl font-bold text-blue-600">
							GainFlow
						</Link>
					</div>

					{/* Navigation Links */}
					<div className="hidden md:flex space-x-8">
						{navItems.map((item) => (
							<Link
								key={item.path}
								to={item.path}
								className={`px-3 py-2 rounded-md text-sm font-medium transition-colors ${
									(
										item.exact
											? location.pathname === item.path
											: isActive(item.path)
									)
										? "text-blue-600 bg-blue-50"
										: "text-gray-700 hover:text-blue-600 hover:bg-gray-50"
								}`}
							>
								{item.label}
							</Link>
						))}
					</div>

					{/* User Actions */}
					<div className="flex items-center space-x-3">
						<Link
							to="/login"
							className="text-gray-700 hover:text-blue-600 px-3 py-2 rounded-md text-sm font-medium transition-colors"
						>
							Sign In
						</Link>
						<Link
							to="/register"
							className="bg-blue-500 text-white px-4 py-2 rounded-md text-sm font-medium hover:bg-blue-600 transition-colors"
						>
							Sign Up
						</Link>
					</div>

					{/* Mobile menu button */}
					<div className="md:hidden">
						<button className="text-gray-700 hover:text-blue-600">
							<svg
								className="h-6 w-6"
								fill="none"
								viewBox="0 0 24 24"
								stroke="currentColor"
							>
								<path
									strokeLinecap="round"
									strokeLinejoin="round"
									strokeWidth={2}
									d="M4 6h16M4 12h16M4 18h16"
								/>
							</svg>
						</button>
					</div>
				</div>
			</div>
		</nav>
	)
}
