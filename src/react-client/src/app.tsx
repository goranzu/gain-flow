import { Routes, Route, useNavigate, useHref } from "react-router-dom"
import Layout from "./components/layout/layout"
import Home from "./pages/home"
import Exercises from "./pages/exercises"
import ExerciseDetail from "./pages/exercise-detail"
import CreateExercise from "./pages/create-exercise"
import Workouts from "./pages/workouts"
import Progress from "./pages/progress"
import Login from "./pages/login"
import Register from "./pages/register"
import NotFound from "./pages/not-found"

import type { NavigateOptions } from "react-router"
import { HeroUIProvider } from "@heroui/react"

declare module "@react-types/shared" {
	interface RouterConfig {
		routerOptions: NavigateOptions
	}
}

function App() {
	const navigate = useNavigate()

	return (
		<HeroUIProvider navigate={navigate} useHref={useHref}>
			<Routes>
				<Route path="/" element={<Layout />}>
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
	)
}

export default App
