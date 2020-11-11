FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["src/aspnetcore/CrudRepository/CrudRepository.csproj", "src/aspnetcore/CrudRepository/"]
COPY *.csproj ./
RUN dotnet restore "src/aspnetcore/CrudRepository/CrudRepository.csproj"
COPY . .
WORKDIR "/src/src/aspnetcore/CrudRepository"
RUN dotnet build "CrudRepository.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CrudRepository.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CrudRepository.dll"]
