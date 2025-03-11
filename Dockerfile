# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS buildApp
WORKDIR /src

COPY . /src

RUN dotnet restore "reto-back-2ev-bueno.csproj"
RUN dotnet publish "reto-back-2ev-bueno.csproj" -c Release -o /app --no-restore

# Etapa de producción
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /application

COPY --from=buildApp /app ./

ENV ASPNETCORE_URLS="http://+:80"

EXPOSE 80

ENTRYPOINT ["dotnet", "reto-back-2ev-bueno.dll"]
