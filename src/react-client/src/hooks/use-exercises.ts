import { keepPreviousData, useQuery } from "@tanstack/react-query"

import { apiClient } from "../lib/api-client"
import type { ExercisesQueryParams, ExercisesResponse } from "../types/exercise"

interface UseExercisesOptions {
	page?: number
	pageSize?: number
	search?: string
}

export function useExercises(options: UseExercisesOptions = {}) {
	const { page = 1, pageSize = 10, search } = options

	const queryParams: ExercisesQueryParams = {
		page,
		pageSize,
		...(search && { q: search }),
	}

	return useQuery({
		queryKey: ["exercises", queryParams],
		queryFn: async () => {
			const response = await apiClient.get<
				ExercisesResponse,
				ExercisesQueryParams
			>("/exercises", queryParams)

			if (response.error) {
				throw new Error(response.error)
			}

			return response.data
		},
		staleTime: 5 * 60 * 1000, // 5 minutes
		placeholderData: keepPreviousData,
	})
}
