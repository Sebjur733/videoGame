# Game Library

A full-stack game library application built with React, TypeScript, Vite and ASP.NET Core.

The application allows users to register, login, search for games using the IGDB API, and save games to their personal library.

## Technologies

### Frontend
- React
- TypeScript
- Vite
- React Router
- Axios
- CSS

### Backend
- ASP.NET Core (.NET 10)
- Entity Framework Core
- SQLite
- JWT Authentication
- IGDB API integration

---

# Features

## Authentication
- User registration and login
- JWT token based authentication
- Protected routes
- User sessions stored locally

## Game search
- Search games through IGDB API
- Display game covers and information
- Add games to personal library

## User library
- View saved games
- User-specific game collections

---

# Project Structure


testreact
│
├── Backend
│ ├── Controllers
│ ├── Data
│ ├── DTOs
│ ├── Models
│ ├── Services
│ └── Migrations
│
└── Frontend
├── src
│ ├── api
│ ├── components
│ ├── pages
│ └── types


---

# Requirements

Install:

- Node.js
- .NET 10 SDK

---

# Setup

## Backend

Navigate to the backend folder:

```bash
cd Backend

Restore dependencies:

dotnet restore

The backend uses .NET user secrets for sensitive information.

Create your own secrets:

dotnet user-secrets set "Igdb:ClientSecret" "YOUR_IGDB_SECRET"

dotnet user-secrets set "Jwt:Key" "YOUR_JWT_SECRET"

Start the backend:

dotnet run

The API will run locally.

Frontend

Navigate to frontend:

cd Frontend

Install dependencies:

npm install

Start development server:

npm run dev

The frontend will start through Vite.

Environment Secrets

Secrets are not stored in GitHub.

The following values must be configured locally:

Igdb:ClientSecret
Jwt:Key

These are required for:

IGDB game searching
JWT authentication
Authentication Flow
User registers an account.
User logs in with username and password.
Backend validates credentials.
Backend creates a JWT token.
Frontend stores the token locally.
Protected routes require authentication.
CI Pipeline

GitHub Actions is used to automatically check the project.

The pipeline runs:

Backend build
Frontend dependency installation
Frontend TypeScript build

This ensures that changes pushed to GitHub compile successfully.

Running the application

Start backend:

cd Backend
dotnet run

Start frontend in another terminal:

cd Frontend
npm run dev

The application is then ready locally.

Security

Sensitive information is handled using .NET User Secrets instead of storing API keys in the repository.

Never commit:

API keys
JWT secrets
Database passwords
User credentials