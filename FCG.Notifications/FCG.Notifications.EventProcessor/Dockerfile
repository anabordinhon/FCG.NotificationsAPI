FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FCG.Notifications.EventProcessor/FCG.Notifications.EventProcessor.csproj", "FCG.Notifications.EventProcessor/"]
COPY ["FCG.Notifications.Application/FCG.Notifications.Application.csproj", "FCG.Notifications.Application/"]
COPY ["FCG.Notifications.Domain/FCG.Notifications.Domain.csproj", "FCG.Notifications.Domain/"]
COPY ["FCG.Notifications.Infrastructure/FCG.Notifications.Infrastructure.csproj", "FCG.Notifications.Infrastructure/"]
RUN dotnet restore "FCG.Notifications.EventProcessor/FCG.Notifications.EventProcessor.csproj"
COPY . .
RUN dotnet build "FCG.Notifications.EventProcessor/FCG.Notifications.EventProcessor.csproj" -c $BUILD_CONFIGURATION --no-restore
RUN dotnet publish "FCG.Notifications.EventProcessor/FCG.Notifications.EventProcessor.csproj" -c $BUILD_CONFIGURATION -o /app/publish --no-build /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "FCG.Notifications.EventProcessor.dll"]
