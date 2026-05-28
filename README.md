[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/cTkjnfB0)

![](http://images.restapi.co.za/pvt/Noroff-64.png)
# Noroff
# Back-end Development Year 2
### Exam Project 2 - MediConnect Clinic Booking System

---

## Project Overview

MediConnect is a full-stack clinic appointment booking system that allows patients to book appointments with doctors at various clinics. The application features user authentication, appointment management, rescheduling, and cancellation capabilities.

---

## Learning & Development Process

### No. 1 - Exception Handling & HTTP Status Codes
**What I Learned:**
Understanding how to properly handle exceptions and map them to appropriate HTTP status codes. Specifically, catching `InvalidOperationException` and returning HTTP 409 (Conflict) when invalid operations occur.

```csharp
catch (InvalidOperationException ex)
{
    return Conflict(ex.Message);
}
```

**Why This Matters:** Those types of pattern is important for providing meaningful error responses to the frontend, allowing better error handling and user feedback. It follows REST conventions for status codes.

---

### No. 2 - Form Data Handling in NextJS
**What I Learned:**
How to handle form submissions with `FormData` API in React, particularly for appointment cancellation. Using `e.preventDefault()` to prevent default form behavior and `FormData` to structure data for API requests.

```typescript
const handleCancel = async (e: React.FormEvent<HTMLFormElement>) => {
  e.preventDefault();
  const formData = new FormData();
  formData.append('appointmentId', appointmentId.toString());
  await cancelAppointment(formData);
};
```

**Challenge Faced:** Managing form state and understanding when to use FormData vs. JSON for API requests.

---

### No. 3 - Toast Notifications for Better UX
**What I Learned:**
User experience is crucial. I researched notification solutions and discovered **Sonner** from shadcn/ui (https://www.shadcn.io/ui/sonner), which provides elegant toast notifications for Next.js applications.

**Installation:**
```bash
npm i sonner
```

**Why This Matters:** Toast notifications provide immediate, non-intrusive feedback to users about the success or failure of their actions, significantly improving the user experience.

---

### No. 4 - Data Transfer Objects (DTOs) for API Security & Efficiency
**What I Learned:**
DTOs (Data Transfer Objects) are crucial for separating your internal database models from what you expose through APIs. Instead of exposing entire database entities, DTOs allow you to control exactly what data gets sent to the frontend.

**Why This Matters:** 
- **Security:** You don't accidentally expose sensitive fields (like internal IDs, passwords, or admin flags)
- **Flexibility:** Frontend needs can differ from database structure
- **Decoupling:** You can change database schema without breaking the API

**Example from MediConnect:**
Instead of sending the entire Doctor entity with all internal fields, I created focused DTOs:

```csharp
// DTO - Only expose what frontend needs
public class DoctorDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Speciality { get; set; }
    public string ClinicName { get; set; }
}
```

**Challenge Faced:** Initially, I was confused when to use DTOs vs. entities. I learned that:
- Use **entities** for database operations
- Use **DTOs** for API responses and requests
- Map between them using mapping logic

**Benefit Discovered:** Using DTOs made the codebase cleaner, the API responses more predictable, and the application more secure.

---

## Tech Stack

### Backend
- **Framework:** ASP.NET Core 8.0
- **Language:** C#
- **Database:** SQL Server with Entity Framework Core
- **Authentication:** JWT (JSON Web Tokens) with Bearer token authorization
- **API Documentation:** Swagger/OpenAPI
- **Pattern:** RESTful API with proper HTTP status codes

### Frontend
- **Framework:** Next.js
- **Language:** TypeScript
- **UI Components:** React with custom styling
- **Notifications:** Sonner (toast notifications)
- **HTTP Client:** Fetch API
- **Testing:** Vitest

---

## Project Structure

```
ep-2-hrishavshrestha007/
│
├── Backend/                              # ASP.NET Core Web API
│   └── ClinicWeb/
│       ├── Controllers/                  # API endpoints
│       ├── Services/                     # Business logic
│       ├── Models/                       # Entities and DTOs
│       ├── Data/                         # DbContext and Migrations
│       ├── Program.cs                    # Application entry point
│       └── appsettings.json              # Configuration
│
├── Frontend/                             # Next.js Application
│   └── mediconnect-app/
│       ├── app/                          # Next.js app router
│       ├── components/                   # Reusable React components
│       ├── services/                     # API integration
│       ├── Libs/                         # Utility functions
│       ├── modules/                      # Feature modules
│       └── public/                       # Static assets
│
└── README.md                             # Overall 
```

---

## API Endpoints

### Authentication
- `POST /api/auth/login` - User login
- `POST /api/auth/logout` - User logout

### Clinics
- `GET /api/clinics` - Get all clinics
- `GET /api/clinics/{id}` - Get clinic by ID
- `GET /api/clinics/{id}/doctors` - Get doctors at clinic

### Doctors
- `GET /api/doctors` - Get all doctors
- `GET /api/doctors/{id}` - Get doctor by ID
- `GET /api/doctors/search?term={searchTerm}` - Search doctors

### Patients
- `GET /api/patients` - Get all patients (requires auth)
- `GET /api/patients/{id}` - Get patient by ID (requires auth)
- `POST /api/patients` - Register new patient
- `PUT /api/patients/{id}` - Update patient (requires auth)

### Appointments
- `POST /api/appointment/book` - Book appointment
- `PUT /api/appointment/move` - Reschedule appointment (requires auth)
- `PUT /api/appointment/{id}/cancel` - Cancel appointment (requires auth)
- `GET /api/appointment/myappointments` - Get user's appointments (requires auth)

---

## Getting Started

### Prerequisites
- **.NET 8.0 SDK** or later
- **Next.js** and npm
- **SQL Server** (Docker)

### Backend Setup
```bash
cd Backend/ClinicWeb

# Install dependencies
dotnet restore

# Configure database connection in appsettings.Development.json
# Then apply migrations
dotnet ef database update

# Run the server
dotnet run
```
The API will be available at `https://localhost:5117` and Swagger docs at `https://localhost:5117/swagger`

### Frontend Setup
```bash
cd Frontend/mediconnect-app

# Install dependencies
npm install

# Configure environment variables in .env.local
# NEXT_PUBLIC_API_URL=http://localhost:5117

# Run development server
npm run dev
```
The app will be available at `http://localhost:3000`

---

## Key Features

✅ **User Authentication** - JWT-based login/logout  
✅ **Appointment Booking** - Book appointments with doctors at clinics  
✅ **Appointment Management** - View, reschedule, and cancel appointments  
✅ **Doctor Search** - Search doctors by name, specialty, or clinic  
✅ **Responsive UI** - Works on desktop and mobile devices  
✅ **Real-time Feedback** - Toast notifications for better user experience   
✅ **Error Handling** - Proper HTTP status codes and error messages  

---

## Project Management

I used **Trello** to organize and track the development progress throughout this exam project. The board was structured into weekly sprints, each containing specific tasks and milestones.

### Trello Board Structure

The project was organized into 5 main phases:

- **Week 1 (EPProject Week 1st)** - Foundation & Backend Planning
  - Understanding the Project requirements
  - Creating database entities
  - Configuring database connection
  - Designing API endpoints
  - Building service layer

- **Week 2 (EPProject Week 2nd)** - Backend Development, Authentication & Testing
  - Implementing Services and Endpoints
  - Implementing JWT authentication
  - Creating Swagger documentation
  - Testing endpoints via ClinicWeb.http on VS Code

- **Week 3 (EPProject Week 3rd)** - Frontend Development
  - Planning frontend architecture
  - Designing UI components
  - Implementing frontend features

- **Week 4 (EPProject Week 4rt)** - API Refinement
  - Fetching and refining API responses
  - Building additional services

- **Week 5 (EPProject Week 5th)** - Documentation & Final Polish
  - Writing comprehensive documentation
  - Final testing and bug fixes

### Trello Board Screenshots

**Week 1-2 Progress:**
![Trello Board - Early Stages](docs/Screenshot%202026-05-05%20at%2016.46.26.png)

**Final Progress:**
![Trello Board - Final Status](docs/Screenshot%202026-05-13%20at%2023.42.55.png)

### Why Trello?

Trello was chosen for this project because it:
- Provides a clear visual overview of project status
- Allows easy task tracking across multiple weeks
- Supports collaboration and progress monitoring
- Helps maintain focus on weekly milestones
- Makes it easy to prioritize and reorganize tasks

---

## Development Highlights

This project was built with focus on:
- Understanding error handling patterns and REST conventions
- Learning React form handling and state management
- Improving user experience through thoughtful UI feedback
- Implementing proper separation of concerns (Controllers → Services → Data)
- Using Entity Framework Core migrations for database management
- Building scalable API design with DTOs and proper validation

---

## Author
Hrishav Shrestha - Noroff School of Technology and Digital Media in Oslo

---

**For detailed backend documentation, see [Backend/Readme.md](Backend/Readme.md)**  
**For detailed frontend documentation, see [Frontend/Readme.md](Frontend/Readme.md)**


