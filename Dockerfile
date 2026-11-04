FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["MESINA_11042026.csproj", "./"]
RUN dotnet restore "MESINA_11042026.csproj"

COPY . .
RUN dotnet publish "MESINA_11042026.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "MESINA_11042026.dll"]