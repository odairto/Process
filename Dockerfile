# Use a imagem base do .NET SDK para compilar o projeto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copie os arquivos do projeto e restaure as dependências
COPY . .
RUN dotnet restore

# Compile o projeto
RUN dotnet publish -c Release -o /app/publish

# Use a imagem base do .NET Runtime para rodar o aplicativo
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Setting and using proper config
ENV DOTNET_ENVIRONMENT=Docker
# COPY GeneratingData/appsettings.Docker.json /app/publish/appsettings.Docker.json


WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "GeneratingData.dll"]
