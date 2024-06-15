# Modular Monolith POC - Bookstore

A monolith refers to a software application that is deployed as a single physical deployment. Many monolithic applications lack sufficient structure and end up becoming Big Balls of Mud. By contrast, a modular monolith breaks up the application into logical modules which are largely independent from one another. This provides many of the benefits of more distributed approaches like microservices without the overhead of deploying and managing a distributed application.

commands:
dotnet ef migrations add Inital -c BookDbContext -p ..\Bookstore.Books\Bookstore.Books.csproj -s .\Bookstore.Web.csproj -o Data/Migrations
dotnet ef database update -c UsersDbContext

---Redis cache---
docker run --name bookstore-redis -p 6379:6379 -d redis
-----------------

---Email test server---
docker run --name=papercut -p 25:25 -p 37408:37408 jijiechen/papercut:latest -d 

On http://localhost:37408/# you can see the collection of emails you have sent
----------------

---MongoDb----
docker run --name mongodb -d -p 27017:27017 mongo


### Next steps:
- Create application architecture diagram
- Create API endpoints flow diagrams
- Clear the TODO list
- Add different modules:
    - Payment processing module
    - Text alerts module
    - Shipping module (updates order status, sends a text or when it was delivered)
    - Book reviews module
- Add more reports (geographic with addresses from different countries)
    - Top Sales by Country Report
