# WebAplicationDotnetRabbitMQ
Aplicação com .NET com RabbitMQ 
A aplicação com arquitetura baseada em microsserviços, que envia e recebe informações com intermédio da mensageria (RabbitMQ), as organiza e salva no banco de dados. Onde são acessadas por outra uma API Rest.

# Para rodar a aplicaçao em ambiente de desenvolvimento utilizando Docker
1. Baixe os códigos do repositório Github
2. Faça o pull das imagens do Docker Hub
3. docker pull pereiradiegob/rabbitmq:1.0
4. docker pull pereiradiegob/postgres:1.0
3. Roda as imagnes via composer pelo arquivo docker-compose.yml
