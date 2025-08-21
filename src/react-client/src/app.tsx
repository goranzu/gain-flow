import type { NavigateOptions } from "react-router"
import { Route, Routes, useHref, useNavigate } from "react-router-dom"

import { HeroUIProvider } from "@heroui/react"

import Layout from "./components/layout/layout"
import ProtectedRoute from "./components/protected-route"
import { AuthProvider } from "./contexts/auth-context"
import CreateExercise from "./pages/create-exercise"
import ExerciseDetail from "./pages/exercise-detail"
import Exercises from "./pages/exercises"
import Home from "./pages/home"
import Login from "./pages/login"
import NotFound from "./pages/not-found"
import Progress from "./pages/progress"
import Register from "./pages/register"
import Workouts from "./pages/workouts"

declare module "@react-types/shared" {
	interface RouterConfig {
		routerOptions: NavigateOptions
	}
}

function App() {
	const navigate = useNavigate()

	return (
		<AuthProvider>
			<HeroUIProvider navigate={navigate} useHref={useHref}>
				<Routes>
					<Route
						path="/"
						element={
							<ProtectedRoute>
								<Layout />
							</ProtectedRoute>
						}
					>
						<Route index element={<Home />} />
						<Route path="exercises" element={<Exercises />} />
						<Route path="exercises/:id" element={<ExerciseDetail />} />
						<Route path="exercises/create" element={<CreateExercise />} />
						<Route path="workouts" element={<Workouts />} />
						<Route path="progress" element={<Progress />} />
					</Route>
					<Route path="/login" element={<Login />} />
					<Route path="/register" element={<Register />} />
					<Route path="*" element={<NotFound />} />
				</Routes>
			</HeroUIProvider>
		</AuthProvider>
	)
}

export default App
