# MESINA_11042026 - Secure .NET Web API Coding Challenge

A secure, high-performance ASP.NET Core Web API featuring custom API key authentication, JSON file processing, and robust metrics/file tracking.

## Tech Stack
* **Framework:** .NET 10 / ASP.NET Core Web API
* **Documentation:** Swashbuckle (Swagger UI)
* **Architecture:** Clean separation of concerns (Middleware, Services, Controllers)

## Authentication
All endpoints are protected by a custom middleware (`ApiKeyAuthMiddleware`) requiring a valid `X-API-KEY` header.
* **Default API Key:** `TestAPIKey123` (configurable in `appsettings.json`)

## Endpoints
1. **`POST /api/FileUpload/upload`** - Accepts a JSON file upload, filters data, processes records, and tracks metrics. Requires `X-API-KEY` header.
2. **`GET /api/FileUpload/reports`** - Retrieves processing history and metrics. Requires `X-API-KEY` header.

## Running Locally
```bash
dotnet restore
dotnet run