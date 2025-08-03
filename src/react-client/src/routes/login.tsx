import { useAuth } from "@/context/auth.tsx"
import { Card, CardBody, CardHeader } from "@heroui/card"
import { Divider, Form, Link } from "@heroui/react"
import { createFileRoute, useRouter } from "@tanstack/react-router"
import { z } from "zod"

import { useAppForm } from "@/hooks/form.ts"

export const Route = createFileRoute("/login")({
  component: LoginComponent,
  validateSearch: z.object({
    redirect: z.string().optional().catch(""),
  }),
})

const schema = z.object({
  email: z.string().email("Email is required"),
  password: z.string().min(6, "Password must be at least 6 characters"),
})

function LoginComponent() {
  const { login, serverError } = useAuth()
  const navigate = Route.useNavigate()
  const search = Route.useSearch()
  const router = useRouter()

  const form = useAppForm({
    defaultValues: {
      email: "",
      password: "",
    },
    validators: {
      onChange: schema,
    },
    onSubmit: async ({ value }) => {
      const success = await login(value.email, value.password)
      if (success) {
        await router.invalidate()
        await navigate({ to: search.redirect ?? "/dashboard" })
      }
    },
  })

  return (
    <div className="mt-38 flex items-center justify-center">
      <Card className="w-full max-w-md shadow-2xl">
        <CardHeader className="flex flex-col gap-3 px-8 pt-8 pb-6">
          <div className="flex flex-col items-center text-center">
            <h1 className="text-foreground text-3xl font-bold">Welcome Back</h1>
            <p className="text-small text-default-500 mt-2">
              Sign in to your account to continue
            </p>
          </div>
        </CardHeader>
        <Divider />
        <CardBody className="px-8 py-6">
          <Form
            onSubmit={async (e) => {
              e.preventDefault()
              e.stopPropagation()
              await form.handleSubmit()
            }}
            className="space-y-6"
          >
            <form.AppField name="email">
              {(field) => (
                <field.Input
                  label="Email"
                  type="email"
                  isRequired
                  labelPlacement="outside"
                  placeholder="example@email.com"
                  name="email"
                  variant="bordered"
                  size="lg"
                  value={field.state.value}
                  onChange={(e) => field.handleChange(e.target.value)}
                  onBlur={field.handleBlur}
                  isInvalid={!field.state.meta.isValid}
                  errorMessage={field.state.meta.errors
                    .map((error) =>
                      typeof error === "string"
                        ? error
                        : error?.message || String(error),
                    )
                    .join(", ")}
                />
              )}
            </form.AppField>

            <form.AppField name="password">
              {(field) => (
                <field.Input
                  label="Password"
                  type="password"
                  isRequired
                  labelPlacement="outside"
                  placeholder="*******"
                  name="password"
                  variant="bordered"
                  size="lg"
                  value={field.state.value}
                  onChange={(e) => field.handleChange(e.target.value)}
                  onBlur={field.handleBlur}
                  isInvalid={!field.state.meta.isValid}
                  errorMessage={field.state.meta.errors
                    .map((error) =>
                      typeof error === "string"
                        ? error
                        : error?.message || String(error),
                    )
                    .join(", ")}
                />
              )}
            </form.AppField>

            <div className="text-small flex w-full items-center justify-end">
              <Link
                href="#"
                size="sm"
                className="text-primary hover:text-primary-600"
              >
                Forgot Password?
              </Link>
            </div>

            <form.Subscribe
              selector={(state) => [state.isSubmitting, state.canSubmit]}
            >
              {([isSubmitting, canSubmit]) => (
                <form.AppForm>
                  <form.Button
                    color="primary"
                    type="submit"
                    size="lg"
                    className="h-12 w-full font-semibold"
                    radius="lg"
                    isDisabled={isSubmitting || !canSubmit}
                  >
                    Sign In
                  </form.Button>
                </form.AppForm>
              )}
            </form.Subscribe>

            <div className="text-small text-default-500 w-full text-center">
              Don't have an account?{" "}
              <Link
                href="/register"
                size="sm"
                className="text-primary hover:text-primary-600 font-medium"
              >
                Sign up
              </Link>
            </div>
          </Form>
          {serverError.length > 0 && (
            <p className="text-small text-danger mt-4 text-center">
              {serverError}
            </p>
          )}
        </CardBody>
      </Card>
    </div>
  )
}
