import { Outlet } from "react-router-dom"

import Navbar from "../navigation/navbar"

export default function Layout() {
	return (
		<div className="min-h-screen bg-gray-50">
			<Navbar />
			<main className="max-w-7xl mx-auto">
				<Outlet />
			</main>
		</div>
	)
}
