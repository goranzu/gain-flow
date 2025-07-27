import { Input } from "@heroui/input"
import { Button } from "@heroui/react"
import { createFormHook } from "@tanstack/react-form"

import { fieldContext, formContext } from "./form-context"

export const { useAppForm } = createFormHook({
  fieldComponents: {
    Input,
  },
  formComponents: {
    Button,
  },
  fieldContext,
  formContext,
})
