FROM mcr.microsoft.com/dotnet/aspnet:6.0 as base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /src

COPY Billing/Billing.csproj Billing/
COPY Messages/Messages.csproj Messages/
COPY Entities/Entities.csproj Entities/
COPY OrderDbContext/OrderDbContext.csproj OrderDbContext/
RUN dotnet restore Billing/Billing.csproj

COPY Billing/ Billing/
COPY Messages/ Messages/
COPY Entities/ Entities/
COPY OrderDbContext/ OrderDbContext/

WORKDIR /src/Billing
RUN dotnet publish -c Release -o /app/publish

FROM base as final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80
EXPOSE 443
ENTRYPOINT [ "dotnet", "Billing.exe" ] 


