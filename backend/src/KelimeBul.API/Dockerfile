# NuGet restore
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

WORKDIR /src
COPY . .
RUN dotnet restore "./KelimeBul.API.csproj"

# publish
FROM build AS publish
WORKDIR "/src/."
CMD ECHO "test"
RUN dotnet publish KelimeBul.API.csproj -c Release -o /src/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /src
COPY --from=publish /src/publish ./
COPY tr.txt /src/
#ENTRYPOINT ["dotnet", "KelimeBul.API.dll"]
## heroku uses the following
CMD ASPNETCORE_URLS=http://*:$PORT dotnet KelimeBul.API.dll
