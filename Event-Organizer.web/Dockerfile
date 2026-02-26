
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY Event-Organizer.sln ./
COPY Event-Organizer.web/Event-Organizer.web.csproj Event-Organizer.web/
COPY Data/Data.csproj Data/
RUN dotnet restore

COPY . .

RUN dotnet publish Event-Organizer.web/Event-Organizer.web.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Event-Organizer.web.dll"]
