# Consulte https://aka.ms/customizecontainer para aprender a personalizar su contenedor de depuración y cómo Visual Studio usa este Dockerfile para compilar sus imágenes para una depuración más rápida.

# Usamos la imagen completa del SDK para compilar
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# --- CORRECCIÓN DE RUTAS ---
# Copiamos los archivos .csproj directamente desde la raíz del repositorio.
# No hay una carpeta 'src/'.
COPY ["Business-Api/Business-Api.csproj", "Business-Api/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
# ¡CUIDADO! Tu carpeta se llama "Infraestructure" (con un error de tipeo).
# El Dockerfile debe usar el nombre exacto de la carpeta.
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]

# Restauramos las dependencias
RUN dotnet restore "Business-Api/Business-Api.csproj"

# Copiamos todo el resto del código fuente al contenedor
COPY . .

# Compilamos la aplicación
WORKDIR "/src/Business-Api"
RUN dotnet build "Business-Api.csproj" -c Release -o /app/build

# --- Etapa 2: Publish ---
FROM build AS publish
RUN dotnet publish "Business-Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# --- Etapa 3: Final (Imagen de Producción) ---
# Usamos la imagen ligera de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "Business-Api.dll"]