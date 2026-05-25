![](http://images.restapi.co.za/pvt/Noroff-64.png)
# Noroff
# Back-end Development Year 2
#### This folder should be used for the front-end code.

---

## PAGES & FEATURES

### Authentication Pages

#### `/login`
- **Description**: User login page where existing users can sign in with their email and password.
- **Features**: 
  - Email and password input fields
  - JWT token storage upon successful login
  - Redirect to dashboard on successful authentication
  - Error handling for invalid credentials
- **Requires Authentication**: No

#### `/register`
- **Description**: Patient registration page where new users can create an account.
- **Features**:
  - Form for patient information (firstName, lastName, email, password, birthdate)
  - Email validation
  - Password confirmation
  - Account creation and automatic login
  - Link to login page for existing users
- **Requires Authentication**: No

#### `/profile`
- **Description**: User profile page displaying patient information and account details.
- **Features**:
  - View personal information (name, email, date of birth, etc.)
  - Edit profile information
  - View profile picture
  - Update contact details
- **Requires Authentication**: Yes

---

### Appointment Management Pages

#### `/bookappointment`
- **Description**: Main page for booking a new appointment.
- **Features**:
  - Clinic selection dropdown
  - Doctor selection (filtered by selected clinic)
  - Appointment category selection
  - Date and time picker
  - Duration selection (15, 30, 45, 60 minutes)
  - Optional notes section
  - Form submission with confirmation
  - Guest booking support (no login required)
- **Requires Authentication**: No (but supports both logged-in and guest users)

#### `/myappointments`
- **Description**: Dashboard showing all appointments booked by the logged-in user.
- **Features**:
  - Display list of upcoming appointments
  - Show past appointments
  - Display appointment details (doctor, clinic, date, time, category)
  - Cancel appointment button (with confirmation modal)
  - Reschedule button (redirect to reschedule page)
  - Appointment status indicator
  - Empty state message when no appointments exist
- **Requires Authentication**: Yes

#### `/reschedule`
- **Description**: Page for rescheduling an existing appointment.
- **Features**:
  - Select appointment to reschedule
  - Change appointment date
  - Change appointment time
  - Confirmation of new appointment details
  - Cancel reschedule option
- **Requires Authentication**: Yes

---

### Browse Pages

#### `/clinics`
- **Description**: Browse all available clinics.
- **Features**:
  - Display list of all clinics
  - Clinic name, address, and phone number
  - View doctors at each clinic
  - Link to book appointment at clinic
  - Clinic details view
- **Requires Authentication**: No

#### `/doctors`
- **Description**: Browse all available doctors.
- **Features**:
  - Display list of all doctors
  - Doctor name and speciality
  - Clinic where doctor works
  - Search functionality
  - Filter by speciality
  - Book appointment with doctor
- **Requires Authentication**: No

#### `/specialists`
- **Description**: Browse doctors by medical specialty.
- **Features**:
  - Display specialties/medical categories
  - List doctors under each specialty
  - Doctor details with clinic information
  - Book appointment option
  - Filter by clinic
- **Requires Authentication**: No

#### `/doctors/search`
- **Description**: Search for doctors by name, speciality, or clinic.
- **Features**:
  - Search input field
  - Real-time search results
  - Display matching doctors
  - Filter search results
  - Direct booking from search results
- **Requires Authentication**: No

---

### Core Pages

#### `/` (Home/Landing)
- **Description**: Homepage with navigation and quick links.
- **Features**:
  - Navigation menu
  - Call-to-action buttons (Book Appointment, View Doctors, etc.)
  - Quick search for doctors
  - Featured clinics/doctors
  - User authentication status indicator
  - Login/Logout navigation
- **Requires Authentication**: No

---

## Components

### Forms & Modals

#### `BookAppointmentForm`
- Patient information form for booking appointments
- Clinic, doctor, and category selection
- Date, time, and duration selection

#### `CancelConfirmationModal`
- Modal dialog confirming appointment cancellation
- Displays appointment details
- Cancel/Keep appointment buttons

#### `MoveAppointmentModal`
- Modal dialog for rescheduling appointments
- New date and time selection
- Confirmation buttons

#### `RegisterPatients`
- Patient registration form
- Input validation
- Password strength indicator

### Navigation & Display

#### `Header`
- Navigation bar with logo and menu
- User authentication status
- Login/Logout buttons
- Profile link for authenticated users

#### `MyAppointmentsCard`
- Card component displaying individual appointment
- Appointment details and actions
- Cancel/Reschedule buttons

#### `SearchDoctor`
- Search bar for finding doctors
- Real-time search functionality
- Search results display

#### `DoctorDropdown`
- Dropdown selection for choosing a doctor
- Filters based on selected clinic

#### `ClinicDropdown`
- Dropdown selection for choosing a clinic

#### `CategoryDropdown`
- Dropdown selection for appointment categories

---

## Features Summary

### User Management
- ✅ User registration and login
- ✅ Profile viewing and editing
- ✅ JWT token-based authentication

### Appointment Booking
- ✅ Book appointment as logged-in user or guest
- ✅ Select clinic, doctor, date, time, and category
- ✅ Appointment confirmation with details
- ✅ View all personal appointments

### Appointment Management
- ✅ View upcoming and past appointments
- ✅ Cancel appointments with confirmation
- ✅ Reschedule appointments to new date/time
- ✅ Appointment status tracking

### Search & Browse
- ✅ Search doctors by name, specialty, or clinic
- ✅ Browse all clinics
- ✅ Browse all doctors
- ✅ Browse by medical specialty
- ✅ Filter results

### User Experience
- ✅ Responsive design
- ✅ Form validation
- ✅ Error handling and user feedback
- ✅ Loading states
- ✅ Confirmation modals for critical actions
- ✅ Success/confirmation cards

---

## Tech Stack

- **Framework**: Next.js 16.2.5 (React 19)
- **Language**: TypeScript
- **Styling**: Tailwind CSS
- **Testing**: Vitest with React Testing Library
- **API Client**: Fetch API
- **Authentication**: JWT tokens
- **Form Handling**: Native HTML forms with React state

---

## Running the Frontend

```bash
# Install dependencies
npm install

# Run development server
npm run dev

# Run tests
npm run test

# Build for production
npm run build