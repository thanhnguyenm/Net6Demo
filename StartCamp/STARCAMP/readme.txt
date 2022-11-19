docker pull <image> : get image from registry

docker images : lits of all images on local machine

docker run <image> : run a docker container of an image

docker run --rm --it <image> : run a docker container of an image, and remove the existing old containter of this image

docker run -d -P --name <name> <image> :
	-d : detach the terminal
	-P : exposed ports to random ports
	--name <name> : set name to container

docker ps -a : list of all containters running

docker run -it <image> sh : run command inside container

docker rm <id container> : delete a container

docker rm $(docker ps -a -q -f status=exited) : delete all containers have a status of existed

docker container prune : remove all containers

docker port <container-name> : list out all ports of container-name

docker stop <container-name> : stop the container-name

docker build

docker login

dockerfile

	FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
	WORKDIR /src

	COPY ALTIELTS.csproj ALTIELTS/
	RUN dotnet restore ALTIELTS/ALTIELTS.csproj

	COPY . ALTIELTS/
	WORKDIR /src/ALTIELTS
	RUN dotnet publish -c Release -o /app/publish

	FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
	WORKDIR /app
	COPY --from=build /app/publish .

	EXPOSE 80
	EXPOSE 443
	ENTRYPOINT ["dotnet", "ALTIELTS.dll"]


# certificates for docker
dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\aspnetapp.pfx -p <CREDENTIAL_PLACEHOLDER>
dotnet dev-certs https --trust

docker run --rm -it -p 8000:80 -p 8001:443 -e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_HTTPS_PORT=8001 -e ASPNETCORE_Kestrel__Certificates__Default__Password="<CREDENTIAL_PLACEHOLDER>" -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx -v ${HOME}/.aspnet/https:/https/ mcr.microsoft.com/dotnet/samples:aspnetapp