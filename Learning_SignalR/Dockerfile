# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# If you want better layer caching, copy csproj files first and run restore.
# For a quick start, copying everything is fine:
COPY . .
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Listen on port 8080 inside the container
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY --from=build /app/publish ./

# IMPORTANT: Replace 'YourApp.dll' with your app's DLL (usually the project name)
ENTRYPOINT ["dotnet", "Learning_SignalR.dll"]
