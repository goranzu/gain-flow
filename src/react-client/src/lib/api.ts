export interface ApiResponse<T = any> {
  data: T | null
  error: string | null
  success: boolean
  status: number
}

export interface ApiOptions extends RequestInit {
  baseUrl?: string
  timeout?: number
  retries?: number
}

class ApiClient {
  private readonly defaultHeaders: HeadersInit = {
    "Content-Type": "application/json",
    "X-Requested-With": "XMLHttpRequest",
  }

  private async makeRequest<T = any>(
    endpoint: string,
    options: ApiOptions = {},
  ): Promise<ApiResponse<T>> {
    const {
      baseUrl = "",
      timeout = 10000,
      retries = 0,
      headers = {},
      ...fetchOptions
    } = options

    const controller = new AbortController()
    const timeoutId = setTimeout(() => controller.abort(), timeout)

    const url = `${baseUrl}${endpoint}`
    const requestOptions: RequestInit = {
      ...fetchOptions,
      headers: {
        ...this.defaultHeaders,
        ...headers,
      },
      signal: controller.signal,
    }

    let lastError: string | null = null

    for (let attempt = 0; attempt <= retries; attempt++) {
      try {
        const response = await fetch(url, requestOptions)
        clearTimeout(timeoutId)

        let data: T | null = null
        const contentType = response.headers.get("content-type")

        if (contentType && contentType.includes("application/json")) {
          try {
            data = await response.json()
          } catch {
            // If JSON parsing fails, leave data as null
          }
        }

        if (response.ok) {
          return {
            data,
            error: null,
            success: true,
            status: response.status,
          }
        } else {
          // Handle specific error status codes
          let errorMessage = "Request failed"

          if (data && typeof data === "object" && "detail" in data) {
            errorMessage = (data as any).detail
          } else if (response.status === 400) {
            errorMessage = "Bad request"
          } else if (response.status === 401) {
            errorMessage = "Unauthorized"
          } else if (response.status === 403) {
            errorMessage = "Forbidden"
          } else if (response.status === 404) {
            errorMessage = "Not found"
          } else if (response.status >= 500) {
            errorMessage = "Server error"
          }

          return {
            data: null,
            error: errorMessage,
            success: false,
            status: response.status,
          }
        }
      } catch (error) {
        clearTimeout(timeoutId)

        if (error instanceof Error) {
          if (error.name === "AbortError") {
            lastError = "Request timeout"
          } else {
            lastError = error.message
          }
        } else {
          lastError = "Network error"
        }

        // If this is the last attempt, break
        if (attempt === retries) break

        // Wait before retrying (exponential backoff)
        await new Promise((resolve) =>
          setTimeout(resolve, Math.pow(2, attempt) * 1000),
        )
      }
    }

    return {
      data: null,
      error: lastError || "Unknown error",
      success: false,
      status: 0,
    }
  }

  async get<T = any>(
    endpoint: string,
    options?: ApiOptions,
  ): Promise<ApiResponse<T>> {
    return this.makeRequest<T>(endpoint, { ...options, method: "GET" })
  }

  async post<T = any>(
    endpoint: string,
    body?: any,
    options?: ApiOptions,
  ): Promise<ApiResponse<T>> {
    const requestOptions: ApiOptions = {
      ...options,
      method: "POST",
    }

    if (body) {
      requestOptions.body = JSON.stringify(body)
    }

    return this.makeRequest(endpoint, requestOptions)
  }

  async put<T = any>(
    endpoint: string,
    body?: any,
    options?: ApiOptions,
  ): Promise<ApiResponse<T>> {
    const requestOptions: ApiOptions = {
      ...options,
      method: "PUT",
    }

    if (body) {
      requestOptions.body = JSON.stringify(body)
    }

    return this.makeRequest(endpoint, requestOptions)
  }

  async delete<T = any>(
    endpoint: string,
    options?: ApiOptions,
  ): Promise<ApiResponse<T>> {
    return this.makeRequest(endpoint, { ...options, method: "DELETE" })
  }
}

export const apiClient = new ApiClient()

export const api = {
  get: apiClient.get.bind(apiClient),
  post: apiClient.post.bind(apiClient),
  put: apiClient.put.bind(apiClient),
  delete: apiClient.delete.bind(apiClient),
}
