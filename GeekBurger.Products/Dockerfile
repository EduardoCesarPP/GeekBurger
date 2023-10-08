FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copie os arquivos do projeto
COPY GeekBurger.Products/GeekBurger.Products.csproj GeekBurger.Products/
COPY GeekBurger.Products.Contract/GeekBurger.Products.Contract.csproj GeekBurger.Products.Contract/
COPY GeekBurger.Tests/GeekBurger.Tests.csproj GeekBurger.Tests/

RUN dotnet restore GeekBurger.Products/GeekBurger.Products.csproj

# Copie todo o conteúdo
COPY GeekBurger.Products/ GeekBurger.Products/
COPY GeekBurger.Products.Contract/ GeekBurger.Products.Contract/

WORKDIR /src/GeekBurger.Products
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GeekBurger.Products.dll"]
