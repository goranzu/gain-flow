import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query"

import { apiClient } from "../lib/api-client"
import type { PaginatedResponse } from "../types/exercise"
import type {
	ConfigureTemplateRequest,
	CreateWorkoutProgramRequest,
	StartProgramRequest,
	WorkoutProgram,
} from "../types/workout-program"

// Query keys
export const workoutProgramKeys = {
	all: ["workout-programs"] as const,
	lists: () => [...workoutProgramKeys.all, "list"] as const,
	list: (params: {
		page?: number
		pageSize?: number
		search?: string
		isPublic?: boolean
	}) => [...workoutProgramKeys.lists(), params] as const,
}

// Get workout programs hook
export function useWorkoutPrograms(params?: {
	page?: number
	pageSize?: number
	search?: string
	isPublic?: boolean
}) {
	return useQuery({
		queryKey: workoutProgramKeys.list(params || {}),
		queryFn: async () => {
			const result = await apiClient.get<PaginatedResponse<WorkoutProgram>>(
				"/workout-programs",
				{
					page: params?.page,
					pageSize: params?.pageSize,
					q: params?.search,
					isPublic: params?.isPublic,
				}
			)

			if (result.error) {
				throw new Error(result.error)
			}

			return result.data
		},
	})
}

// Create workout program mutation
export function useCreateWorkoutProgram() {
	const queryClient = useQueryClient()

	return useMutation({
		mutationFn: async (data: CreateWorkoutProgramRequest) => {
			const result = await apiClient.post<{ programId: string }>(
				"/workout-programs",
				data
			)

			if (result.error) {
				throw new Error(result.error)
			}

			return result.data
		},
		onSuccess: async () => {
			await queryClient.invalidateQueries({ queryKey: workoutProgramKeys.lists() })
		},
	})
}

// Configure template mutation
export function useConfigureTemplate() {
	const queryClient = useQueryClient()

	return useMutation({
		mutationFn: async ({
			programId,
			...data
		}: { programId: string } & Omit<
			ConfigureTemplateRequest,
			"workoutProgramId"
		>) => {
			const payload: ConfigureTemplateRequest = {
				...data,
				workoutProgramId: programId,
			}

			const result = await apiClient.post<{ message: string }>(
				`/workout-programs/${programId}/configure-template`,
				payload
			)

			if (result.error) {
				throw new Error(result.error)
			}

			return result.data
		},
		onSuccess: async () => {
			await queryClient.invalidateQueries({ queryKey: workoutProgramKeys.lists() })
		},
	})
}

// Apply template mutation
export function useApplyTemplate() {
	const queryClient = useQueryClient()

	return useMutation({
		mutationFn: async (programId: string) => {
			const result = await apiClient.post<{
				message: string
				weeksCreated: number
			}>(`/workout-programs/${programId}/apply-template`)

			if (result.error) {
				throw new Error(result.error)
			}

			return result.data
		},
		onSuccess: async () => {
			await queryClient.invalidateQueries({ queryKey: workoutProgramKeys.lists() })
		},
	})
}

// Start program mutation
export function useStartProgram() {
	const queryClient = useQueryClient()

	return useMutation({
		mutationFn: async ({
			programId,
			...data
		}: { programId: string } & Omit<
			StartProgramRequest,
			"workoutProgramId"
		>) => {
			const payload: StartProgramRequest = {
				...data,
				workoutProgramId: programId,
			}

			const result = await apiClient.post<{ userProgramId: string }>(
				`/workout-programs/${programId}/start`,
				payload
			)

			if (result.error) {
				throw new Error(result.error)
			}

			return result.data
		},
		onSuccess: async () => {
			// Could also invalidate user programs if we have that query
			await queryClient.invalidateQueries({ queryKey: workoutProgramKeys.lists() })
		},
	})
}

// Delete workout program mutation
export function useDeleteWorkoutProgram() {
	const queryClient = useQueryClient()

	return useMutation({
		mutationFn: async (programId: string) => {
			const result = await apiClient.delete(`/workout-programs/${programId}`)

			if (result.error) {
				throw new Error(result.error)
			}

			return result.data
		},
		onSuccess: async () => {
			await queryClient.invalidateQueries({ queryKey: workoutProgramKeys.lists() })
		},
	})
}
