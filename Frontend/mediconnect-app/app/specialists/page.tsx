
import { getDoctors } from "@/services/doctorServices";
import Link from "next/link";

export default async function SpecialistsPage() {
  const doctors = await getDoctors();

  // Group doctors by specialty
  const specialties = Array.from(
    new Set(doctors.map(doc => doc.specialityName))
  ).sort();

  const groupedBySpecialty = specialties.map(specialty => ({
    specialty,
    doctors: doctors.filter(doc => doc.specialityName === specialty)
  }));

  return (
    <main className="min-h-screen w-full bg-[#f3eee6]">
      <div className="mx-auto w-full max-w-[1200px] px-4 sm:px-6 lg:px-10 py-8">
        <h1 className="text-3xl font-bold text-[#5d554d] mb-8">Specialties</h1>

        <div className="space-y-8">
          {groupedBySpecialty.map((group) => (
            <div key={group.specialty}>
              <h2 className="text-lg font-semibold text-[#5d554d] mb-3">{group.specialty}</h2>
              <div className="grid grid-cols-1 gap-4 md:grid-cols-2 lg:grid-cols-3">
                {group.doctors.map((doctor) => (
                  <div
                    key={doctor.id}
                    className="rounded-lg border border-[#d8cec0] bg-white p-4 shadow-sm hover:shadow-md transition-shadow"
                  >
                    <h3 className="font-semibold text-[#5d554d]">{doctor.fullName}</h3>
                    <p className="text-xs text-[#8b7d75] mt-1">{doctor.clinicName}</p>
                    <Link
                      href="/bookappointment"
                      className="mt-3 inline-block rounded-md bg-[#d98a5a] px-3 py-1 text-xs font-semibold text-white hover:bg-[#c97948] transition-colors"
                    >
                      Book
                    </Link>
                  </div>
                ))}
              </div>
            </div>
          ))}
        </div>
      </div>
    </main>
  );
}