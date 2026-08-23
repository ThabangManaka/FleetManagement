FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["Services/Fleet/Fleet.API/Fleet.API.csproj", "Services/Fleet/Fleet.API/"]
COPY ["Services/Fleet/Fleet.Application/Fleet.Application.csproj", "Services/Fleet/Fleet.Application/"]
COPY ["Services/Fleet/Fleet.Core/Fleet.Core.csproj", "Services/Fleet/Fleet.Core/"]
COPY ["Services/Fleet/Fleet.Infrastructure/Fleet.Infrastructure.csproj", "Services/Fleet/Fleet.Infrastructure/"]

RUN dotnet restore "Services/Fleet/Fleet.API/Fleet.API.csproj"

COPY . .

WORKDIR "/src/Services/Fleet/Fleet.API"
RUN dotnet publish "Fleet.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Fleet.API.dll"]