#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/Admin/Admin.API/Admin.API.csproj", "src/Admin/Admin.API/"]
COPY ["src/EventBus/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "src/EventBus/EventBusRabbitMQ/"]
RUN dotnet restore "src/Admin/Admin.API/Admin.API.csproj"
COPY . .
WORKDIR "/src/src/Admin/Admin.API"
RUN dotnet build "Admin.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Admin.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Admin.API.dll"]