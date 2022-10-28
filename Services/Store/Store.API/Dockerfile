#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Store/Store.API/Store.API.csproj", "Services/Store/Store.API/"]
COPY ["Services/Store/Store.Infrasructure/Store.Infrasructure.csproj", "Services/Store/Store.Infrasructure/"]
COPY ["Services/Store/Store.Application/Store.Application.csproj", "Services/Store/Store.Application/"]
COPY ["Services/Store/Store.Domain/Store.Domain.csproj", "Services/Store/Store.Domain/"]
RUN dotnet restore "Services/Store/Store.API/Store.API.csproj"
COPY . .
WORKDIR "/src/Services/Store/Store.API"
RUN dotnet build "Store.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Store.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Store.API.dll"]