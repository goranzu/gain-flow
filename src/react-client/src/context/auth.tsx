import { createContext, useCallback, useContext, useState } from "react"

export interface User {
  id: string
  email: string
}

export interface AuthContext {
  isAuthenticated: boolean
  login: (email: string, password: string) => Promise<boolean>
  getMe: () => Promise<void>
  register: (
    email: string,
    password: string,
    confirmPassword: string,
  ) => Promise<void>
  logout: () => Promise<void>
  user: User | null
  serverError: string
}

const AuthContext = createContext<AuthContext | null>(null)

export function AuthProvider({
  children,
}: Readonly<{ children: React.ReactNode }>) {
  const [user, setUser] = useState<User | null>(null)
  const isAuthenticated = !!user
  const [serverError, setServerError] = useState("")

  const headers = {
    "Content-Type": "application/json",
    "X-Requested-With": "XMLHttpRequest",
  }

  const logout = useCallback(async () => {
    const response = await fetch("/api/logout", {
      method: "POST",
      headers: {
        ...headers,
      },
    })

    if (response.ok) {
      setUser(null)
    }
  }, [])

  const login = useCallback(async (email: string, password: string) => {
    setServerError("")

    try {
      const response = await fetch("/api/login", {
        method: "POST",
        body: JSON.stringify({ email, password }),
        headers: {
          ...headers,
        },
      })

      if (response.ok) {
        const data = await response.json()
        setUser({ id: data.id, email: data.email })
        return true
      }

      if (response.status === 400) {
        const data = await response.json()
        const errorMessage = data.detail as string
        setServerError(errorMessage)
        return false
      } else {
        setServerError("Login failed. Please try again.")
        return false
      }
    } catch {
      setServerError("Login failed. Please try again.")
      return false
    }
  }, [])

  const register = useCallback(
    async (email: string, password: string, confirmPassword: string) => {
      setServerError("")
      const response = await fetch("/api/register", {
        method: "POST",
        body: JSON.stringify({ email, password, confirmPassword }),
        headers: {
          ...headers,
        },
      })

      if (response.ok) {
        // send to protected dashboard page
      }

      const data = await response.json()
      const errorMessage = data.detail as string
      setServerError(errorMessage)
    },
    [],
  )

  const getMe = useCallback(async () => {
    const response = await fetch("/api/me", {
      method: "GET",
      headers: {
        ...headers,
      },
    })

    if (response.ok) {
      const data = await response.json()
      setUser({ id: data.id, email: data.email })
      return
    }

    setUser(null)
  }, [])

  const contextData = {
    isAuthenticated,
    user,
    login,
    logout,
    register,
    serverError,
    getMe,
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
