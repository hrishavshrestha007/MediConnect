
import { getDoctors } from "@/services/doctorServices";
import Link from "next/link";
import SearchDoctor from "@/components/searchDoctor";



export default async function DoctorsPage() {
  const doctors = await getDoctors();

  return (
    <main className="min-h-screen w-full bg-[#f3eee6]">
      <div className="mx-auto w-full px-4 sm:px-6 lg:px-10">
        <section className="px-4 py-8 sm:px-6 lg:px-10">
          <div className="mb-8">
            <h1 className="text-3xl font-bold text-[#5d554d]">All Doctors</h1>
            <p className="mt-2 text-sm text-[#5d554d]">Meet our team of experienced healthcare professionals</p>
            <div className="mt-6">
              <SearchDoctor/>
            </div> 

          </div>

          {doctors.length > 0 ? (
            <div className="grid grid-cols-1 gap-6 md:grid-cols-2 lg:grid-cols-3">
              {doctors.map((doctor) => (
                <div
                  key={doctor.id}
                  className="flex flex-col rounded-lg border border-[#d8cec0] bg-white p-6 shadow-sm hover:shadow-md transition-shadow"
                >
                  
                  
                  <h3 className="text-lg font-semibold text-[#5d554d]">
                    {doctor.fullName}
                  </h3>
                  
                  <div className="mt-4 space-y-3">
                    <div>
                      <p className="text-xs font-medium text-[#8b7d75]">SPECIALTY</p>
                      <p className="mt-1 text-sm text-[#5d554d]">{doctor.specialityName}</p>
                    </div>
                    
                    <div>
                      <p className="text-xs font-medium text-[#8b7d75]">CLINIC</p>
                      <p className="mt-1 text-sm text-[#5d554d]">{doctor.clinicName}</p>
                    </div>
                  </div>

                  <Link
                    href="/bookappointment"
                    className="mt-6 inline-block rounded-md bg-[#d98a5a] px-4 py-2 text-sm font-semibold text-white hover:bg-[#c97948] transition-colors"
                  >
                    Book Appointment
                  </Link>
                </div>
              ))}
            </div>
          ) : (
            <div className="flex flex-col items-center justify-center rounded-lg border border-[#d8cec0] bg-white p-12 text-center">
              <p className="text-[#5d554d]">No doctors available</p>
            </div>
          )}
        </section>
      </div>
    </main>
  );
}