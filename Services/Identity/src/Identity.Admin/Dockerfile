FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Identity.Admin/Identity.Admin.csproj", "src/Identity.Admin/"]
COPY ["src/Identity.Admin.EntityFramework.Shared/Identity.Admin.EntityFramework.Shared.csproj", "src/Identity.Admin.EntityFramework.Shared/"]
COPY ["src/Identity.Admin.EntityFramework.SqlServer/Identity.Admin.EntityFramework.SqlServer.csproj", "src/Identity.Admin.EntityFramework.SqlServer/"]
COPY ["src/Identity.Shared/Identity.Shared.csproj", "src/Identity.Shared/"]
COPY ["src/Identity.Admin.EntityFramework.PostgreSQL/Identity.Admin.EntityFramework.PostgreSQL.csproj", "src/Identity.Admin.EntityFramework.PostgreSQL/"]
COPY ["src/Identity.Admin.EntityFramework.MySql/Identity.Admin.EntityFramework.MySql.csproj", "src/Identity.Admin.EntityFramework.MySql/"]
RUN dotnet restore "src/Identity.Admin/Identity.Admin.csproj"
COPY . .
WORKDIR "/src/src/Identity.Admin"
RUN dotnet build "Identity.Admin.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Identity.Admin.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true
ENTRYPOINT ["dotnet", "Identity.Admin.dll"]