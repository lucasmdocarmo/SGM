#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["SGM.Manager.API/SGM.Manager.API.sln", "SGM.Manager.API/"]
COPY ["SGM.Shared/SGM.Shared.Core.csproj", "SGM.Shared/"]
COPY ["SGM.Manager.Application/SGM.Manager.Application.csproj", "SGM.Manager.Application/"]
COPY ["SGM.Manager.Infra/SGM.Manager.Infra.csproj", "SGM.Manager.Infra/"]
COPY ["SGM.Manager.Domain/SGM.Manager.Domain.csproj", "SGM.Manager.Domain/"]
COPY ["SGM.IdentityService/SGM.Identity.Service.csproj", "SGM.IdentityService/"]
RUN dotnet restore "SGM.Manager.API/SGM.Manager.API.sln"
COPY . .
WORKDIR "/src/SGM.Manager.API"
RUN dotnet build "SGM.Manager.API.sln" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SGM.Manager.API.sln" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SGM.Manager.API.dll"]