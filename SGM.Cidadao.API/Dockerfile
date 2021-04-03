#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["SGM.Cidadao.API/SGM.Cidadao.API.csproj", "SGM.Cidadao.API/"]
COPY ["SGM.Shared/SGM.Shared.Core.csproj", "SGM.Shared/"]
COPY ["SGM.Cidadao.Application/SGM.Cidadao.Application.csproj", "SGM.Cidadao.Application/"]
COPY ["SGM.Cidadao.Domain/SGM.Cidadao.Domain.csproj", "SGM.Cidadao.Domain/"]
COPY ["SGM.Cidacao.Infra/SGM.Cidadao.Infra.csproj", "SGM.Cidacao.Infra/"]
COPY ["SGM.IdentityService/SGM.Identity.Service.csproj", "SGM.IdentityService/"]
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