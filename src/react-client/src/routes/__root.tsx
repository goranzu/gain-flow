import { type AuthContext } from "@/context/auth.tsx"
import { HeroUIProvider } from "@heroui/react"
import type { QueryClient } from "@tanstack/react-query"
import {
  createRootRouteWithContext,
  Outlet,
  useRouter,
  type NavigateOptions,
  type ToOptions,
} from "@tanstack/react-router"
import { TanStackRouterDevtools } from "@tanstack/react-router-devtools"

import Navigation from "@/components/navigation.tsx"

import TanStackQueryLayout from "../integrations/tanstack-query/layout.tsx"

interface MyRouterContext {
  queryClient: QueryClient
  auth: AuthContext
}

declare module "@react-types/shared" {
  interface RouterConfig {
    href: ToOptions["to"]
    routerOptions: Omit<NavigateOptions, keyof ToOptions>
  }
}

export const Route = createRootRouteWithContext<MyRouterContext>()({
  component: () => {
    let router = useRouter()

    return (
      <HeroUIProvider
        navigate={(to, options) => router.navigate({ to, ...options })}
        useHref={(to) => router.buildLocation({ to }).href}
      >
        <div className="dark text-foreground bg-background h-screen">
          <Navigation />

          <Outlet />
          <TanStackRouterDevtools />

          <TanStackQueryLayout />
        </div>
      </HeroUIProvider>
    )
  },
})
