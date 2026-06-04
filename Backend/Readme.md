# Backend Readme.

---

## ENDPOINTS

### Authentication Endpoints

#### `POST /api/auth/login`
- **Description**: Authenticate a user and receive a JWT token.
- **Request Body**: 
  - `email` (string, required): User's email address
  - `password` (string, required): User's password
- **Response**: JWT token with user information (email, fullName)
- **Status Codes**: 200 OK, 401 Unauthorized, 400 Bad Request

#### `POST /api/auth/logout`
- **Description**: Logout a user by invalidating their session.
- **Authorization**: Required (Bearer token)
- **Response**: Confirmation message
- **Status Codes**: 200 OK, 401 Unauthorized

---

### Clinic Endpoints

#### `GET /api/clinics`
- **Description**: Retrieve all clinics.
- **Response**: List of all clinics with their details
- **Status Codes**: 200 OK, 404 Not Found

#### `GET /api/clinics/{id}`
- **Description**: Retrieve a specific clinic by ID.
- **Parameters**: `id` (integer, required)
- **Response**: Clinic details
- **Status Codes**: 200 OK, 404 Not Found

#### `GET /api/clinics/{id}/doctors`
- **Description**: Retrieve all doctors associated with a specific clinic.
- **Parameters**: `id` (integer, required)
- **Response**: List of doctors at the clinic
- **Status Codes**: 200 OK, 404 Not Found

#### `POST /api/clinics`
- **Description**: Create a new clinic.
- **Request Body**: Clinic object with name, address, and phoneNumber
- **Response**: Created clinic with assigned ID
- **Status Codes**: 201 Created, 400 Bad Request, 409 Conflict

#### `PUT /api/clinics/{id}`
- **Description**: Update an existing clinic.
- **Parameters**: `id` (integer, required)
- **Request Body**: Updated clinic information
- **Response**: Updated clinic details
- **Status Codes**: 200 OK, 404 Not Found, 400 Bad Request

#### `DELETE /api/clinics/{id}`
- **Description**: Delete a clinic.
- **Parameters**: `id` (integer, required)
- **Response**: Confirmation message
- **Status Codes**: 204 No Content, 404 Not Found

---

### Doctor Endpoints

#### `GET /api/doctors`
- **Description**: Retrieve all doctors.
- **Response**: List of all doctors with their speciality and clinic information
- **Status Codes**: 200 OK, 404 Not Found

#### `GET /api/doctors/{id}`
- **Description**: Retrieve a specific doctor by ID.
- **Parameters**: `id` (integer, required)
- **Response**: Doctor details with speciality and clinic
- **Status Codes**: 200 OK, 404 Not Found

#### `GET /api/doctors/search?term={searchTerm}`
- **Description**: Search for doctors by name, speciality, or clinic.
- **Query Parameters**: `term` (string, required)
- **Response**: List of doctors matching the search criteria
- **Status Codes**: 200 OK, 404 Not Found, 400 Bad Request

#### `POST /api/doctors`
- **Description**: Create a new doctor.
- **Request Body**: Doctor object with firstName, lastName, specialityId, clinicId
- **Response**: Created doctor with assigned ID
- **Status Codes**: 201 Created, 400 Bad Request, 409 Conflict

#### `PUT /api/doctors/{id}`
- **Description**: Update an existing doctor.
- **Parameters**: `id` (integer, required)
- **Request Body**: Updated doctor information
- **Response**: Updated doctor details
- **Status Codes**: 200 OK, 404 Not Found, 400 Bad Request

#### `DELETE /api/doctors/{id}`
- **Description**: Delete a doctor.
- **Parameters**: `id` (integer, required)
- **Response**: Confirmation message
- **Status Codes**: 204 No Content, 404 Not Found

---

### Patient Endpoints

#### `GET /api/patients`
- **Description**: Retrieve all patients. Requires authentication.
- **Authorization**: Required (Bearer token)
- **Response**: List of all patients
- **Status Codes**: 200 OK, 404 Not Found, 401 Unauthorized

#### `GET /api/patients/{id}`
- **Description**: Retrieve a specific patient by ID. Users can only view their own profile.
- **Parameters**: `id` (integer, required)
- **Authorization**: Required (Bearer token)
- **Response**: Patient details (only accessible to the patient themselves or authorized staff)
- **Status Codes**: 200 OK, 404 Not Found, 401 Unauthorized, 403 Forbidden

#### `GET /api/patients/registered`
- **Description**: Retrieve all registered patients. Requires authentication.
- **Authorization**: Required (Bearer token)
- **Response**: List of registered patients
- **Status Codes**: 200 OK, 404 Not Found, 401 Unauthorized

#### `POST /api/patients`
- **Description**: Create a new patient (register a new patient).
- **Request Body**: Registration request with firstName, lastName, email, birthdate, etc.
- **Response**: Confirmation message with patient details
- **Status Codes**: 201 Created, 400 Bad Request, 409 Conflict

#### `PUT /api/patients/{id}`
- **Description**: Update an existing patient. Requires authentication.
- **Parameters**: `id` (integer, required)
- **Authorization**: Required (Bearer token)
- **Request Body**: Updated patient information
- **Response**: Updated patient details
- **Status Codes**: 200 OK, 404 Not Found, 400 Bad Request, 401 Unauthorized

#### `DELETE /api/patients/{id}`
- **Description**: Delete a patient. Requires authentication.
- **Parameters**: `id` (integer, required)
- **Authorization**: Required (Bearer token)
- **Response**: Confirmation message
- **Status Codes**: 204 No Content, 404 Not Found, 401 Unauthorized

---

### Appointment Endpoints

#### `POST /api/appointment/book`
- **Description**: Book a new appointment. Can be used by logged-in users or guest users.
- **Request Body**: 
  - `clinicId` (integer, required)
  - `doctorId` (integer, required)
  - `appointmentDate` (date, required)
  - `appointmentTime` (time, required)
  - `durationMinutes` (integer, required)
  - `appointmentCategoryId` (integer, required)
  - `notes` (string, optional)
  - For guests: `firstName`, `lastName`, `email`, `birthdate` (required)
- **Response**: Confirmation message "Appointment booked successfully"
- **Status Codes**: 201 Created, 400 Bad Request, 404 Not Found, 409 Conflict

#### `PUT /api/appointment/move`
- **Description**: Reschedule an existing appointment to a new date and time. Only logged-in patients can reschedule their own appointments.
- **Authorization**: Required (Bearer token)
- **Request Body**: 
  - `appointmentId` (integer, required)
  - `newDate` (date, required)
  - `newTime` (time, required)
- **Response**: Confirmation message with updated appointment details
- **Status Codes**: 200 OK, 404 Not Found, 400 Bad Request, 401 Unauthorized, 409 Conflict

#### `PUT /api/appointment/{id}/cancel`
- **Description**: Cancel an existing appointment. Only logged-in patients can cancel their own appointments.
- **Parameters**: `id` (integer, required - appointment ID)
- **Authorization**: Required (Bearer token)
- **Response**: Confirmation message "Appointment cancelled successfully"
- **Status Codes**: 200 OK, 404 Not Found, 401 Unauthorized, 409 Conflict

#### `GET /api/appointment/myappointments`
- **Description**: Retrieve all appointments for the logged-in user.
- **Authorization**: Required (Bearer token)
- **Response**: List of appointments belonging to the authenticated user
- **Status Codes**: 200 OK, 404 Not Found, 401 Unauthorized

---

### Appointment Category Endpoints

#### `GET /api/appointmentcategories`
- **Description**: Retrieve all appointment categories.
- **Response**: List of all appointment categories
- **Status Codes**: 200 OK, 404 Not Found, 500 Internal Server Error

#### `GET /api/appointmentcategories/{id}`
- **Description**: Retrieve a specific appointment category by ID.
- **Parameters**: `id` (integer, required)
- **Response**: Appointment category details
- **Status Codes**: 200 OK, 404 Not Found, 500 Internal Server Error

---

## Authentication

Most endpoints marked with **Authorization: Required** need a JWT token in the request header:

---

## Tech Stack 
- **Framework**: ASP.NET Core 8.0 (Web API)
- **Language**: C#
- **Database**: SQL Server with Entity Framework Core
- **ORM**: Entity Framework Core (for database operations and migrations)
- **Authentication**: JWT (JSON Web Tokens) with Bearer token authorization
- **API Documentation**: Swagger/OpenAPI
- **CORS**: Enabled for frontend requests
- **Password Hashing**: BCrypt or similar for secure password storage
- **Validation**: Data validation on API endpoints
- **API Design**: RESTful API with standard HTTP methods

---

### Backend Setup & Installation

#### Prerequisites
- **.NET 8.0 SDK** or later
- **SQL Server** (local or cloud instance)
- **Visual Studio Code** with C# extension

#### Installation Steps

1. **Create folder Backend**
   ```bash
   dotnet new webapi --use-controllers -n ClinicWeb

2. **Install dependencies**
   ```bash
   dotnet add package Microsoft.EntityFrameworkCore.SqlServer
   dotnet add package Microsoft.EntityFrameworkCore.Design
   dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer 
   ```

3. **Configure Database Connection**
   - Update `appsettings.Development.json` with your SQL Server connection string:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=your-server;Database=ClinicDb;Trusted_Connection=true;"
   }
   ```

4. **Apply Database Migrations**
   ```bash
   dotnet ef migrations add IntialCreate
   dotnet ef database update
   ```

5. **Run the Application**
   ```bash
   dotnet run
   ```
   The API will be available at `https://localhost:5001` or `http://localhost:5000`

6. **Access Swagger Documentation**
   - Navigate to `https://localhost:5117/swagger` to view API documentation and test endpoints

#### Environment Configuration
- **JWT_SECRET**: Secret key for token generation (configured in `appsettings.json`)
- **Database Connection**: SQL Server connection string
- **CORS Origin**: Frontend URL (typically `http://localhost:3000` for development)

---

## Project Structure

```
ClinicWeb/
│
├── Controllers/                          # API endpoints
│   ├── AuthController.cs                 # Authentication endpoints
│   ├── ClinicController.cs               # Clinic management
│   ├── DoctorController.cs               # Doctor management
│   ├── PatientController.cs              # Patient management
│   └── AppointmentController.cs          # Appointment management
│
├── Services/                             # Business logic layer
│   ├── Auth/                             # Authentication logic
│   ├── Clinics/                          # Clinic operations
│   ├── Doctors/                          # Doctor operations
│   ├── Patients/                         # Patient operations
│   ├── Appointments/                     # Appointment operations
│   ├── AppointmentCategories/            # Category operations
│   └── Shared/                           # Shared utilities
│
├── Models/
│   ├── Entities/                         # Database entity models
│   ├── DTOs/                             # Data Transfer Objects
│   │   └── Auth/                         # Auth DTOs
│   └── Config/                           # Configuration models
│
├── Data/
│   ├── ClinicDbContext.cs                # Entity Framework DbContext
│   └── Migrations/                       # Database migrations
│
├── Properties/
│   └── launchSettings.json               # Run configuration
│
├── appsettings.json                      # Configuration file
├── appsettings.Development.json          # Development configuration
├── Program.cs                            # Application entry point
└── ClinicWeb.csproj                      # Project file
```

### Key Folder Descriptions

- **Controllers/**: Contains API endpoint controllers that handle HTTP requests and responses for authentication, clinic management, doctor management, patient management, and appointments.

- **Services/**: Contains the business logic layer that processes data, handles validation, and manages operations. Organized by domain (Auth, Clinics, Doctors, Patients, Appointments, AppointmentCategories, and Shared utilities).

- **Models/**: Contains all data models including:
  - **Entities/**: Database entity models that represent tables in SQL Server
  - **DTOs/**: Data Transfer Objects used to transfer data between API layers
  - **Config/**: Configuration models for application settings

- **Data/**: Contains Entity Framework Core configuration:
  - **ClinicDbContext.cs**: The database context that manages database connections and entity mappings

- **Migrations/**: Database migration files that track schema changes over time

- **Properties/**: Contains project configuration files like launch settings for different run profiles

- **Root files**: 
  - **Program.cs**: Application entry point where the web host is configured
  - **appsettings.json**: Global configuration file
  - **appsettings.Development.json**: Development-specific configuration (database connection, JWT secret, etc.)
  - **ClinicWeb.csproj**: Project file defining dependencies and build configuration

