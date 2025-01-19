# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
# Stage 1: Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Stage 2: Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiar el archivo NSoft.csproj desde la raíz del contexto
COPY ./NSoft.csproj ./NSoft/

# Restaurar dependencias
RUN dotnet restore ./NSoft/NSoft.csproj

# Copiar el resto del código
COPY ./ ./NSoft/

# Publicar el proyecto
WORKDIR /src/NSoft
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Stage 3: Configurar el entorno de producción
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

#Establecer la varialbe de entorno para el entorno de ejecucion
ENTRYPOINT ["dotnet", "NSoft.dll"]