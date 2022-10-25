# ContactList Management

In the UI Layer, which is ASP.NET Core Razor Pages, implemented CRUD with the User Experience of a SPA.

Used Onion Architecture with Inverted Dependencies. There are 4 Layers, 
-ASP.NET Core Web Application with Razor Pages
-Core 
-Infrastructure and
-API Layer

Core Layer will have the Interfaces and Entities while the Infrastructure Layer will have Entity Framework Core, Repository and Unit Of Work Implementations. At the Web Project we will use jQuery Ajax and Partials Views along with jQuery Datatable to build a CRUD Application. For RESTful Web API ASP.NET Core 6.0 and Entity Framework Core was used. This API will manage contact list stored in a relational database (SQL Server)
