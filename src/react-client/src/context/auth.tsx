import {
  createContext,
  useCallback,
  useContext,
  useEffect,
  useState,
} from "react"

import { api } from "@/lib/api.ts"

export interface User {
  id: string
  email: string
}

export interface AuthContext {
  loading: boolean
  bootstrapping: boolean
  login: (email: string, password: string) => Promise<boolean>
  getMe: () => Promise<User | null>
  register: (
    email: string,
    password: string,
    confirmPassword: string,
  ) => Promise<void>
  logout: () => Promise<void>
  user: User | null
  clearError?: () => void
  error?: string | null
}

const AuthContext = createContext<AuthContext | null>(null)

export function AuthProvider({
  children,
}: Readonly<{ children: React.ReactNode }>) {
  const [user, setUser] = useState<User | null>(null)
  const [loading, setLoading] = useState(false)
  const [bootstrapping, setBootstrapping] = useState(true)
  const [error, setError] = useState<string | null>(null)

  const refresh = useCallback(async () => {
    const data = await getMe()
    setUser(data)
  }, [])

  useEffect(() => {
    ;(async () => {
      setBootstrapping(true)
      await refresh()
      setBootstrapping(false)
    })()
  }, [refresh])

  const logout = useCallback(async () => {
    setLoading(true)
    const response = await api.post("/api/logout")
    setUser(null)
    setLoading(false)
    if (!response.success) {
      setError(response.error || "Logout failed. Please try again.")
    }
  }, [])

  const login = useCallback(async (email: string, password: string) => {
    setError(null)
    setLoading(true)
    const response = await api.post("/api/login", { email, password })
    if (response.success) {
      await refresh()
    } else {
      setError(response.error || "Login failed. Please try again.")
    }

    setLoading(false)
    return response.success
  }, [])

  const register = useCallback(
    async (email: string, password: string, confirmPassword: string) => {
      setError(null)
      setLoading(true)
      const response = await api.post("/api/register", {
        email,
        password,
        confirmPassword,
      })
      if (response.success) {
        await refresh()
      } else {
        setError(response.error || "Registration failed. Please try again.")
      }

      setLoading(false)
    },
    [],
  )

  const getMe = useCallback(async () => {
    setLoading(true)
    const response = await api.get<User>("/api/me")
    setLoading(false)
    if (response.success && response.data) {
      return response.data
    }
    return null
  }, [])

  const contextData = {
    user,
    login,
    logout,
    register,
    getMe,
    loading,
    bootstrapping,
    error
  }

  return (
    <AuthContext.Provider value={contextData}>{children}</AuthContext.Provider>
  )
}

export function useAuth() {
  const context = useContext(AuthContext)
  if (!context) {
    throw new Error("useAuth must be used within an AuthProvider")
  }
  return context
}
