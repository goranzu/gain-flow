import { useAuth } from "@/context/auth.tsx"
import { Link } from "@heroui/react"
import {
  createFileRoute,
  Outlet,
  redirect,
  useRouter,
} from "@tanstack/react-router"

export const Route = createFileRoute("/_auth")({
  beforeLoad: ({ context, location }) => {
    if(context.auth.bootstrapping) {
      return (
        <div className="h-full p-2 flex items-center justify-center">
          <h1 className="text-2xl">Loading...</h1>
        </div>
      )
    }

    if (!context.auth.user) {
      throw redirect({
        to: "/login",
        search: {
          redirect: location.href,
        },
      })
    }
  },
  component: AuthLayout,
})

function AuthLayout() {
  const router = useRouter()
  const navigate = Route.useNavigate()
  const auth = useAuth()

  const handleLogout = () => {
    if (window.confirm("Are you sure you want to logout?")) {
      auth.logout().then(() => {
        router.invalidate().finally(() => {
          navigate({ to: "/" })
        })
      })
    }
  }

  return (
    <div className="h-full p-2">
      <h1>Authenticated Route</h1>
      <p>This route's content is only visible to authenticated users.</p>
      <ul className="flex gap-2 py-2">
        <li>
          <Link
            href="/dashboard"
            className="hover:underline data-[status='active']:font-semibold"
          >
            Dashboard
          </Link>
        </li>
        <li>
          <button
            type="button"
            className="hover:underline"
            onClick={handleLogout}
          >
            Logout
          </button>
        </li>
      </ul>
      <hr />
      <Outlet />
    </div>
  )
}
