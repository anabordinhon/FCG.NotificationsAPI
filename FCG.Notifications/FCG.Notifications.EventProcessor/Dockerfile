# Estágio 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar os arquivos de projeto (.csproj) para restaurar as dependências
# O caminho à esquerda é relativo ao CONTEXTO do build (raiz da solução)
COPY ["FCG.Notifications.EventProcessor/FCG.Notifications.EventProcessor.csproj", "FCG.Notifications.EventProcessor/"]
COPY ["FCG.Notifications.Application/FCG.Notifications.Application.csproj", "FCG.Notifications.Application/"]
COPY ["FCG.Notifications.Domain/FCG.Notifications.Domain.csproj", "FCG.Notifications.Domain/"]
COPY ["FCG.Notifications.Infrastructure/FCG.Notifications.Infrastructure.csproj", "FCG.Notifications.Infrastructure/"]

# Restaurar dependências
RUN dotnet restore "FCG.Notifications.EventProcessor/FCG.Notifications.EventProcessor.csproj"

# Copiar todo o código fonte da solução
COPY . .

# Mudar para a pasta do projeto de entrada para o Build/Publish
WORKDIR "/src/FCG.Notifications.EventProcessor"
RUN dotnet build "FCG.Notifications.EventProcessor.csproj" -c Release -o /app/build
RUN dotnet publish "FCG.Notifications.EventProcessor.csproj" -c Release -o /app/publish

# Estágio 2: Runtime
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "FCG.Notifications.EventProcessor.dll"]