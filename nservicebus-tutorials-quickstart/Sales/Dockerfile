FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /src

COPY Sales/Sales.csproj Sales/
COPY Messages/Messages.csproj Messages/
COPY Entities/Entities.csproj Entities/
COPY OrderDbContext/OrderDbContext.csproj OrderDbContext/
RUN dotnet restore Sales/Sales.csproj

COPY Sales/ Sales/
COPY Messages/ Messages/
COPY Entities/ Entities/
COPY OrderDbContext/ OrderDbContext/

WORKDIR /src/Sales
RUN dotnet publish -c Release -o /app/publish

WORKDIR /app/publish

EXPOSE 80
EXPOSE 443
CMD [ "./Sales" ] 


