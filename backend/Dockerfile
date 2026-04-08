# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy solution and project files first for better layer caching
COPY NareshLearn.slnx ./

COPY src/NareshLearn.Domain/NareshLearn.Domain.csproj src/NareshLearn.Domain/
COPY src/NareshLearn.Application/NareshLearn.Application.csproj src/NareshLearn.Application/
COPY src/NareshLearn.Infrastructure/NareshLearn.Infrastructure.csproj src/NareshLearn.Infrastructure/
COPY src/NareshLearn.Api/NareshLearn.Api.csproj src/NareshLearn.Api/
COPY tests/NareshLearn.UnitTests/NareshLearn.UnitTests.csproj tests/NareshLearn.UnitTests/
COPY tests/NareshLearn.IntegrationTests/NareshLearn.IntegrationTests.csproj tests/NareshLearn.IntegrationTests/

RUN dotnet restore NareshLearn.slnx

# Copy the rest of the source
COPY . .

# Publish the API
RUN dotnet publish src/NareshLearn.Api/NareshLearn.Api.csproj \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Development

ENTRYPOINT ["dotnet", "NareshLearn.Api.dll"]