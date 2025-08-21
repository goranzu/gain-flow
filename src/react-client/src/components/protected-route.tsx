import { type ReactNode } from "react"
import { Navigate, useLocation } from "react-router-dom"

import { Spinner } from "@heroui/react"

import { useAuth } from "../contexts/auth-context"

interface ProtectedRouteProps {
	children: ReactNode
	fallback?: ReactNode
}

export default function ProtectedRoute({
	children,
	fallback,
}: Readonly<ProtectedRouteProps>) {
	const { isAuthenticated, isLoading } = useAuth()
	const location = useLocation()

	if (isLoading) {
		return (
			fallback || (
				<div className="flex justify-center items-center min-h-screen">
					<Spinner size="lg" />
				</div>
			)
		)
	}

	if (!isAuthenticated) {
		return <Navigate to="/login" state={{ from: location }} replace />
	}

	return <>{children}</>
}
