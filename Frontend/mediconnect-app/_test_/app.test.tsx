import { render, screen } from "@testing-library/react";
import { describe, it, expect } from "vitest";  
import BookAppointmentFormClient from "@/components/BookAppointmentForm";
import CancelConfirmationModal from "@/components/cancelConfirmationModal";
import MoveAppointmentModal from "@/components/moveAppointmentModal";
import SearchDoctor from "@/components/searchDoctor";

const mockClinics = [
  {
    id: 1,
    name: "City Health Clinic",
    address: "123 Main St, Downtown",        
    phoneNumber: "+1 (555) 123-4567",        
    doctors: [
      { id: 1, firstName: "Alice", lastName: "Smith", specialityId: 1, clinicId: 1 }, 
      { id: 2, firstName: "Bob", lastName: "Johnson", specialityId: 2, clinicId: 1 },  
    ]
  }
];

const mockCategories = [
  { id: 1, name: "General Practice" },
  { id: 2, name: "Pediatrics" },
];

describe("BookAppointmentFormClient", () => {
  it("renders the form heading", () => {
    render(
      <BookAppointmentFormClient
        clinics={mockClinics}
        categories={mockCategories}
        isLoggedIn={true}
        firstName="John"
        lastName="Doe"
        email="john.doe@example.com"
      />
    )
    expect(screen.getByText("Book an Appointment")).toBeInTheDocument()
  })

  it("renders clinic dropdown with clinics", () => {
    render(
      <BookAppointmentFormClient
        clinics={mockClinics}
        categories={mockCategories}
        isLoggedIn={true}
        firstName="John"
        lastName="Doe"
        email="john.doe@example.com"
      />
    )
    expect(screen.getByText("City Health Clinic")).toBeInTheDocument()
  })

  it("renders appointment categories", () => {
    render(
      <BookAppointmentFormClient
        clinics={mockClinics}
        categories={mockCategories}
        isLoggedIn={true}
        firstName="John"
        lastName="Doe"
        email="john.doe@example.com"
      />
    )
    expect(screen.getByText("General Practice")).toBeInTheDocument()
    expect(screen.getByText("Pediatrics")).toBeInTheDocument()
  })


  it("hides patient information when logged in", () => {
    render(
      <BookAppointmentFormClient
        clinics={mockClinics}
        categories={mockCategories}
        isLoggedIn={true}
        firstName="John"
        lastName="Doe"
        email="john.doe@example.com"
      />
    )
    expect(screen.queryByText("Patient Information")).not.toBeInTheDocument()
  })

  it("renders book my appointment button", () => {
    render(
      <BookAppointmentFormClient
        clinics={mockClinics}
        categories={mockCategories}
        isLoggedIn={true}
        firstName="John"
        lastName="Doe"
        email="john.doe@example.com"
      />
    )
    const button = screen.getByText("Book Appointment")
    expect(button).toBeInTheDocument()
    expect(button).toHaveAttribute("type", "submit")
  })
})

describe("CancelConfirmationModal", () => {
  it("renders cancel button", () => {
    render(
      <CancelConfirmationModal
        appointmentId={1}
        doctorName="Alice Smith"
        clinicName="City Health Clinic"
        appointmentDate="2024-07-01T10:00:00Z"
      />
    )
    const button = screen.getByText("Cancel")
    expect(button).toBeInTheDocument()
  })
})

describe("MoveAppointmentModal", () => {
  it("renders reschedule button", () => {
    render(
      <MoveAppointmentModal
        appointmentId={1}
        doctorName="Alice Smith"
        clinicName="City Health Clinic"
        currentDate="2024-07-01T10:00:00Z"
        durationMinutes={30}
      />
    )
    const button = screen.getByText("Reschedule")
    expect(button).toBeInTheDocument()
  })
})

describe("SearchDoctor", () => {
  it("renders search input and button", () => {
    render(<SearchDoctor />)
    const input = screen.getByPlaceholderText("Search doctors")
    const button = screen.getByText("Search")
    expect(input).toBeInTheDocument()
    expect(button).toBeInTheDocument()
  })    
})