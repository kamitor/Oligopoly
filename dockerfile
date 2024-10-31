# Use .NET 8.0 SDK for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy project files and restore dependencies
COPY Source/Oligopoly.Game/Oligopoly.Game.csproj ./Source/Oligopoly.Game/
RUN dotnet restore ./Source/Oligopoly.Game/Oligopoly.Game.csproj

# Copy the remaining project files and build the application
COPY . ./
RUN dotnet publish ./Source/Oligopoly.Game/Oligopoly.Game.csproj -c Release -o /app/out

# Use .NET 8.0 runtime for running the application
FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Entry point
ENTRYPOINT ["dotnet", "Oligopoly.Game.dll"]
