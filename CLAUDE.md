# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

GainFlow is a full-stack fitness application with a .NET 9 Web API backend and a React frontend built with TanStack Router. The application focuses on exercise management and user authentication with plans for workout tracking and progress monitoring.

## Development Commands

### Backend (.NET API)
- **Build**: `dotnet build` (from root directory)
- **Run API**: `dotnet run --project src/GainFlow.Api`
- **Run Tests**: `dotnet test`
- **Database Migrations**: 
  - Add migration: `dotnet ef migrations add <MigrationName> --project src/GainFlow.Api --context ApplicationDbContext`
  - Update database: `dotnet ef database update --project src/GainFlow.Api --context ApplicationDbContext`

### Frontend (React Client)
Navigate to `src/react-client/` for all frontend commands:
- **Development**: `npm run dev` (runs on port 3000)
- **Build**: `npm run build`
- **Test**: `npm run test`
- **Lint**: `npm run lint`
- **Format**: `npm run format:write`
- **Check (format + lint)**: `npm run check`

## Architecture Overview

### Backend Structure
- **Vertical Slice Architecture**: Features are organized by business capabilities under `src/GainFlow.Api/Features/`
- **CQRS Pattern**: Commands and Queries are separated with dedicated handlers
- **Entity Framework**: Uses both ApplicationDbContext (business data) and IdentityApplicationDbContext (authentication)
- **Database**: SQLite for development, PostgreSQL support configured
- **Authentication**: ASP.NET Core Identity with cookie-based authentication
- **Validation**: FluentValidation for request validation
- **Error Handling**: Global exception middleware

### Frontend Structure
- **TanStack Router**: File-based routing with type-safe navigation
- **TanStack Query**: Server state management and caching
- **TanStack Form**: Form state management with validation
- **HeroUI + Tailwind**: Component library and styling
- **Authentication**: Context-based auth state with cookie sessions
- **Testing**: Vitest with React Testing Library

### Key Architectural Patterns

#### API Endpoints (Backend)
Each feature follows a consistent pattern:
- `{Feature}Command.cs` - Request/response models
- `{Feature}CommandValidator.cs` - FluentValidation rules
- `{Feature}Endpoint.cs` - Minimal API endpoint registration

#### Route Organization (Frontend)
- `__root.tsx` - Root layout with navigation
- `_auth.tsx` - Protected route layout
- Route files use TanStack Router conventions for nested layouts and authentication

#### Authentication Flow
- Backend uses ASP.NET Core Identity with cookie authentication
- Frontend checks auth status on app load via `/api/me` endpoint
- Protected routes automatically redirect to login when unauthenticated

## Development Workflow

### Adding New API Features
1. Create feature folder under `src/GainFlow.Api/Features/`
2. Implement Command/Query classes with validators
3. Create endpoint class implementing `IEndpoint`
4. Add domain entities to `src/GainFlow.Api/Shared/Domain/Entities/`
5. Configure EF mappings in `src/GainFlow.Api/Shared/Persistence/Configurations/`

### Adding New Frontend Routes
1. Create new route file in `src/react-client/src/routes/`
2. TanStack Router auto-generates route tree in `routeTree.gen.ts`
3. Use `useAuth()` hook for authentication state
4. Implement data fetching with TanStack Query hooks

### Database Changes
- Always create EF migrations for schema changes
- Seed data is applied automatically in development
- Use separate contexts for business data and identity

## Testing Strategy

### Backend
- Unit tests in `tests/GainFlow.Api.UnitTests/`
- Integration tests in `tests/GainFlow.Api.IntegrationTests/`

### Frontend
- Component tests with Vitest and React Testing Library
- Demo files prefixed with `demo.*` can be safely deleted

## API Proxy Configuration

Frontend development server proxies `/api/*` requests to `http://localhost:5000` (the .NET API server).

## Build and Deployment

The .NET project is configured to automatically build the React client during Release builds and include the built files in `wwwroot/`.
