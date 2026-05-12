import { getMyAppointments } from "@/services/appointmentServices";
import Link from "next/link";

export default async function MyAppointmentsPage() {
  try {
    const appointments = await getMyAppointments();

    return (
      <main className="min-h-screen w-full bg-[#f3eee6]">
        <div className="mx-auto w-full px-0">
          <section className="px-4 py-8 sm:px-6 lg:px-10">
            <div className="mb-8">
              <h1 className="text-3xl font-bold text-[#5d554d]">My Appointments</h1>
              <p className="mt-2 text-sm text-[#5d554d]">View and manage your scheduled appointments</p>
            </div>

            {appointments.length > 0 ? (
              <div className="grid grid-cols-1 gap-6 sm:grid-cols-2 lg:grid-cols-3">
                {appointments.map((appointment) => (
                  <div
                    key={appointment.id}
                    className="flex flex-col rounded-lg border border-[#d8cec0] bg-white p-6 shadow-sm"
                  >
                    <h3 className="text-lg font-semibold text-[#5d554d]">{appointment.doctorName}</h3>
                    <p className="mt-2 text-sm text-[#5d554d]">
                      {new Date(appointment.appointmentDate).toLocaleDateString()}
                    </p>
                    <p className="text-sm text-[#5d554d]">{appointment.durationMinutes} minutes</p>
                    <span className="mt-2 inline-block rounded-full bg-[#d98a5a]/20 px-3 py-1 text-xs font-medium text-[#d98a5a]">
                      {appointment.status}
                    </span>
                  </div>
                ))}
              </div>
            ) : (
              <div className="flex flex-col items-center justify-center rounded-lg border border-[#d8cec0] bg-white p-12 text-center">
                <p className="text-[#5d554d] mb-4">No appointments yet</p>
                <Link
                  href="/bookappointment"
                  className="rounded-md bg-[#d98a5a] px-6 py-2 text-sm font-semibold text-white shadow-sm transition-all hover:bg-[#c97948]"
                >
                  Book Your First Appointment
                </Link>
              </div>
            )}
          </section>
        </div>
      </main>
    );
  } catch (error) {
    console.error("Failed to load appointments:", error);
    return (
      <main className="min-h-screen w-full bg-[#f3eee6]">
        <div className="mx-auto w-full px-0">
          <section className="px-4 py-8 sm:px-6 lg:px-10">
            <p className="text-red-600">Failed to load appointments. Please try again later.</p>
          </section>
        </div>
      </main>
    );
  }
}