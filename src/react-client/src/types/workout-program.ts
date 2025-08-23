import type { UserResponse } from "./user"

export interface WorkoutProgram {
	id: string
	name: string
	description: string
	durationWeeks: number
	isPublic: boolean
	createdByUser: UserResponse
	createdAt: string
}

export interface ProgramWeek {
	id: string
	workoutProgramId: string
	weekNumber: number
	name: string
	description?: string
	days: ProgramDay[]
}

export interface ProgramDay {
	id: string
	programWeekId: string
	dayNumber: number
	name: string
	description?: string
	setGroups: ProgramSetGroup[]
}

export interface ProgramSetGroup {
	id: string
	programDayId: string
	orderIndex: number
	type: "Regular" | "Superset" | "GiantSet"
	restSeconds: number
	notes?: string
	exercises: ProgramExercise[]
}

export interface ProgramExercise {
	id: string
	programSetGroupId: string
	exerciseId: string
	orderIndex: number
	targetSets: number
	targetReps: string
	targetWeight?: number
	targetRpe?: string
	notes?: string
	progressionRule?: string
}

export interface UserProgram {
	id: string
	userId: string
	workoutProgramId: string
	startDate: string
	endDate?: string
	status: "Active" | "Paused" | "Completed" | "Cancelled"
	currentWeek: number
	currentDay: number
	notes?: string
}

export interface WorkoutSession {
	id: string
	userProgramId: string
	programDayId: string
	workoutDate: string
	startTime?: string
	endTime?: string
	weekNumber: number
	dayNumber: number
	notes?: string
	isCompleted: boolean
	workoutSets: WorkoutSet[]
}

export interface WorkoutSet {
	id: string
	workoutSessionId: string
	programExerciseId: string
	exerciseId: string
	setNumber: number
	reps: number
	weight: number
	rpe?: number
	restSeconds?: number
	notes?: string
	isWarmup: boolean
}

export interface CreateWorkoutProgramRequest {
	name: string
	description?: string
	durationWeeks: number
	isPublic: boolean
}

export interface ConfigureTemplateRequest {
	workoutProgramId: string
	trainingDaysPerWeek: number
	days: TemplateDayRequest[]
}

export interface TemplateDayRequest {
	dayNumber: number
	name: string
	description?: string
	setGroups: TemplateSetGroupRequest[]
}

export interface TemplateSetGroupRequest {
	orderIndex: number
	type: "Regular" | "Superset" | "GiantSet"
	restSeconds: number
	notes?: string
	exercises: TemplateExerciseRequest[]
}

export interface TemplateExerciseRequest {
	exerciseId: string
	orderIndex: number
	targetSets: number
	targetReps: string
	targetWeight?: number
	targetRpe?: string
	notes?: string
	progressionRule?: string
}

export interface StartProgramRequest {
	workoutProgramId: string
	startDate: string
	notes?: string
}