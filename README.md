# net15-bot-aspnetcore

O projeto contém conexões com os banco de dados mongodb e sql server.

O servidor mongodb pode ser usado pelo docker, assim dispensando a instalação.

Comando:
docker run --name my_mongo -d -p 127.0.0.1:27017:27017 mongo:latest

A connectionString para o mongodb por não usar usuário ou senha, está configurado diretamento no appsettings.connectionstring

Para o sqlserver é necessário criar a connectionString por secrets


# Script do banco SQL server
CREATE DATABASE "15NET_999"
GO

USE "15NET_999"
GO

CREATE TABLE LOGS_BOT
(
	ID BIGINT IDENTITY(1,1) PRIMARY KEY,
	MENSAGEM VARCHAR(4000) NOT NULL,
	DATACRIACAO DATETIME NOT NULL
)

GO


# Funções adicionas no projeto.

- Configuração de connection string e secrets
- Serializacao e deserializacao de documentos BSON
- Acesso ao MongoDB (CRUD)
- Acesso ao SQL Server
- Injeção de Dependência
- Uso do pattern de Repositórios

