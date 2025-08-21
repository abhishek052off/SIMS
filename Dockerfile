# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy solution file
COPY SIMSWeb.sln ./

# Copy csproj files (important for layer caching)
COPY SIMSWeb/*.csproj SIMSWeb/
COPY SIMSWeb.Business/*.csproj SIMSWeb.Business/
COPY SIMSWeb.ConstantsAndUtilities/*.csproj SIMSWeb.ConstantsAndUtilities/
COPY SIMSWeb.Data/*.csproj SIMSWeb.Data/
COPY SIMSWeb.Model/*.csproj SIMSWeb.Model/

# Restore dependencies
RUN dotnet restore SIMSWeb.sln

# Copy the rest of the source
COPY . .

# Build and publish the web project
WORKDIR /src/SIMSWeb
RUN dotnet publish -c Release -o /app/publish

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "SIMSWeb.dll"]
