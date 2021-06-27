FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app
COPY src ./
RUN dotnet restore

WORKDIR /app/AuthService.WebApi
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
COPY --from=build /app/AuthService.WebApi/out .

ENTRYPOINT ["dotnet", "AuthService.WebApi.dll"]