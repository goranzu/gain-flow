import { Input } from "@heroui/input"
import { Button } from "@heroui/react"
import { createFormHook } from "@tanstack/react-form"

import { fieldContext, formContext } from "./demo.form-context"

export const { useAppForm } = createFormHook({
  fieldComponents: {
    Input,
    // TextField,
    // Select,
    // TextArea,
  },
  formComponents: {
    // SubscribeButton,
    Button,
  },
  fieldContext,
  formContext,
})
