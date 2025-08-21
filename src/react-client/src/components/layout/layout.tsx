import { Outlet } from "react-router-dom"

import Navigation from "../navigation/navigation.tsx"

export default function Layout() {
	return (
		<div className="min-h-screen">
			<Navigation />
			<main className="max-w-7xl mx-auto">
				<Outlet />
			</main>
		</div>
	)
}
