# Consulte https://aka.ms/customizecontainer para aprender a personalizar su contenedor de depuración y cómo Visual Studio usa este Dockerfile para compilar sus imágenes para una depuración más rápida.

# --- Etapa 1: Build ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# El WORKDIR ahora es la raíz del contexto de build.
WORKDIR /src

# --- CORRECCIÓN DE RUTAS ---
# El contexto de build ya es 'Business-Api', por lo que copiamos su .csproj directamente.
# El primer argumento es la ruta FUERA del contenedor, el segundo es DENTRO.
COPY ["Business-Api.csproj", "Business-Api/"]

# Para los otros proyectos, tenemos que subir un nivel desde el contexto (usando ..)
# ¡¡IMPORTANTE!! Esto solo funciona si el Dockerfile está en un subdirectorio.
COPY ["../Application/Application.csproj", "Application/"]
COPY ["../Domain/Domain.csproj", "Domain/"]
COPY ["../Infrastructure/Infrastructure.csproj", "Infrastructure/"]

# Restauramos las dependencias usando la ruta del proyecto principal.
RUN dotnet restore "Business-Api/Business-Api.csproj"

# Ahora copiamos TODO el código fuente.
# '.' se refiere al contexto actual ('Business-Api').
# '..' se refiere al directorio padre ('src/').
# Copiamos todo el directorio 'src' al directorio de trabajo '/src' del contenedor.
COPY ["..", "."]

# Nos movemos al directorio del proyecto a compilar.
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