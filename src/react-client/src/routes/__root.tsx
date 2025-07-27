import {Outlet, createRootRouteWithContext, useRouter} from '@tanstack/react-router'
import type {NavigateOptions, ToOptions} from '@tanstack/react-router'
import {TanStackRouterDevtools} from '@tanstack/react-router-devtools'

import TanStackQueryLayout from '../integrations/tanstack-query/layout.tsx'

import type {QueryClient} from '@tanstack/react-query'
import {HeroUIProvider} from "@heroui/react";
import Navigation from "@/components/navigation.tsx";

interface MyRouterContext {
    queryClient: QueryClient
}

declare module "@react-types/shared" {
    interface RouterConfig {
        href: ToOptions['to'];
        routerOptions: Omit<NavigateOptions, keyof ToOptions>;
    }
}

export const Route = createRootRouteWithContext<MyRouterContext>()({

    component: () => {
        let router = useRouter();
        return (
            <HeroUIProvider
                navigate={(to, options) => router.navigate({to, ...options})}
                useHref={(to) => router.buildLocation({to}).href}
            >
                <div className="dark text-foreground bg-background h-screen">
                    <Navigation />

                    <Outlet/>
                    <TanStackRouterDevtools/>

                    <TanStackQueryLayout/>
                </div>
            </HeroUIProvider>
        )
    },
})
