# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

GainFlow is a full-stack fitness application with a .NET 9 Web API backend and a React frontend built with Vite. The application focuses on exercise management, user authentication, workout tracking, and progress monitoring. The frontend uses HeroUI for modern, accessible UI components.

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
- **Development**: `npm run dev` (runs on port 5173)
- **Build**: `npm run build`
- **Preview**: `npm run preview`
- **Lint**: `npm run lint`

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
- **React 19**: Latest React with modern features and hooks
- **Vite**: Fast build tool with hot module replacement
- **TypeScript**: Full type safety with strict configuration
- **HeroUI**: Modern React UI library with Tailwind CSS integration
- **React Router**: Client-side routing with nested layouts
- **ESLint & Prettier**: Code linting and formatting
- **Kebab-case Naming**: All files use kebab-case convention (e.g., `user-profile.tsx`)

### Key Architectural Patterns

#### API Endpoints (Backend)
Each feature follows a consistent pattern:
- `{Feature}Command.cs` - Request/response models
- `{Feature}CommandValidator.cs` - FluentValidation rules
- `{Feature}Endpoint.cs` - Minimal API endpoint registration

#### Component Organization (Frontend)
- `src/main.tsx` - Application entry point with React DOM rendering and HeroUIProvider
- `src/app.tsx` - Main application component with routing configuration
- `src/pages/` - Page components using kebab-case naming (login.tsx, register.tsx, etc.)
- `src/components/` - Reusable components organized by type (layout, navigation)
- Components use HeroUI components for consistent design and accessibility

#### Authentication Flow
- Backend uses ASP.NET Core Identity with cookie authentication
- Frontend has dedicated login (`/login`) and registration (`/register`) pages using HeroUI components
- Forms include validation, loading states, and social login placeholders (Google, GitHub)
- Authentication state management can be implemented using React context or state libraries

## Development Workflow

### Adding New API Features
1. Create feature folder under `src/GainFlow.Api/Features/`
2. Implement Command/Query classes with validators
3. Create endpoint class implementing `IEndpoint`
4. Add domain entities to `src/GainFlow.Api/Shared/Domain/Entities/`
5. Configure EF mappings in `src/GainFlow.Api/Shared/Persistence/Configurations/`

### Adding New Frontend Components
1. Create new component files in `src/react-client/src/components/` or feature-specific folders using kebab-case naming
2. Use HeroUI components for consistent styling and accessibility
3. Follow TypeScript conventions with proper prop types
4. Use React hooks for state management and side effects
5. Implement proper error handling and loading states with HeroUI's built-in states

### Database Changes
- Always create EF migrations for schema changes
- Seed data is applied automatically in development
- Use separate contexts for business data and identity

## Testing Strategy

### Backend
- Unit tests in `tests/GainFlow.Api.UnitTests/`
- Integration tests in `tests/GainFlow.Api.IntegrationTests/`

### Frontend  
- Testing framework not yet configured (ready for Vitest + React Testing Library)
- ESLint and Prettier configured for code quality and consistent formatting
- HeroUI components provide built-in accessibility and testing attributes

## Development Configuration

### Code Quality and Analysis
- **SonarAnalyzer**: Enabled for C# code quality analysis
- **Treat Warnings as Errors**: Enabled in Release builds for strict code quality
- **Central Package Management**: All NuGet package versions managed in `Directory.Packages.props`
- **ESLint & Prettier**: Configured for React and TypeScript best practices with automatic formatting
- **HeroUI**: Provides consistent design system with accessibility built-in

### API Integration
- Frontend should make requests to `http://localhost:5000/api/*` during development
- Configure proxy in Vite config if needed for CORS during development

## Build and Deployment

The .NET project is configured to automatically build the React client during Release builds and include the built files in `wwwroot/`.
