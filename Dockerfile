# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

COPY src/. ./src/
WORKDIR /source/src
RUN dotnet publish NamR.Server/NamR.Server.csproj -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app ./

ENV ASPNETCORE_ENVIRONMENT Production
ENV ASPNETCORE_URLS http://*:$PORT

CMD ["dotnet", "NamR.Server.dll"]
