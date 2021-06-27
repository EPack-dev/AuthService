FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app
COPY src ./
RUN dotnet restore

WORKDIR /app/AuthService.WebApp
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
COPY --from=build /app/AuthService.WebApp/out .

ENTRYPOINT ["dotnet", "AuthService.WebApp.dll"]