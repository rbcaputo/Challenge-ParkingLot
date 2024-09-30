# ParkingLotAPI

** Disclaimer **
This is ultimately an unfinished project, it is intented to be presented as an MVP.

## Introduction
ParkingLotAPI is a RESTful Web API which goal is to serve as the middleware between user and database in the context of a parking lot business. It encompasses all CRUD basic operations and adheres to most SOLID principles.
The application is currently in the MVP stage. There are improvements to be made and features to consider, depending on the complexity of the business rules. Some ideas for future expansion are already implemented in the code base, although they are not being used in this version.
It was developed leveraging some of the latest features of .NET 8.0. Since it is in development stage, Swagger is active as means to test HTTP requests transactions.

The further development of this application can largely benefit from the introduction of better validation and exception handling. The management of different user access levels can also be implemented through JWT and proper routing.

## Technologies
- SQL Server
- Visual Studio 2022
- C#
- ASP.NET
- Entity Framework
- LINQ

## Structure
![ParkingLot UML Model](https://github.com/user-attachments/assets/95069c9b-c844-43f6-96e0-f501a2c24b95)

## Running the application
n
1. Clone or download the code to your local machine.
2. Open Visual Studio 2022, make sure the latest IDE updates are installed and load the project 'sln' file.
3. Go to Tools > NuGet Package Manager > Package Manager Console.
4. Type in the command restore-package.
5. Type in the command update-database.
6. Either build or run the application.

## Frontend application
[ParkingLotUI Repository](https://github.com/rbcaputo/ParkingLotUI)
