# Modular Monolith POC - Bookstore

A monolith refers to a software application that is deployed as a single physical deployment. Many monolithic applications lack sufficient structure and end up becoming Big Balls of Mud. By contrast, a modular monolith breaks up the application into logical modules which are largely independent from one another. This provides many of the benefits of more distributed approaches like microservices without the overhead of deploying and managing a distributed application.

commands:
dotnet ef migrations add Inital -c BookDbContext -p ..\Bookstore.Books\Bookstore.Books.csproj -s .\Bookstore.Web.csproj -o Data/Migrations
dotnet ef database update -c UsersDbContext

docker run --name bookstore-redis -p 6379:6379 -d redis
