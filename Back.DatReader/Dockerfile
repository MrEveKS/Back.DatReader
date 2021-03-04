#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Back.DatReader/Back.DatReader.csproj", "Back.DatReader/"]
COPY ["File.DatReader/File.DatReader.csproj", "File.DatReader/"]
RUN dotnet restore "Back.DatReader/Back.DatReader.csproj"
COPY . .
WORKDIR "/src/Back.DatReader"
RUN dotnet build "Back.DatReader.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Back.DatReader.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Back.DatReader.dll"]