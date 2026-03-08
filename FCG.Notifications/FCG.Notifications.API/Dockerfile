FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FCG.Notifications.API/FCG.Notifications.API.csproj", "FCG.Notifications.API/"]
COPY ["FCG.Notifications.Application/FCG.Notifications.Application.csproj", "FCG.Notifications.Application/"]
COPY ["FCG.Notifications.Domain/FCG.Notifications.Domain.csproj", "FCG.Notifications.Domain/"]
COPY ["FCG.Notifications.Infrastructure/FCG.Notifications.Infrastructure.csproj", "FCG.Notifications.Infrastructure/"]
RUN dotnet restore "FCG.Notifications.API/FCG.Notifications.API.csproj"
COPY . .
RUN dotnet build "FCG.Notifications.API/FCG.Notifications.API.csproj" -c $BUILD_CONFIGURATION --no-restore
RUN dotnet publish "FCG.Notifications.API/FCG.Notifications.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish --no-build /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "FCG.Notifications.API.dll"]
