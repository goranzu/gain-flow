interface ApiResponse<T = unknown> {
	data: T
	error: null
}

interface ApiError {
	data: null
	error: string
}

interface ProblemDetails {
	type?: string
	title: string
	status: number
	detail?: string
	instance?: string
	errors?: Record<string, string[]>
	[key: string]: unknown
}

type ApiResult<T = unknown> = ApiResponse<T> | ApiError

interface RequestOptions extends RequestInit {
	timeout?: number
}

export class ApiClient {
	private readonly baseUrl: string

	constructor(baseUrl = "/api") {
		this.baseUrl = baseUrl
	}

	private async makeRequest<T>(
		endpoint: string,
		options: RequestOptions = {}
	): Promise<ApiResult<T>> {
		const { timeout = 10000, ...fetchOptions } = options

		const controller = new AbortController()
		const timeoutId = setTimeout(() => controller.abort(), timeout)

		try {
			const response = await fetch(`${this.baseUrl}${endpoint}`, {
				credentials: "include",
				headers: {
					"Content-Type": "application/json",
					"X-Requested-With": "XMLHttpRequest",
					...fetchOptions.headers,
				},
				signal: controller.signal,
				...fetchOptions,
			})

			clearTimeout(timeoutId)

			if (!response.ok) {
				const contentType = response.headers.get("content-type")

				if (contentType?.includes("application/problem+json")) {
					try {
						const problemDetails: ProblemDetails = await response.json()

						// If there are validation errors, format them as a list
						if (problemDetails.errors) {
							const validationErrors = Object.entries(problemDetails.errors)
								.flatMap(([field, messages]) =>
									messages.map((msg) => `${field}: ${msg}`)
								)
								.join("\n")
							return { data: null, error: validationErrors }
						}

						// Otherwise use detail field, falling back to title, then HTTP status
						const errorMessage =
							(problemDetails.detail ??
							problemDetails.title) ||
							`HTTP ${response.status}`

						return { data: null, error: errorMessage }
					} catch {
						// Fall back to text if JSON parsing fails
						const errorText = await response.text()
						return {
							data: null,
							error:
								errorText || `HTTP ${response.status}: ${response.statusText}`,
						}
					}
				} else {
					const errorText = await response.text()
					return {
						data: null,
						error:
							errorText || `HTTP ${response.status}: ${response.statusText}`,
					}
				}
			}

			const contentType = response.headers.get("content-type")
			const data = contentType?.includes("application/json")
				? await response.json()
				: await response.text()

			return { data, error: null }
		} catch (error) {
			clearTimeout(timeoutId)

			if (error instanceof Error) {
				if (error.name === "AbortError") {
					return { data: null, error: "Request timed out" }
				}
				return { data: null, error: error.message }
			}

			return { data: null, error: "An unexpected error occurred" }
		}
	}

	async get<T, P = Record<string, string | number | boolean | undefined>>(
		endpoint: string,
		params?: P,
		options?: RequestOptions
	): Promise<ApiResult<T>> {
		let url = endpoint
		if (params) {
			const searchParams = new URLSearchParams()
			Object.entries(params).forEach(([key, value]) => {
				if (value !== undefined && value !== null) {
					searchParams.append(key, String(value))
				}
			})
			const queryString = searchParams.toString()
			if (queryString) {
				url += `?${queryString}`
			}
		}
		return this.makeRequest<T>(url, { ...options, method: "GET" })
	}

	async post<T>(
		endpoint: string,
		data?: unknown,
		options?: RequestOptions
	): Promise<ApiResult<T>> {
		return this.makeRequest<T>(endpoint, {
			...options,
			method: "POST",
			body: data ? JSON.stringify(data) : undefined,
		})
	}

	async put<T>(
		endpoint: string,
		data?: unknown,
		options?: RequestOptions
	): Promise<ApiResult<T>> {
		return this.makeRequest<T>(endpoint, {
			...options,
			method: "PUT",
			body: data ? JSON.stringify(data) : undefined,
		})
	}

	async delete<T>(
		endpoint: string,
		options?: RequestOptions
	): Promise<ApiResult<T>> {
		return this.makeRequest<T>(endpoint, { ...options, method: "DELETE" })
	}
}

export const apiClient = new ApiClient()

export async function withLoadingState<T>(
	operation: () => Promise<ApiResult<T>>,
	setLoading: (loading: boolean) => void
): Promise<T> {
	setLoading(true)
	try {
		const result = await operation()
		if (result.error) {
			throw new Error(result.error)
		}

		return result.data as T
	} finally {
		setLoading(false)
	}
}
