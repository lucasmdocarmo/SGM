#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["SGM.Saude.API/SGM.Saude.API.csproj", "SGM.Saude.API/"]
COPY ["SGM.Shared/SGM.Shared.Core.csproj", "SGM.Shared/"]
COPY ["SGM.Saude.Infra/SGM.Saude.Infra.csproj", "SGM.Saude.Infra/"]
COPY ["SGM.Saude.Domain/SGM.Saude.Domain.csproj", "SGM.Saude.Domain/"]
COPY ["SGM.Saude.Application/SGM.Saude.Application.csproj", "SGM.Saude.Application/"]
RUN dotnet restore "SGM.Saude.API/SGM.Saude.API.csproj"
COPY . .
WORKDIR "/src/SGM.Saude.API"
RUN dotnet build "SGM.Saude.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SGM.Saude.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SGM.Saude.API.dll"]