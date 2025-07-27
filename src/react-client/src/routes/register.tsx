import { createFileRoute } from "@tanstack/react-router"

export const Route = createFileRoute("/register")({
  component: RegisterComponent,
})

function RegisterComponent() {
  return <div>Hello "/register"!</div>
}
