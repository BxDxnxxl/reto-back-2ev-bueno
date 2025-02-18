FROM mcr.microsoft.com/dotnet/sdk:8.0 AS buildApp
WORKDIR /src

COPY . /src

WORKDIR /src

RUN dotnet restore "reto-back-2ev-bueno.csproj"

RUN dotnet publish "reto-back-2ev-bueno.csproj" -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /application

COPY --from=buildApp /app ./

EXPOSE 80

ENTRYPOINT ["dotnet", "reto-back-2ev-bueno.dll"]
