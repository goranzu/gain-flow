export interface Exercise {
	id: string
	name: string
	primaryMuscles: string[]
	secondaryMuscles: string[]
}

export interface PaginatedResponse<T> {
	items: T[]
	totalCount: number
	pageSize: number
	pageNumber: number
	totalPages: number
	hasPreviousPage: boolean
	hasNextPage: boolean
}

export type ExercisesResponse = PaginatedResponse<Exercise>

export interface ExercisesQueryParams {
	q?: string
	page?: number
	pageSize?: number
}
