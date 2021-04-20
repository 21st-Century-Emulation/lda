FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
ARG CONFIGURATION=release
WORKDIR /app

COPY LDA.vbproj .
RUN dotnet restore

COPY . .
RUN dotnet publish -c $CONFIGURATION -o /app/output --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app/output ./
ENV ASPNETCORE_URLS=http://0.0.0.0:8080
ENTRYPOINT ["dotnet", "LDA.dll"]