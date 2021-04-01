#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["SGM.Cidadao.API/SGM.Cidadao.API.csproj", "SGM.Cidadao.API/"]
COPY ["SGM.Cidadao.API/SGM.Cidadao.Application.csproj", "SGM.Cidadao.Application/"]
COPY ["SGM.Cidadao.API/SGM.Cidadao.Domain.csproj", "SGM.Cidadao.Domain/"]
COPY ["SGM.Cidadao.API/SGM.Cidadao.Infra.csproj", "SGM.Cidadao.Infra/"]
RUN dotnet restore "SGM.Cidadao.API/SGM.Cidadao.API.csproj"
COPY . .
WORKDIR "/src/SGM.Cidadao.API"
RUN dotnet build "SGM.Cidadao.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SGM.Cidadao.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SGM.Cidadao.API.dll"]