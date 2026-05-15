import { getClinics } from "@/services/clinicServices";
import Link from "next/link";

export default async function ClinicsPage() {
  const clinics = await getClinics();

  return (
    <main className="min-h-screen w-full bg-[#f3eee6]">
      <div className="mx-auto w-full px-4 sm:px-6 lg:px-10">
        <section className="px-4 py-8 sm:px-6 lg:px-10">
          <div className="mb-8">
            <h1 className="text-3xl font-bold text-[#5d554d]">Our Clinics</h1>
            <p className="mt-2 text-sm text-[#5d554d]">Find the clinic nearest to you</p>
          </div>

          {clinics.length > 0 ? (
            <div className="grid grid-cols-1 gap-6 md:grid-cols-2 lg:grid-cols-3">
              {clinics.map((clinic) => (
                <div
                  key={clinic.id}
                  className="flex flex-col rounded-lg border border-[#d8cec0] bg-white p-6 shadow-sm hover:shadow-md transition-shadow"
                >
                  <h2 className="text-lg font-semibold text-[#5d554d]">{clinic.name}</h2>
                  
                  <div className="mt-4 space-y-3">
                    <div>
                      <p className="text-xs font-medium text-[#8b7d75]">ADDRESS</p>
                      <p className="mt-1 text-sm text-[#5d554d]">{clinic.address}</p>
                    </div>
                    
                    <div>
                      <p className="text-xs font-medium text-[#8b7d75]">PHONE</p>
                      <p className="mt-1 text-sm text-[#5d554d]">{clinic.phoneNumber}</p>
                    </div>
                    
                    <div>
                      <p className="text-xs font-medium text-[#8b7d75]">DOCTORS</p>
                      <p className="mt-1 text-sm text-[#5d554d]">
                        {clinic.doctors.length} specialist{clinic.doctors.length !== 1 ? 's' : ''}
                      </p>
                      <ul className="mt-2 space-y-1">
                        {clinic.doctors.map((doctor) => (
                          <li key={doctor.id} className="text-xs text-[#8b7d75]">
                            • Dr. {doctor.firstName} {doctor.lastName}
                          </li>
                        ))}
                      </ul>
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
              <p className="text-[#5d554d]">No clinics available</p>
            </div>
          )}
        </section>
      </div>
    </main>
  );
}