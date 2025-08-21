import { useState } from "react"

import { Link, Pagination } from "@heroui/react"

import ExerciseCard from "../components/exercise-card.tsx"
import { useExercises } from "../hooks/use-exercises"

export default function Exercises() {
	const [currentPage, setCurrentPage] = useState(1)
	const pageSize = 10

	const {
		data: exercisesData,
		isLoading,
		error,
	} = useExercises({
		page: currentPage,
		pageSize,
	})

	if (error) {
		return (
			<div className="p-8">
				<div className="text-red-500">
					Error loading exercises: {error.message}
				</div>
			</div>
		)
	}

	return (
		<div className="p-8">
			<div className="flex justify-between items-center mb-6">
				<h1 className="text-3xl font-bold">Exercises</h1>
				<Link isBlock color="primary" href="/exercises/create">
					Add Exercise
				</Link>
			</div>

			{isLoading ? (
				<div className="text-center py-8">Loading exercises...</div>
			) : (
				<>
					<div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4 mb-8">
						{exercisesData?.items?.map((exercise) => (
							<ExerciseCard key={exercise.id} exercise={exercise} />
						))}
					</div>

					{exercisesData && exercisesData.totalPages > 1 && (
						<div className="flex justify-center">
							<Pagination
								total={exercisesData.totalPages}
								page={currentPage}
								onChange={setCurrentPage}
								showControls
								showShadow
								color="primary"
								isCompact
							/>
						</div>
					)}
				</>
			)}
		</div>
	)
}
