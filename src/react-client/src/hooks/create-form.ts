import { Button, Checkbox, Input } from "@heroui/react"
import { createFormHook, createFormHookContexts } from "@tanstack/react-form"

const { fieldContext, formContext } = createFormHookContexts()

// Allow us to bind components to the form to keep type safety but reduce production boilerplate
// Define this once to have a generator of consistent form instances throughout your app

const { useAppForm } = createFormHook({
	fieldComponents: {
		Input,
		Checkbox,
	},
	formComponents: {
		Button,
	},
	fieldContext,
	formContext,
})

export { useAppForm }
