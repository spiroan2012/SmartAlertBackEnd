#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SmartAlertApi/SmartAlertApi.csproj", "SmartAlertApi/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Core/Core.csproj", "Core/"]
RUN dotnet restore "SmartAlertApi/SmartAlertApi.csproj"
COPY . .
WORKDIR "/src/SmartAlertApi"
RUN dotnet build "SmartAlertApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SmartAlertApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SmartAlertApi.dll"]