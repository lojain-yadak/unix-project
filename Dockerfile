FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["ecommerce.sln", "./"]
COPY ["ecommerce/ecommerce.csproj", "ecommerce/"]

RUN dotnet restore "ecommerce.sln"

COPY . .

WORKDIR /src/ecommerce
RUN dotnet build "ecommerce.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ecommerce.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

RUN mkdir -p /app/uploades
RUN chmod -R 755 /app/uploades

COPY --from=publish /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Production


EXPOSE 80

ENTRYPOINT ["dotnet", "ecommerce.dll"]