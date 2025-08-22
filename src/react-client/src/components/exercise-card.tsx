import {
	Card,
	CardBody,
	CardFooter,
	CardHeader,
	Chip,
	Link,
} from "@heroui/react"
import { IconEye, IconPencil, IconWeight } from "@tabler/icons-react"

import type { Exercise } from "../types/exercise"

export interface ExerciseCardProps {
	exercise: Exercise
}

export default function ExerciseCard(
	exerciseProps: Readonly<ExerciseCardProps>
) {
	const { exercise } = exerciseProps

	return (
		<Card className="hover:shadow-lg transition-shadow duration-200 h-full">
			<CardHeader className="pb-3">
				<div className="flex flex-col w-full">
					<h3 className="text-xl font-semibold text-foreground mb-2">
						{exercise.name}
					</h3>
					<div className="flex items-center gap-2 text-small text-default-500">
						<IconWeight className="w-4 h-4" />
						<span>Exercise</span>
					</div>
				</div>
			</CardHeader>

			<CardBody className="pt-0 pb-4">
				<div className="space-y-4">
					{/* Primary Muscle Groups */}
					<div>
						<p className="text-small font-medium text-default-700 mb-2">
							Primary Muscles
						</p>
						<div className="flex flex-wrap gap-1">
							{exercise.primaryMuscles.map((muscle) => (
								<Chip
									key={muscle}
									size="sm"
									color="primary"
									variant="flat"
									className="text-tiny"
								>
									{muscle}
								</Chip>
							))}
						</div>
					</div>

					{/* Secondary Muscle Groups */}
					{exercise.secondaryMuscles.length > 0 && (
						<div>
							<p className="text-small font-medium text-default-700 mb-2">
								Secondary Muscles
							</p>
							<div className="flex flex-wrap gap-1">
								{exercise.secondaryMuscles.map((muscle) => (
									<Chip
										key={muscle}
										size="sm"
										color="default"
										variant="flat"
										className="text-tiny"
									>
										{muscle}
									</Chip>
								))}
							</div>
						</div>
					)}
				</div>
			</CardBody>

			<CardFooter className="pt-2">
				<div className="flex gap-2 w-full">
					<Link
						color="primary"
						href={`/exercises/${exercise.id}`}
						className="flex items-center gap-2 flex-1 justify-center p-2 rounded-medium hover:bg-primary-50 transition-colors"
					>
						<IconEye className="w-4 h-4" />
						<span className="text-small font-medium">View Details</span>
					</Link>

					<Link
						color="primary"
						href={`/exercises/${exercise.id}/edit`}
						className="flex items-center gap-2 flex-1 justify-center p-2 rounded-medium hover:bg-default-100 transition-colors"
					>
						<IconPencil className="w-4 h-4" />
						<span className="text-small font-medium">Edit</span>
					</Link>
				</div>
			</CardFooter>
		</Card>
	)
}
