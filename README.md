# bookstore

commands:
dotnet ef migrations add Inital -c BookDbContext -p ..\Bookstore.Books\Bookstore.Books.csproj -s .\Bookstore.Web.csproj -o Data/Migrations
dotnet ef database update -c UsersDbContext