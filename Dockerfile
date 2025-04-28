FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy solution and project files and restore dependencies
COPY *.sln .
COPY BibliotecaDigital.Core/*.csproj ./BibliotecaDigital.Core/
COPY BibliotecaDigital.Infrastructure/*.csproj ./BibliotecaDigital.Infrastructure/
COPY BibliotecaDigital.Api/*.csproj ./BibliotecaDigital.Api/
RUN dotnet restore

# Copy all source code
COPY BibliotecaDigital.Core/. ./BibliotecaDigital.Core/
COPY BibliotecaDigital.Infrastructure/. ./BibliotecaDigital.Infrastructure/
COPY BibliotecaDigital.Api/. ./BibliotecaDigital.Api/

# Build and publish
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "BibliotecaDigital.Api.dll"]