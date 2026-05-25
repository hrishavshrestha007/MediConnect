# Noroff
# Back-end Development Year 2
#### Backend.

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

## Setup & Installation


## Project Structure
ClinicWeb/
├── Controllers/              # API endpoints (Auth, Clinic, Doctor, Patient, Appointment)
├── Services/                 # Business logic layer
│   ├── Auth/
│   ├── Clinics/
│   ├── Doctors/
│   ├── Patients/
│   ├── Appointments/
│   ├── AppointmentCategories/
│   └── Shared/
├── Models/
│   ├── Entities/            # Database entity models
│   ├── DTOs/                # Data Transfer Objects
│   └── Config/              # Configuration models
├── Data/
│   ├── ClinicDbContext.cs   # Entity Framework DbContext
│   └── Migrations/          # Database migrations
├── Properties/
│   └── launchSettings.json  # Run configuration
├── appsettings.json         # Configuration file
├── appsettings.Development.json
├── Program.cs               # Application entry point
└── ClinicWeb.csproj
