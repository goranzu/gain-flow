import { useEffect, useState } from "react"

import {
	Button,
	Dropdown,
	DropdownItem,
	DropdownMenu,
	DropdownTrigger,
} from "@heroui/react"
import { IconDeviceDesktop, IconMoon, IconSun } from "@tabler/icons-react"
import { useTheme } from "next-themes"

export function ThemeToggle() {
	const [mounted, setMounted] = useState(false)
	const { theme, setTheme } = useTheme()

	useEffect(() => {
		setMounted(true)
	}, [])

	if (!mounted) return null

	const getIcon = () => {
		switch (theme) {
			case "light":
				return <IconSun size={18} />
			case "dark":
				return <IconMoon size={18} />
			case "system":
				return <IconDeviceDesktop size={18} />
			default:
				return <IconSun size={18} />
		}
	}

	return (
		<Dropdown>
			<DropdownTrigger>
				<Button isIconOnly variant="light" aria-label="Toggle theme">
					{getIcon()}
				</Button>
			</DropdownTrigger>
			<DropdownMenu
				aria-label="Theme selection"
				onAction={(key) => setTheme(key as string)}
				selectedKeys={[theme!]}
				selectionMode="single"
			>
				<DropdownItem key="light" startContent={<IconSun size={16} />}>
					Light
				</DropdownItem>
				<DropdownItem key="dark" startContent={<IconMoon size={16} />}>
					Dark
				</DropdownItem>
				<DropdownItem
					key="system"
					startContent={<IconDeviceDesktop size={16} />}
				>
					System
				</DropdownItem>
			</DropdownMenu>
		</Dropdown>
	)
}
