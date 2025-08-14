import { Link } from "react-router-dom"

export default function NotFound() {
	return (
		<div className="min-h-screen flex items-center justify-center bg-gray-50">
			<div className="text-center">
				<div className="text-8xl font-bold text-gray-300 mb-4">404</div>
				<h1 className="text-2xl font-semibold text-gray-800 mb-4">
					Page Not Found
				</h1>
				<p className="text-gray-600 mb-8">
					Sorry, the page you're looking for doesn't exist.
				</p>
				<Link
					to="/"
					className="bg-blue-500 text-white px-6 py-3 rounded-lg hover:bg-blue-600 transition-colors"
				>
					Go Home
				</Link>
			</div>
		</div>
	)
}
