import { apiClient } from "../lib/api-client.ts"
import { useEffect, useState } from "react"
import type {
	Exercise,
	ExercisesQueryParams,
	ExercisesResponse,
} from "../types/exercise.ts"
import ExerciseCard from "../components/exercise-card.tsx"
import { Link } from "@heroui/react"

export default function Exercises() {
	const [exercises, setExercises] = useState<Exercise[]>([])

	useEffect(() => {
		async function getExercises() {
			const response = await apiClient.get<
				ExercisesResponse,
				ExercisesQueryParams
			>("/exercises", {
				page: 1,
				pageSize: 10,
			})
			if (response.error) {
				console.error(response.error)
				return
			}

			console.log(response.data)
			setExercises(response.data?.items ?? [])
		}

		void getExercises()
	}, [])

	return (
		<div className="p-8">
			<div className="flex justify-between items-center mb-6">
				<h1 className="text-3xl font-bold">Exercises</h1>
				<Link isBlock color="primary" href="/exercises/create">Add Exercise</Link>
			</div>

			<div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
				{exercises.map((exercise) => (
					<ExerciseCard key={exercise.id} exercise={exercise} />
				))}
			</div>
		</div>
	)
}
