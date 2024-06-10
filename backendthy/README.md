# Ticket System-Built in .net core 6
## Used
- Entity Framework
- Sql Server
- RabbitMQ
- MailKit
- BCrypt and JWT

## API Documentation

### Database Setup

To set up the database, you can use the provided SQL export file. Download the file from the following link:

[Download TicketSystem.bacpac](https://github.com/SabirHalil/TicketSystem/blob/master/TicketSystem/assets/TicketSystem.bacpac)

You can import this file into your SQL Server to set up the database schema and data.

  
## EndPoints
### Airport
{
  "endpoints": [
    {
      "method": "POST",
      "url": "/api/Airport",
      "description": "Creates a new airport.",
      "requestBody": {
        "id": 0,
        "city": "string",
        "name": "string",
        "code": "string"
      },
      "responses": [
        {
          "statusCode": 201,
          "description": "Airport created successfully.",
          "body": {
            "id": 2,
            "city": "string",
            "name": "string",
            "code": "string"
          }
        },
        {
          "statusCode": 400,
          "description": "Invalid airport data.",
          "body": {}
        }
      ]
    },
    {
      "method": "GET",
      "url": "/api/Airport/{id}",
      "description": "Retrieves an airport by its ID.",
      "pathParameters": [
        {
          "name": "id",
          "description": "The ID of the airport to retrieve.",
          "required": true,
          "type": "integer"
        }
      ],
      "responses": [
        {
          "statusCode": 200,
          "description": "Airport retrieved successfully.",
          "body": {
            "id": 2,
            "city": "string",
            "name": "string",
            "code": "string"
          }
        },
        {
          "statusCode": 404,
          "description": "Airport not found.",
          "body": {}
        }
      ]
    },
    {
      "method": "GET",
      "url": "/api/Airport",
      "description": "Retrieves all airports.",
      "responses": [
        {
          "statusCode": 200,
          "description": "Airports retrieved successfully.",
          "body": [
            {
              "id": 1,
              "city": "string",
              "name": "string",
              "code": "string"
            },
            {
              "id": 2,
              "city": "string",
              "name": "string",
              "code": "string"
            }
          ]
        }
      ]
    }
  ]
}

### Flight
{
  "endpoints": [
    {
      "method": "POST",
      "url": "/api/Flight",
      "description": "Creates a new flight.",
      "requestBody": {
        "id": 0,
        "flightId": "string",
        "departureAirportId": 1,
        "arrivalAirportId": 2,
        "departureTime": "2024-06-01T00:00:00",
        "arrivalTime": "2024-06-01T00:00:00",
        "capacity": 100
      },
      "responses": [
        {
          "statusCode": 201,
          "description": "Flight created successfully.",
          "body": {
            "id": 1,
            "flightId": "string",
            "departureAirportId": 1,
            "arrivalAirportId": 2,
            "departureTime": "2024-06-01T00:00:00",
            "arrivalTime": "2024-06-01T00:00:00",
            "capacity": 100
          }
        },
        {
          "statusCode": 400,
          "description": "Invalid flight data.",
          "body": {}
        }
      ]
    },
    {
      "method": "GET",
      "url": "/api/Flight/{id}",
      "description": "Retrieves a flight by its ID.",
      "pathParameters": [
        {
          "name": "id",
          "description": "The ID of the flight to retrieve.",
          "required": true,
          "type": "integer"
        }
      ],
      "responses": [
        {
          "statusCode": 200,
          "description": "Flight retrieved successfully.",
          "body": {
            "id": 1,
            "flightId": "string",
            "departureAirportId": 1,
            "arrivalAirportId": 2,
            "departureTime": "2024-06-01T00:00:00",
            "arrivalTime": "2024-06-01T00:00:00",
            "capacity": 100
          }
        },
        {
          "statusCode": 404,
          "description": "Flight not found.",
          "body": {}
        }
      ]
    },
    {
      "method": "GET",
      "url": "/api/Flight/search",
      "description": "Searches for flights based on departure and arrival airports, departure date, and number of passengers.",
      "queryParameters": [
        {
          "name": "departureAirport",
          "description": "The code of the departure airport.",
          "required": true,
          "type": "string"
        },
        {
          "name": "arrivalAirport",
          "description": "The code of the arrival airport.",
          "required": true,
          "type": "string"
        },
        {
          "name": "departureDate",
          "description": "The date of departure.",
          "required": true,
          "type": "string",
          "format": "date-time"
        },
        {
          "name": "numberOfPassengers",
          "description": "The number of passengers.",
          "required": true,
          "type": "integer"
        }
      ],
      "responses": [
        {
          "statusCode": 200,
          "description": "Flights retrieved successfully.",
          "body": [
            {
              "id": 1,
              "flightId": "string",
              "departureAirportId": 1,
              "arrivalAirportId": 2,
              "departureTime": "2024-06-01T00:00:00",
              "arrivalTime": "2024-06-01T00:00:00",
              "capacity": 100
            },
            {
              "id": 2,
              "flightId": "string",
              "departureAirportId": 3,
              "arrivalAirportId": 4,
              "departureTime": "2024-07-01T00:00:00",
              "arrivalTime": "2024-07-01T00:00:00",
              "capacity": 150
            }
          ]
        },
        {
          "statusCode": 404,
          "description": "No flights found.",
          "body": {}
        }
      ]
    }
  ]
}

### MilesSmilesAccount

{
  "endpoints": [
    {
      "method": "POST",
      "url": "/api/MilesSmilesAccount",
      "description": "Creates a new MilesSmiles account for a user.",
      "requestBody": {
        "userId": 1
      },
      "responses": [
        {
          "statusCode": 200,
          "description": "MilesSmiles account created successfully.",
          "body": {}
        },
        {
          "statusCode": 400,
          "description": "Invalid user ID.",
          "body": {}
        }
      ]
    },
    {
      "method": "PUT",
      "url": "/api/MilesSmilesAccount/{userId}",
      "description": "Updates miles for a user's MilesSmiles account.",
      "pathParameters": [
        {
          "name": "userId",
          "description": "The ID of the user whose miles are to be updated.",
          "required": true,
          "type": "integer"
        }
      ],
      "requestBody": {
        "miles": 100
      },
      "responses": [
        {
          "statusCode": 200,
          "description": "Miles updated successfully.",
          "body": {}
        },
        {
          "statusCode": 400,
          "description": "Invalid input.",
          "body": {}
        },
        {
          "statusCode": 404,
          "description": "User not found.",
          "body": {}
        }
      ]
    }
  ]
}

### Ticket

{
  "endpoints": [
    {
      "method": "POST",
      "url": "/api/Ticket/buy",
      "description": "Creates a new ticket and associates passengers with it, also decreases the flight capacity.",
      "requestBody": {
        "flightId": 1,
        "userId": 1,
        "isMilesSmilesPurchase": false,
        "passengers": [
          {
            "id": 0,
            "ticketId": "string",
            "fullName": "string",
            "birthDate": "2024-06-01T00:00:00",
            "gender": true
          }
        ]
      },
      "responses": [
        {
          "statusCode": 200,
          "description": "Ticket purchased successfully.",
          "body": {
            "id": 1,
            "flightId": 1,
            "userId": 1,
            "purchaseDate": "2024-06-01T00:00:00",
            "numberOfPassengers": 1,
            "isMilesSmilesPurchase": false
          }
        },
        {
          "statusCode": 400,
          "description": "Invalid input or purchase failed.",
          "body": {
            "error": "Error message explaining the reason."
          }
        }
      ]
    }
  ]
}

### User

{
  "endpoints": [
    {
      "method": "POST",
      "url": "/api/User/register",
      "description": "Registers a new user.",
      "requestBody": {
        "id": 0,
        "email": "string",
        "password": "string",
        "fullName": "string",
        "birthDate": "2024-06-01T00:00:00",
        "gender": true
      },
      "responses": [
        {
          "statusCode": 201,
          "description": "User registered successfully.",
          "body": {
            "token": "string",
            "user": {
              "id": 1,
              "email": "string",
              "fullName": "string",
              "birthDate": "2024-06-01T00:00:00",
              "gender": true
            }
          }
        },
        {
          "statusCode": 400,
          "description": "Invalid user information or user already exists.",
          "body": {
            "error": "string"
          }
        }
      ]
    },
    {
      "method": "GET",
      "url": "/api/User/{id}",
      "description": "Retrieves a user by their ID.",
      "pathParameters": [
        {
          "name": "id",
          "description": "The ID of the user to retrieve.",
          "required": true,
          "type": "integer"
        }
      ],
      "responses": [
        {
          "statusCode": 200,
          "description": "User retrieved successfully.",
          "body": {
            "id": 1,
            "email": "string",
            "fullName": "string",
            "birthDate": "2024-06-01T00:00:00",
            "gender": true
          }
        },
        {
          "statusCode": 404,
          "description": "User not found.",
          "body": {}
        }
      ]
    },
    {
      "method": "GET",
      "url": "/api/User/login",
      "description": "Logs in a user.",
      "queryParameters": [
        {
          "name": "email",
          "description": "The email of the user.",
          "required": true,
          "type": "string"
        },
        {
          "name": "password",
          "description": "The password of the user.",
          "required": true,
          "type": "string"
        }
      ],
      "responses": [
        {
          "statusCode": 200,
          "description": "User logged in successfully.",
          "body": {
            "user": {
              "id": 1,
              "email": "string",
              "fullName": "string",
              "birthDate": "2024-06-01T00:00:00",
              "gender": true
            },
            "token": "string"
          }
        },
        {
          "statusCode": 404,
          "description": "User not found.",
          "body": {}
        }
      ]
    }
  ]
}





