import { useState } from "react"
import { useLocation } from "react-router-dom"

import {
	Button,
	Link,
	Navbar,
	NavbarBrand,
	NavbarContent,
	NavbarItem,
	NavbarMenu,
	NavbarMenuItem,
	NavbarMenuToggle,
} from "@heroui/react"

import { useAuth } from "../../contexts/auth-context.tsx"
import { ThemeToggle } from "../theme-toggle"

export default function Navigation() {
	const [isMenuOpen, setIsMenuOpen] = useState(false)
	const { isAuthenticated, logout } = useAuth()
	const location = useLocation()

	const isActiveRoute = (path: string) => {
		if (path === "/" && location.pathname === "/") return true
		if (path !== "/" && location.pathname.startsWith(path)) return true
		return false
	}

	const navigationItems = [
		{ label: "Home", path: "/" },
		{ label: "Programs", path: "/programs" },
		{ label: "Exercises", path: "/exercises" },
	]

	return (
		<Navbar onMenuOpenChange={setIsMenuOpen} isBordered>
			<NavbarContent>
				<NavbarMenuToggle
					aria-label={isMenuOpen ? "Close menu" : "Open menu"}
					className="sm:hidden"
				/>
				<NavbarBrand>
					<p className="font-bold text-inherit">GainFlow</p>
				</NavbarBrand>
			</NavbarContent>

			<NavbarContent className="hidden sm:flex gap-4" justify="center">
				{navigationItems.map((item) => (
					<NavbarItem key={item.path} isActive={isActiveRoute(item.path)}>
						<Link
							color={isActiveRoute(item.path) ? "primary" : "foreground"}
							href={item.path}
						>
							{item.label}
						</Link>
					</NavbarItem>
				))}
			</NavbarContent>
			<NavbarContent justify="end">
				<NavbarItem>
					<ThemeToggle />
				</NavbarItem>
				{isAuthenticated ? (
					<NavbarItem>
						<Button as={Link} color="primary" variant="flat" onPress={logout}>
							Logout
						</Button>
					</NavbarItem>
				) : (
					<>
						<NavbarItem className="hidden lg:flex">
							<Link href="/login">Login</Link>
						</NavbarItem>
						<NavbarItem>
							<Button as={Link} color="primary" href="/register" variant="flat">
								Sign Up
							</Button>
						</NavbarItem>
					</>
				)}
			</NavbarContent>
			<NavbarMenu>
				{navigationItems.map((item) => (
					<NavbarMenuItem key={item.path}>
						<Link
							className="w-full"
							color={isActiveRoute(item.path) ? "primary" : "foreground"}
							href={item.path}
							size="lg"
						>
							{item.label}
						</Link>
					</NavbarMenuItem>
				))}
				<NavbarMenuItem>
					<Link
						className="w-full"
						color="danger"
						href="#"
						size="lg"
						onPress={logout}
					>
						Log Out
					</Link>
				</NavbarMenuItem>
			</NavbarMenu>
		</Navbar>
	)
}
