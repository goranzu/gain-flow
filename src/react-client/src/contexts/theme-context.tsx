import { ThemeProvider as NextThemesProvider } from "next-themes"

export function ThemeProvider({
	children,
}: Readonly<{ children: React.ReactNode }>) {
	return (
		<NextThemesProvider
			attribute="class"
			defaultTheme="system"
			themes={["light", "dark", "system"]}
			enableSystem
		>
			{children}
		</NextThemesProvider>
	)
}

// eslint-disable-next-line react-refresh/only-export-components
export { useTheme } from "next-themes"
