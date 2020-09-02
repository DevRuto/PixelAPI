FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

COPY *.sln .
COPY PixelAPI/*.csproj ./PixelAPI/
RUN dotnet restore

COPY PixelAPI/. ./PixelAPI/
WORKDIR /app/PixelAPI
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=build /app/PixelAPI/out ./
ENTRYPOINT ["dotnet", "PixelAPI.dll"]