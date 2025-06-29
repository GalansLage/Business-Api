# Consulte https://aka.ms/customizecontainer para aprender a personalizar su contenedor de depuración y cómo Visual Studio usa este Dockerfile para compilar sus imágenes para una depuración más rápida.

# --- Etapa 1: Build ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Las rutas ahora son relativas a la raíz del repositorio, donde está este Dockerfile.
COPY ["Business-Api/Business-Api.csproj", "Business-Api/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]

# Restauramos las dependencias.
RUN dotnet restore "Business-Api/Business-Api.csproj"

# Copiamos todo el código fuente de la carpeta 'src'.
COPY src/. .

# Compilamos el proyecto principal.
WORKDIR "/src/Business-Api"
RUN dotnet build "Business-Api.csproj" -c Release -o /app/build

# --- Etapa 2: Publish ---
FROM build AS publish
RUN dotnet publish "Business-Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# --- Etapa 3: Final ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "Business-Api.dll"]