# AnimalShelterAPI

#### By _Qian Li_ 😊

#### This is my c# and .NET project which builds an API with two versions that allows users to GET,POST, PUT and Delete cats and dogs in a animal shelter.

## Technologies Used

* Git
* C#
* dotnet script(.NET 6.0 CLI)
* .NET
* Swagger
* RestSharp
* Entity Framework Core
* JSON Web Token Authentication
* MySQL Workbench
* VS code

## Description

* A user should be able to GET and POST animals, search animals by `species` , `name`, `age` in different `page` by optionally setting the parameters.
* A user should be able to use V1 and V2 version of AnimalApi.
* In API version v1, only authorized user is able to PUT and DELETE reviews; In API version v2, all users are able to PUT and DELETE reviews.
* In API version V2, a user should be able to look up random cute animals just for fun.

### Set Up and Run Project

1. Clone this repo.
2. Open the terminal and navigate to this project's production directory called "AnimalApi".
3. Within the production directory "AnimalApi", create two three files: `appsettings.json`,  `appsettings.Development.json`, and `EnvironmentVariables.cs`.
4. Within `appsettings.json`, put in the following code. Make sure to replace the `uid` and `pwd` values in the MySQL database connection string with your own username and password for MySQL.

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;database=[YOUR-DATA-BASE];uid=[YOUR-USER-HERE];pwd=[YOUR-PASSWORD];"
  }
}
```

5. Within `appsettings.Development.json`, add the following code:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Trace",
      "Microsoft.AspNetCore": "Information",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  }
}
```
6. Within `EnvironmentVariables.cs`, add the following code:
```
namespace AnimalApiCall.Keys
{
  public static class EnvironmentVariables
  {
    public static string ApiKey = [YOUR-API-KEY-VALUE];
  }
}
```

7. Create the database using the migrations in the AnimalApi project. Open your shell (e.g., Terminal or GitBash) to the production directory "AnimalApi", and run `dotnet ef database update`.  
8. To build out this project in development mode, start the project with `dotnet watch run` in the production directory "AnimalApi".
9. Use your program of choice to make API calls. In your API calls, use the domain _http://localhost:5000_. Keep reading to learn about all of the available endpoints.

## Testing the API Endpoints

You are welcome to test this API via [Postman](https://www.postman.com/), or access the [Swagger UI](https://localhost:5000/swagger/index.html).

### Available Endpoints

```
GET http://localhost:5000/api/{version}/animals/
POST http://localhost:5000/api/{version}/animals/
GET http://localhost:5000/api/{version}/animals/{id}
PUT http://localhost:5000/api/{version}/animals/{id}
DELETE http://localhost:5000/api/{version}/animals/{id}
GET http://localhost:5000/api/{version}/animals/random
```

Note: `{version}` is a version number and it should be replaced with a "v2" or "v1"; `{id}` is a variable and it should be replaced with the id number of the animal you want to GET and POST.

#### Optional Query String Parameters for GET Request

GET requests to `http://localhost:5000/api/{version}/animals/` can optionally include query strings to filter or search animals.

| Parameter   | Type        |  Required    | Description |
| ----------- | ----------- | -----------  | ----------- |
| species    | String      | not required | Returns animals with a matching species value |
| name       | String      | not required | Returns animals with a matching name value |
| minimumAge  | Number      | not required | Returns animals that have an age that is greater than or equal to the specified minimumAge value |
| page  | Number      | not required | Returns animals that in the page |

The following query will return all dogs with a species value of "dog":

```
GET http://localhost:5000/api/{version}/animals?species=dog
```

The following query will return all animals with the name "Tiger":

```
GET http://localhost:5000/api/{version}/travels?name=tiger
```

The following query will return all travel animals with a age of 3 or higher:

```
GET http://localhost:5000/api/{version}/animals?minimumAge=3
```

The following query will return all animals in page 2:

```
GET http://localhost:5000/api/{version}/animals?page=2
```

You can include multiple query strings by separating them with an `&`:

```
GET http://localhost:5000/api/{version}/animals?species=dog&minimumAge=3
```

#### Additional Requirements for POST Request

When making a POST request to `http://localhost:5000/api{version}/animals/`, you need to include a **body**. Here's an example body in JSON:

```json
{
   "name": "Rexie",
    "species": "Dog",
    "breed": "Bulldog",
    "age": 2,
    "adoptionDate": "2022-12-01"
}
```

#### Additional Requirements for PUT Request

When making a PUT request to `http://localhost:5000/api/{version}/animals/{id}`, you need to include a **body** that includes the travel's `animalId` property. Here's an example body in JSON:

```json
{
    "animalId": 2,
    "name": "Rexie",
    "species": "Dog",
    "breed": "Bulldog",
    "age": 2,
    "adoptionDate": "2022-12-01"
}
```

And here's the PUT request we would send the previous body to:

```
http://localhost:5000/api/{version}/animals/2
```

Notice that the value of `animalId` needs to match the id number in the URL. In this example, they are both 2.

## Known Bugs

* For API V1, by providing the correct authentication token in the request headers, the server's authentication middleware are not able to validate the token and authorize the request.
* For API V2, "Enable CORS with attributes" in GET animals: CORS headers are not being added to the response.

## License
[MIT](license.txt)
Copyright (c) 2023 Qian Li 