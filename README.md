# Superheroes
## Docker + .Net Core 2.1 + SQL Server + Redis + CQRS
API de superheroes baseado em Docker com ASP.Net Core 2.1, banco de dados SQL Server e cache com Redis.

## Como usar
O projeto é executado rodando o docker-compose. Esse processo cria automaticamente o banco de dados com o nome **SuperHero**.

```
$ docker-compose build --no-cache
$ docker-compose up
```

Após isso, três imagens serão criadas: redis, sql server e dotnet.

## Banco de dados
O banco de dados pode ser conectado com o endereço localhost e porta 1433.

## Usuário para testes
O script deve criar dois usuários: superhero@adm.com.br e superhero@standard.com.br, com a senha _12345678_.

## Swagger
A documentação da API, gerada com Swagger, ficará disponível no endereço http://localhost:3001/swagger/