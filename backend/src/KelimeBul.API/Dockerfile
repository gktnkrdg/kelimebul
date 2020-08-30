# NuGet restore
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

WORKDIR /app
COPY . .
RUN dotnet restore

# publish
FROM build AS publish
CMD ECHO "test"
RUN dotnet publish  KelimeBul.API.csproj -c  Release -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /src
COPY --from=publish /app ./
COPY tr.txt /app/

##local
#ENTRYPOINT ["dotnet", "KelimeBul.API.dll"]

## heroku uses the following
CMD ASPNETCORE_URLS=http://*:$PORT dotnet KelimeBul.API.dll
