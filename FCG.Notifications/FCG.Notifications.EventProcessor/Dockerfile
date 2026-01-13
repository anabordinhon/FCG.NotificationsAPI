# Estágio 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar os .csproj de todas as camadas para restaurar dependências
# Ajuste os caminhos conforme sua estrutura exata de pastas
COPY ["FCG.Notifications.API/FCG.Notifications.API.csproj", "FCG.Notifications.API/"]
COPY ["FCG.Notifications.Application/FCG.Notifications.Application.csproj", "FCG.Notifications.Application/"]
COPY ["FCG.Notifications.Domain/FCG.Notifications.Domain.csproj", "FCG.Notifications.Domain/"]
COPY ["FCG.Notifications.Infrastructure/FCG.Notifications.Infrastructure.csproj", "FCG.Notifications.Infrastructure/"]

# Restaurar dependências
RUN dotnet restore "FCG.Notifications.API/FCG.Notifications.API.csproj"

# Copiar todo o código fonte
COPY . .

# Build e Publish
WORKDIR "/src/FCG.Notifications.API"
RUN dotnet build "FCG.Notifications.API.csproj" -c Release -o /app/build
RUN dotnet publish "FCG.Notifications.API.csproj" -c Release -o /app/publish

# Estágio 2: Runtime (Imagem final leve)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "FCG.Notifications.API.dll"]