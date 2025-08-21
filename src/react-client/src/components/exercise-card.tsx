import { Link } from "@heroui/react"

import type { Exercise } from "../types/exercise"

export type ExerciseCardProps = {
	exercise: Exercise
}

export default function ExerciseCard(
	exerciseProps: Readonly<ExerciseCardProps>
) {
	const { exercise } = exerciseProps
	return (
		<div className="border rounded-lg p-4 shadow-sm">
			<h3 className="text-xl font-semibold mb-2">{exercise.name}</h3>
			<p className="text-gray-600 mb-3">
				Primary Muscle Groups: {exercise.primaryMuscles.join(", ")}
			</p>
			<p className="text-gray-600 mb-3">
				Secondary Muscle Groups: {exercise.secondaryMuscles.join(", ")}
			</p>
			<div className="flex gap-2">
				<Link color="primary" href={`/exercises/${exercise.id}`}>
					View Details
				</Link>
				<Link color="primary" href={`/exercises/${exercise.id}/edit`}>
					Edit
				</Link>
			</div>
		</div>
	)
}
