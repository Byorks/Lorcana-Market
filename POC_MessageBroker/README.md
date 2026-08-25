## Entendendo softwares de mensageria com RabbitMQ

### Como montar o ambiente com o RabbitMQ

Podemos utilizar o Docker para facilitar a instalação do RabbitMQ da seguinte forma:

1° Crie um arquivo docker-compose.yaml.
2° Adicione a imagem do RabbitMQ:

```yaml
services:
  rabbitmq:
    image: rabbitmq:4-management
    ports:
      - "5672:5672"
      - "15672:15672"
```

[How to Run RabbitMQ in Docker Compose](https://medium.com/@kaloyanmanev/how-to-run-rabbitmq-in-docker-compose-e5baccc3e644)