# CitizenN

A hotel booking engine REST API built on top of .NET 10

## Technologies Used
- .NET 10, 
- C#
- ASP.NET Core
- Entity Framework Core
- SQLite
- Swagger for API documentation

## How to run
```
dotnet run --project .\CitizenNet\CitizenNet.API.csproj
```

Console output will show the URL where the application is running (e.g.  Now listening on: [http://localhost:5255/](http://localhost:5255/)).

## API Documentation

### Accessing the API Documentation
API documentation is available at `/swagger` endpoint after running the application

e.g. [http://localhost:5255/swagger](http://localhost:5255/swagger)

### Seeding the Database
The api provides an endpoint to seed the database with initial data. You can access this via the above swagger documentation. 

### Resetting the Database
Similarly, the api provides an endpoint to reset the database. You can access this via the above swagger documentation.
