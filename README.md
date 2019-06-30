# net15-bot-aspnetcore

O projeto contém conexões com os banco de dados mongodb e sql server.

O servidor mongodb pode ser usado pelo docker, assim dispensando a instalação.

Comando:
docker run --name my_mongo -d -p 127.0.0.1:27017:27017 mongo:latest

A connectionString para o mongodb por não usar usuário ou senha, está configurado diretamento no appsettings.connectionstring
