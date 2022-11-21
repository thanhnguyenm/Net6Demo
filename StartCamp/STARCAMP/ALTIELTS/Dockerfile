#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

#COPY ALTIELTS.csproj ALTIELTS/
COPY ./ALTIELTS/ALTIELTS.csproj ALTIELTS/
RUN dotnet restore ALTIELTS/ALTIELTS.csproj

#COPY . ALTIELTS/
COPY ./ALTIELTS/ ALTIELTS/
WORKDIR /src/ALTIELTS
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "ALTIELTS.dll"]
