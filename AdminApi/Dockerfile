FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AdminApi/AdminApi.csproj", "AdminApi/"]
RUN dotnet restore "AdminApi/AdminApi.csproj"
COPY . .
WORKDIR "/src/AdminApi"
RUN dotnet build "AdminApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdminApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdminApi.dll"]
