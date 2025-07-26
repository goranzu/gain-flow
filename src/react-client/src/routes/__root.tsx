import {Outlet, createRootRouteWithContext} from '@tanstack/react-router'
import {TanStackRouterDevtools} from '@tanstack/react-router-devtools'

import Header from '../components/Header'

import TanStackQueryLayout from '../integrations/tanstack-query/layout.tsx'

import type {QueryClient} from '@tanstack/react-query'
import {HeroUIProvider} from "@heroui/react";

interface MyRouterContext {
    queryClient: QueryClient
}

export const Route = createRootRouteWithContext<MyRouterContext>()({
    component: () => (
        <HeroUIProvider>
            <Header/>

            <Outlet/>
            <TanStackRouterDevtools/>

            <TanStackQueryLayout/>
        </HeroUIProvider>
    ),
})
