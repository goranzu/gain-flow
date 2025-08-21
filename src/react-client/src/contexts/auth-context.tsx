import {
	createContext,
	useContext,
	useEffect,
	useState,
	useCallback,
	type ReactNode,
} from "react"
import { apiClient, withLoadingState } from "../lib/api-client"

interface User {
	id: string
	email: string
	name: string
}

interface AuthContextType {
	user: User | null
	isLoading: boolean
	isAuthenticated: boolean
	error: string | null
	login: (email: string, password: string) => Promise<void>
	logout: () => Promise<void>
	register: (
		email: string,
		password: string,
		confirmPassword: string
	) => Promise<void>
	clearError: () => void
}

const AuthContext = createContext<AuthContextType | null>(null)

export function useAuth() {
	const context = useContext(AuthContext)
	if (!context) {
		throw new Error("useAuth must be used within an AuthProvider")
	}
	return context
}

interface AuthProviderProps {
	children: ReactNode
}

export function AuthProvider({ children }: Readonly<AuthProviderProps>) {
	const [user, setUser] = useState<User | null>(null)
	const [isLoading, setIsLoading] = useState(true)
	const [error, setError] = useState<string | null>(null)

	const clearError = useCallback(() => setError(null), [])

	const login = useCallback(async (email: string, password: string) => {
		setError(null)
		try {
			const userData = await withLoadingState(
				() => apiClient.post<User>("/login", { email, password }),
				setIsLoading
			)
			setUser(userData)
		} catch (err) {
			const errorMessage = err instanceof Error ? err.message : "Login failed"
			setError(errorMessage)
			throw err
		}
	}, [])

	const register = useCallback(
		async (email: string, password: string, confirmPassword: string) => {
			setError(null)
			try {
				const userData = await withLoadingState(
					() =>
						apiClient.post<User>("/register", {
							email,
							password,
							confirmPassword,
						}),
					setIsLoading
				)
				setUser(userData)
			} catch (err) {
				const errorMessage =
					err instanceof Error ? err.message : "Registration failed"
				setError(errorMessage)
				throw err
			}
		},
		[]
	)

	const logout = useCallback(async () => {
		setError(null)
		try {
			await withLoadingState(() => apiClient.post("/logout"), setIsLoading)
			setUser(null)
		} catch (err) {
			const errorMessage = err instanceof Error ? err.message : "Logout failed"
			setError(errorMessage)
			throw err
		}
	}, [])

	const checkAuthStatus = async () => {
		const result = await apiClient.get<User>("/me")
		if (!result.error) {
			setUser(result.data)
		}
		setIsLoading(false)
	}

	useEffect(() => {
		void checkAuthStatus()
	}, [])

	const value: AuthContextType = {
		user,
		isLoading,
		isAuthenticated: !!user,
		error,
		login,
		logout,
		register,
		clearError,
	}

	return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>
}
