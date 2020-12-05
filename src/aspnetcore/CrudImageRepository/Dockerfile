FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["CrudImageRepository.csproj", "./"]
RUN dotnet restore "CrudImageRepository.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "CrudImageRepository.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CrudImageRepository.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "CrudImageRepository.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet CrudImageRepository.dll
