#### Docker Commands

- docker images
- docker rmi 
- docker ps 
- docker stop `<container-id>`
- docker ps -a
- docker rm `<container-id>`
- docker rm -f $(docker ps -a -q) 

- docker build -t shoppingapi .
- dotnet compose up


#### Moving Local Image to Docker Hub:

- docker login
- docker tag `<ImageId>` `<dockerhubReposirotyFullname>` (EX: docker tag bbb prabhakargvp/shoppingapi )
- docker push `<Image Name>` (EX: docker push prabhakargvp/shoppingapi:latest)


##### Deploying to Azure WebApp (inux) from Docker Hub Registry

- Registry server URL: https://index.docker.io
- Image and tag: prabhakargvp/shoppingapi:latest
- Port: 8080

