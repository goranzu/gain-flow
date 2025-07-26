import {createFileRoute} from '@tanstack/react-router'
import {z} from "zod";
import {useAppForm} from "@/hooks/demo.form.ts";
import {Form} from '@heroui/react';

export const Route = createFileRoute('/login')({
    component: LoginComponent,
})

const schema = z.object({
    email: z.string().email("Email is required"),
    password: z.string().min(6, "Password must be at least 6 characters"),
})

function LoginComponent() {
    const form = useAppForm({
        defaultValues: {
            email: '',
            password: '',
        },
        validators: {
            onBlur: schema,
        },
        onSubmit: ({value}) => {
            console.log(value)
            // Show success message
            alert('Form submitted successfully!')
        },
    })

    return (
        <div className="max-w-xl mx-auto p-4 sm:p-6 md:p-8 font-bold mt-12">
            <h1 className="text-2xl">Login</h1>
            <Form
                onSubmit={(e) => {
                    e.preventDefault()
                    e.stopPropagation()
                    form.handleSubmit()
                }}
                className="space-y-6 mt-8"
            >
                <form.AppField name="email">
                    {(field) => <field.Input
                        label="Email"
                        type="email"
                        isRequired
                        labelPlacement="outside"
                        placeholder="example@email.com"
                        name="email"/>}
                </form.AppField>

                <form.AppField name="password">
                    {(field) => <field.Input
                        label="Password"
                        type="password"
                        isRequired
                        labelPlacement="outside"
                        placeholder="*******"
                        name="password"/>}
                </form.AppField>

                <form.AppForm>
                    <form.Button color="primary" type="submit">Submit</form.Button>
                </form.AppForm>
            </Form>
        </div>
    )
}
