# TrueSec Vulnerability API

A REST API for managing STIX 2.1 Vulnerability objects, built with ASP.NET Core and MongoDB.

This project provides a set of endpoints to perform CRUD (Create, Read, Update, Delete) operations on vulnerability data.

## Prerequisites

-   [.NET 8 SDK](https://dotnet.microsoft.com/download) (or a compatible version)
-   [Docker](https://www.docker.com/products/docker-desktop/) and Docker Compose

## Getting Started

Follow these steps to get the application running locally.

### 1. Clone the Repository

If you haven't already, clone the project to your local machine.

### 2. Start the Database

This project uses Docker Compose to run a MongoDB instance. From the root directory of the project, run:

```sh
docker-compose up -d
```

This will start a MongoDB container in the background.

### 3. Configure the Application

Ensure your `appsettings.Development.json` file contains the correct connection string for the local MongoDB container:

```json
{
  "MongoDbSettings": {
    "ConnectionString": "mongodb://root:password@localhost:27017",
    "DatabaseName": "TrueSecDb",
    "VulnerabilitiesCollectionName": "Vulnerabilities"
  }
}
```

### 4. Run the Application

Navigate to the project directory (`/Users/niken/Documents/TrueSecProject/TrueSecProject`) and run the application:

```sh
dotnet run
```

The API will be available at `http://localhost:5245` or `https://localhost:5001`.

---

## API Endpoints & Examples

Here are some sample `curl` requests to interact with the API.

### 1. Create a Vulnerability

Creates a new vulnerability object.

**Request:**

```sh
curl -X POST "http://localhost:5245/vulnerabilities" \
-H "Content-Type: application/json" \
-d '{
  "type": "vulnerability",
  "id": "vulnerability--c3AdBcda-e4a7-2352-AeA7-84ebabba4ac5",
  "name": "string",
  "description": "string",
  "spec_version": "2.0",
  "created_by_ref": "identity--f7B51670-605a-4A04-959f-A8adfe617727",
  "labels": [
    "string"
  ],
  "created": "3199-09-30T01:55:00Z",
  "modified": "2490-11-05T17:12:59Z",
  "revoked": true,
  "confidence": 100,
  "lang": "en",
  "external_references": [
    {
      "source_name": "string",
      "hashes": {
        "sha-256": "5"
      },
      "description": "string",
      "url": "https://www.google.se",
      "external_id": "string"
    }
  ],
  "object_marking_refs": ["marking-definition--7038c0fe-3B8a-5234-bAd1-d1E1379D2fF5"],
  "granular_markings": [
    {
      "marking_ref": "marking-definition--7038c0fe-3B8a-5234-bAd1-d1E1379D2fF5",
      "selectors": [
        "string"
      ]
    }
  ]
}'
```

**Response:** A `201 Created` status with the newly created object.

### 2. Get All Vulnerabilities

Retrieves a list of all vulnerability objects stored in the database.

**Request:**

```sh
curl -X GET "http://localhost:5000/vulnerabilities"
```

**Response:** A `200 OK` status with an array of vulnerability objects.

### 3. Get a Vulnerability by ID

Retrieves a single vulnerability by its unique ID.

**Request:**

```sh
curl -X GET "http://localhost:5000/vulnerabilities/vulnerability--0c7b5b88-8ff7-4a4d-aa9d-feb398cd0061"
```

**Response:** A `200 OK` status with the requested vulnerability object, or `404 Not Found`.

### 4. Update a Vulnerability

Updates an existing vulnerability. The entire object must be provided in the request body.

**Request:**

```sh
curl -X PUT "http://localhost:5000/vulnerabilities/vulnerability--0c7b5b88-8ff7-4a4d-aa9d-feb398cd0061" \
-H "Content-Type: application/json" \
-d '{
  "id": "vulnerability--0c7b5b88-8ff7-4a4d-aa9d-feb398cd0061",
  "name": "Log4Shell (Updated)",
  "description": "A critical remote code execution (RCE) vulnerability in Apache Log4j 2, updated with more details.",
  "spec_version": "V2_1",
  "confidence": 95,
  "revoked": false
}'
```

**Response:** A `204 No Content` status on success, or `404 Not Found`.

### 5. Delete a Vulnerability

Deletes a vulnerability by its ID.

**Request:**

```sh
curl -X DELETE "http://localhost:5000/vulnerabilities/vulnerability--0c7b5b88-8ff7-4a4d-aa9d-feb398cd0061"
```

**Response:** A `204 No Content` status on success, or `404 Not