# Project

Publisher and consumer of queues in RabbitMQ using Docker.

## Description

This project is a .NET Core application that demonstrates the use of queues with RabbitMQ and .NET Core Worker Service. It includes both a publisher and a consumer for the queue. The commands use Docker to run the application.

## Technologies
- Visual Studio 2022 Community
- .NET Core 8.0
- Docker Desktop
- RabbitMQ

## Configuration
Let's assume that Docker Desktop and Visual Studio are installed and running. To install and run the application, follow the steps below:
In Docker, we need two containers: RabbitMQ and the Worker Service. To enable communication between them, we need to create a network in Docker.

1. Clone the repository:
    ```sh
    git clone https://github.com/odairto/Process.git
    ```

2. Create a network in Docker:
    ```sh
    docker network create my_network
    ```

3. Download the RabbitMQ image and run it:
    ```sh
    docker run -d --name rabbitmq --network my_network -p 5672:5672 -p 15672:15672 rabbitmq:3-management
    ```
    This command enables 2 ports in the external environment of Docker. You can access the RabbitMQ Admin interface at `localhost:15672` with user: `guest` and password: `guest`. The port 5672 is used to run the project locally.


4. Create the image and run the Publisher:
    In PowerShell, navigate to the project path, something like: `\Process`, and execute:
    ```sh
    docker build -f Dockerfile.publisher -t publisher:1.0.0 .
    docker run -d --network my_network --name publisher publisher:1.0.0
    ```
    At this point, you can see in the container's log (in Docker Desktop) some data being generated. In the RabbitMQ Admin interface, the Queues tab will show the number of messages increasing. Now let's publish the "Consumer" project.

5. Create the image and run the Consumer:
    ```sh
    docker build -f Dockerfile.consumer -t consumer:1.0.0 .
    docker run -d --network my_network --name consumer consumer:1.0.0
    ```
    Once again, look at the Docker container's log. You will see the messages that were consumed and the Queue having 0 messages, because when the publisher adds a new message, RabbitMQ sends this message to the Consumer.
