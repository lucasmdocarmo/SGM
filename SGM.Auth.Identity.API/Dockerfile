#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["SGM.Auth.Identity.API/SGM.Auth.Identity.API.csproj", "SGM.Auth.Identity.API/"]
RUN dotnet restore "SGM.Auth.Identity.API/SGM.Auth.Identity.API.csproj"
COPY . .
WORKDIR "/src/SGM.Auth.Identity.API"
RUN dotnet build "SGM.Auth.Identity.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SGM.Auth.Identity.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SGM.Auth.Identity.API.dll"]