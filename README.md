# Flight-Management

Pravel in a fligth management application that makes it easy for administrators to register airports and create flights between them and for users to create accounts and book flights.

The backend is written in C#, using the .NET (v7.0) developement platform.
The frontend is built using the AngularJS framework.
For data persistence, the relational database SQLite is used.
FluentValidation library is used to validate the requests and AutoMapper to map one object to another.

The REST API is versioned and is built using the MediatR implementaiton of the mediator design pattern.
JWT tokens are used for user authentication and authorization.

The unit tests and the integration tests for the API use the XUnit testing tool and FluentAssertions library.
The code coverage, according to SonarQube, is 87.1%.
