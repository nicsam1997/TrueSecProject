# TrueSec Vulnerability API

A REST API for managing STIX 2.1 Vulnerability objects, built with ASP.NET Core and MongoDB.

This project provides a set of endpoints to perform CRUD (Create, Read, Update, Delete) operations on the vulnerability data.

## Prerequisites

-   [.NET 9 SDK](https://dotnet.microsoft.com/download) (or a compatible version)
-   [Docker](https://www.docker.com/products/docker-desktop/) and Docker Compose

## Getting Started

Follow these steps to get the application running locally.

### 1. Clone the Repository

If you haven't already, clone the project to your local machine.

### 2. Start the Database

This project uses Docker Compose to run a local MongoDB instance. From the root directory of the project, run:

```sh
docker-compose up -d
```

### 3. Run the tests

From the root directory run:

```sh
dotnet test
```


### 4. Run the Application

From the root directory run:

```sh
dotnet run --project src/TrueSecProject.csproj --launch-profile http
```

### 5. Try the API

1.  **Open the Swagger UI** by navigating to `http://localhost:5000/swagger/index.html` in your browser.

2.  **Get an authentication token (JWT):**
    *   Expand the `/api/auth/login` endpoint.
    *   Click "Try it out" and provide the following credentials in the request body:
        ```json
        {
          "username": "admin",
          "password": "AdminPassword123!"
        }
        ```
    *   Execute the request and copy the `token` from the response body.

3.  **Authorize your session:**
    *   Click the **Authorize** button at the top right of the page.
    *   In the "Value" field, paste your token, prefixed with `Bearer ` (e.g., `Bearer eyJhbGciOi...`).
    *   Click "Authorize" to close the dialog.

You can now use the other API endpoints that require authentication.

### 6. Some notes on validating the vulnerability
We are validating against the Stix 2.1 specification, defined in https://docs.oasis-open.org/cti/stix/v2.1/os/stix-v2.1-os.pdf. We do not allow extensions or custom hashes,
and we do not validate that the selectors in granular_markings really refer to a
field that exists on the object