FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DefineFIT/DefineFIT.Api.csproj", "DefineFIT/"]
COPY ["DefineFIT.Application/DefineFIT.Application.csproj", "DefineFIT.Application/"]
COPY ["DefineFIT.Domain/DefineFIT.Domain.csproj", "DefineFIT.Domain/"]
COPY ["DefineFIT.Infra/DefineFIT.Infra.csproj", "DefineFIT.Infra/"]
RUN dotnet restore "DefineFIT/DefineFIT.Api.csproj"
COPY . .
WORKDIR "/src/DefineFIT"
RUN dotnet build "DefineFIT.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "DefineFIT.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false	

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet DefineFIT.Api.dll