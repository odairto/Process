# Project

Publisher and consumer of queues in RabbitMQ using Docker.

## Description

This project is a .NET Core application that demonstrates the use of queues with RabbitMQ and .NET Core Worker Service. It includes both a publisher and a consumer for the queue. The commands use Docker to run the application.

## Technologies
- Visual Studio 2022 Community
- NET Core 8.0
- Docker Desktop
- RabbitMQ

## Configuration
Let's assume that Docker Desktop and Visual Studio are installed and running. To install and run the application, follow the steps below:
In Docker, we need two containers: RabbitMQ and the Worker Service. To enable communication between them, we need to create a network in Docker.

1. Clone repository:
   `git clone https://github.com/odairto/Process.git`

2. Create a network in Docker:
`docker network create my_network`

3. Download RabbitMQ Image and run:
    `docker run -d --name rabbitmq --network my_network -p 5672:5672 -p 15672:15672 rabbitmq:3-management`
This command enable 2 ports in external environment of Docker. You can access The Admin interface of RabbitMQ in 'localhost:15672' user: 'guest' and password: 'guest'.
The port 5672 you can use to run locally the project and make this happen!

4. Create image and run the Publisher:
	In powershell, access the path of the project. Something like: "Repositories\Process" and execute:
	`docker build -t generating-data:1.0.0 .`
	`docker run -d --network my_network --name process generating-data:1.0.0`
	
	


    