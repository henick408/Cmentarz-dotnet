# ===== BUILD =====
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# kopiujemy csproj i restore
COPY *.csproj ./
RUN dotnet restore

# kopiujemy resztę
COPY . ./
RUN dotnet publish -c Release -o out

# ===== RUNTIME =====
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

EXPOSE 5151

COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "Cmentarz.dll"]
