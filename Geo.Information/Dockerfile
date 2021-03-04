#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Geo.Information/Geo.Information.csproj", "Geo.Information/"]
COPY ["Geo.DatReader/Geo.DatReader.csproj", "Geo.DatReader/"]
RUN dotnet restore "Back.DatReader/Back.DatReader.csproj"
COPY . .
WORKDIR "/src/Geo.Information"
RUN dotnet build "Geo.Information.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Geo.Information.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Geo.Information.dll"]