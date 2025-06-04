# Lit Fibre Mock Appointment API

This project implements a mock ASP.NET Core Web API for managing appointments in the fibre and telecommunications industry. It supports standard CRUD operations for appointments and provides functionality to retrieve available future time slots, adhering to a defined OpenAPI Specification.

## Key Features

* **Appointment Management:** Create, retrieve, update, and delete appointments.
* **Slot Availability:** Generate and retrieve available 4-hour time slots for specific appointment types (weekdays, next 30 days).
* **Data Persistence:** Uses Entity Framework Core with SQL Server LocalDB.
* **API Documentation:** Interactive Swagger UI for easy testing and exploration.

## Technologies Used

* .NET 8
* ASP.NET Core Web API
* Entity Framework Core (SQL Server)
* Swagger/OpenAPI

## Getting Started

### Prerequisites

* .NET 8 SDK
* SQL Server LocalDB (usually installed with Visual Studio)

### Setup and Run

1.  **Clone the repository:**
    ```bash
    git clone [https://github.com/Saju-MA/LitFibre.git](https://github.com/Saju-MA/LitFibre.git)
    cd LitFibreApp # Navigate into the project folder
    ```
2.  **Restore NuGet packages:**
    ```bash
    dotnet restore
    ```
3.  **Apply Database Migrations:**
    ```bash
    dotnet ef database update
    ```
    *(Ensure your `appsettings.json` connection string is correctly configured for your SQL Server LocalDB.)*
4.  **Run the application:**
    ```bash
    dotnet run
    ```

### Access API Documentation (Swagger UI)

Once the application is running, open your browser and navigate to:
`https://localhost:XXXXX/swagger` (replace `XXXXX` with the port number shown in your console).

## OpenAPI Specification

The API's contract is defined by its OpenAPI Specification
