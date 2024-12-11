FROM mcr.microsoft.com/dotnet/sdk:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Stock.BE.Email/Stock.BE.Email.csproj", "Stock.BE.Email/"]
COPY ["Stock.BE.Jobs/Stock.BE.Jobs.csproj", "Stock.BE.Jobs/"]
COPY ["Stock.BE.Infrastructure/Stock.BE.Infrastructure.csproj", "Stock.BE.Infrastructure/"]
COPY ["Stock.BE.Core/Stock.BE.Core.csproj", "Stock.BE.Core/"]
COPY ["Stock.BE.Application/Stock.BE.Application.csproj", "Stock.BE.Application/"]
COPY ["Stock.BE.Host/Stock.BE.Host.csproj", "Stock.BE.Host/"]
RUN dotnet restore "./Stock.BE.Host/Stock.BE.Host.csproj"
COPY . .
WORKDIR "/src/Stock.BE.Host"
RUN dotnet build "./Stock.BE.Host.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Stock.BE.Host.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS http://*:5000

EXPOSE 5000
ENTRYPOINT ["dotnet", "Stock.BE.Host.dll"]