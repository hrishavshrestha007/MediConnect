'use client';

import { useState, useTransition } from 'react';
import { bookAppointment } from "@/modules/appointments/actions";
import { Clinic } from "@/services/clinicServices";
import CategoryDropdown from "./categoryDropdown";
import { toast } from "sonner";

interface Category {
  id: number;
  name: string;
}

export default function BookAppointmentFormClient({
  clinics,
  categories,
  isLoggedIn,
  firstName,
  lastName,
  email,
}: {
  clinics: Clinic[];
  categories: Category[];
  isLoggedIn: boolean;
  firstName: string;
  lastName: string;
  email: string;
}) {
  const [selectedClinicId, setSelectedClinicId] = useState<number | null>(null);
  const [isPending, startTransition] = useTransition();

  // Extract all doctors from clinics
  const allDoctors = clinics.flatMap(clinic => clinic.doctors);

  // Filter doctors by selected clinic
  const filteredDoctors = selectedClinicId
    ? allDoctors.filter(doc => doc.clinicId === selectedClinicId)
    : [];

  // Get doctor's full name
  const getDoctorFullName = (doctor: any) => `${doctor.firstName} ${doctor.lastName}`;

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
  e.preventDefault();
  
  const formData = new FormData(e.currentTarget);
  
  startTransition(async () => {
    try {
      await bookAppointment(formData);
      // Show success toast immediately
      toast.success('Appointment booked successfully!', {
        description: 'Redirecting to your appointments...',
      });
    } catch (error) {
      // Ignore redirect errors - they mean success
      if (error instanceof Error && error.message.includes('NEXT_REDIRECT')) {
        toast.success('Appointment booked successfully!', {
          description: 'Redirecting to your appointments...',
        });
        return;
      }
      toast.error('Failed to book appointment', {
        description: error instanceof Error ? error.message : 'Please try again',
      });
    }
  });
};

  return (
    <div className="flex items-center justify-center min-h-screen bg-[#f3eee6]">
      <div className="w-full max-w-2xl rounded-3xl border border-white/70 bg-white/80 p-8 shadow-[0_18px_50px_rgba(15,23,42,0.12)] backdrop-blur-xl mx-4">
        <h2 className="text-2xl font-bold text-[#5d554d] mb-6">Book an Appointment</h2>
        
        <form onSubmit={handleSubmit} className="space-y-6">
          <div>
            <label className="block text-sm font-medium text-[#22201e] mb-2">Clinic</label>
            <select
              name="clinicId"
              value={selectedClinicId || ''}
              onChange={(e) => setSelectedClinicId(Number(e.target.value))}
              required
              className="w-full px-3 py-2 border border-[#d8cec0] rounded-lg text-[#22201e] focus:outline-none focus:ring-2 focus:ring-[#8b7e6a]/20"
            >
              <option value="">Select a clinic</option>
              {clinics.map((clinic) => (
                <option key={clinic.id} value={clinic.id}>
                  {clinic.name}
                </option>
              ))}
            </select>
          </div>

          <div>
            <label className="block text-sm font-medium text-[#22201e] mb-2">Doctor</label>
            <select
              name="doctorId"
              disabled={!selectedClinicId || filteredDoctors.length === 0}
              required
              className="w-full px-3 py-2 border border-[#d8cec0] rounded-lg text-[#22201e] focus:outline-none focus:ring-2 focus:ring-[#8b7e6a]/20 disabled:opacity-50 disabled:cursor-not-allowed"
            >
              <option value="">
                {selectedClinicId ? (
                  filteredDoctors.length > 0 ? 'Select a doctor' : 'No doctors available'
                ) : (
                  'Select a clinic first'
                )}
              </option>
              {filteredDoctors.map((doctor) => (
                <option key={doctor.id} value={doctor.id}>
                  {getDoctorFullName(doctor)}
                </option>
              ))}
            </select>
          </div>

          <CategoryDropdown categories={categories} />
          
          <div className="grid grid-cols-2 gap-4">
            <div>
              <label className="block text-sm font-medium text-[#22201e]">Date</label>
              <input 
                type="date" 
                name="appointmentDate"
                required
                className="w-full mt-2 px-3 py-2 border border-[#d8cec0] rounded-lg text-[#22201e] focus:outline-none focus:ring-2 focus:ring-[#8b7e6a]/20" 
              />
            </div>
            <div>
              <label className="block text-sm font-medium text-[#22201e]">Time</label>
              <input 
                type="time" 
                name="appointmentTime"
                required
                className="w-full mt-2 px-3 py-2 border border-[#d8cec0] rounded-lg text-[#22201e] focus:outline-none focus:ring-2 focus:ring-[#8b7e6a]/20" 
              />
            </div>
          </div>

          <div>
            <label className="block text-sm font-medium text-[#22201e]">Duration (minutes)</label>
            <select 
              name="durationMinutes"
              defaultValue="30"
              className="w-full mt-2 px-3 py-2 border border-[#d8cec0] rounded-lg text-[#22201e] focus:outline-none focus:ring-2 focus:ring-[#8b7e6a]/20"
            >
              <option value="15">15 minutes</option>
              <option value="30">30 minutes</option>
              <option value="45">45 minutes</option>
              <option value="60">60 minutes</option>
            </select>
          </div>

          <div>
            <label className="block text-sm font-medium text-[#22201e]">Notes (optional)</label>
            <textarea 
              name="notes"
              className="w-full mt-2 px-3 py-2 border border-[#d8cec0] rounded-lg text-[#22201e] placeholder:text-[#a39e9a] focus:outline-none focus:ring-2 focus:ring-[#8b7e6a]/20" 
              placeholder="Add any additional notes..." 
              rows={4}
            />
          </div>

          {isLoggedIn && (
            <>
              <input type="hidden" name="firstName" value={firstName} />
              <input type="hidden" name="lastName" value={lastName} />
              <input type="hidden" name="email" value={email} />
            </>
          )}

          {!isLoggedIn && (
            <div className="border-t border-[#d8cec0] pt-6">
              <h3 className="text-lg font-semibold text-[#22201e] mb-4">Patient Information</h3>
              
              <div className="grid grid-cols-2 gap-4 mb-4">
                <div>
                  <label className="block text-sm font-medium text-[#22201e]">First Name</label>
                  <input 
                    type="text" 
                    name="firstName"
                    required
                    className="w-full mt-2 px-3 py-2 border border-[#d8cec0] rounded-lg text-[#22201e] focus:outline-none focus:ring-2 focus:ring-[#8b7e6a]/20" 
                    placeholder="John" 
                  />
                </div>
                <div>
                  <label className="block text-sm font-medium text-[#22201e]">Last Name</label>
                  <input 
                    type="text" 
                    name="lastName"
                    required
                    className="w-full mt-2 px-3 py-2 border border-[#d8cec0] rounded-lg text-[#22201e] focus:outline-none focus:ring-2 focus:ring-[#8b7e6a]/20" 
                    placeholder="Doe" 
                  />
                </div>
              </div>

              <div className="mb-4">
                <label className="block text-sm font-medium text-[#22201e]">Email</label>
                <input 
                  type="email" 
                  name="email"
                  required
                  className="w-full mt-2 px-3 py-2 border border-[#d8cec0] rounded-lg text-[#22201e] focus:outline-none focus:ring-2 focus:ring-[#8b7e6a]/20" 
                  placeholder="name@example.com" 
                />
              </div>

              <div className="mb-4">
                <label className="block text-sm font-medium text-[#22201e]">Date of Birth</label>
                <input 
                  type="date" 
                  name="birthdate"
                  required
                  className="w-full mt-2 px-3 py-2 border border-[#d8cec0] rounded-lg text-[#22201e] focus:outline-none focus:ring-2 focus:ring-[#8b7e6a]/20" 
                />
              </div>
            </div>
          )}

          <button 
            type="submit"
            disabled={isPending}
            className="w-full bg-[#d98a5a] text-white py-3 rounded-md font-semibold hover:bg-[#c97948] transition-colors mt-6 disabled:opacity-50 disabled:cursor-not-allowed"
          >
            {isPending ? 'Booking...' : 'Book Appointment'}
          </button>
        </form>
      </div>
    </div>
  );
}