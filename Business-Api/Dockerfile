# Consulte https://aka.ms/customizecontainer para aprender a personalizar su contenedor de depuración y cómo Visual Studio usa este Dockerfile para compilar sus imágenes para una depuración más rápida.

# --- Etapa 1: Build ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia todos los archivos .csproj y restaura las dependencias.
# Las rutas ahora incluyen la carpeta 'src/'.
COPY ["src/Business-Api/Business-Api.csproj", "src/Business-Api/"]
COPY ["src/Application/Application.csproj", "src/Application/"]
COPY ["src/Domain/Domain.csproj", "src/Domain/"]
COPY ["src/Infrastructure/Infrastructure.csproj", "src/Infrastructure/"]
RUN dotnet restore "src/Business-Api/Business-Api.csproj"

# Copia todo el código fuente.
COPY . .
WORKDIR "/src/src/Business-Api"
RUN dotnet build "Business-Api.csproj" -c Release -o /app/build

# --- Etapa 2: Publish ---
FROM build AS publish
RUN dotnet publish "Business-Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# --- Etapa 3: Final ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
# El puerto interno estándar para HTTP es 80.
EXPOSE 80
ENTRYPOINT ["dotnet", "Business-Api.dll"]