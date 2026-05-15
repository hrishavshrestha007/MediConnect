import { moveAppointment } from "@/modules/appointments/actions";
import Link from "next/link";

export default function MoveAppointmentPage({ params }: { params: { id: string } }) {
  return (
    <main className="min-h-screen w-full bg-[#f3eee6]">
      <div className="flex items-center justify-center py-8">
        <div className="w-full max-w-md rounded-3xl border border-white/70 bg-white/80 p-8 shadow-[0_18px_50px_rgba(15,23,42,0.12)] backdrop-blur-xl mx-4">
          <h2 className="text-2xl font-bold text-[#5d554d] mb-6">Reschedule Appointment</h2>
          
          <form action={moveAppointment} className="space-y-4">
            <input type="hidden" name="appointmentId" value={params.id} />
            
            <div>
              <label className="block text-sm font-medium text-[#22201e]">New Date</label>
              <input 
                type="date" 
                name="newDate"
                required
                className="w-full mt-2 px-3 py-2 border border-[#d8cec0] rounded-lg text-[#22201e] focus:outline-none focus:ring-2 focus:ring-[#8b7e6a]/20" 
              />
            </div>

            <div>
              <label className="block text-sm font-medium text-[#22201e]">New Time</label>
              <input 
                type="time" 
                name="newTime"
                required
                className="w-full mt-2 px-3 py-2 border border-[#d8cec0] rounded-lg text-[#22201e] focus:outline-none focus:ring-2 focus:ring-[#8b7e6a]/20" 
              />
            </div>

            <div className="flex gap-2 pt-4">
              <button 
                type="submit"
                className="flex-1 bg-[#d98a5a] text-white py-2 rounded-md font-semibold hover:bg-[#c97948] transition-colors"
              >
                Confirm
              </button>
              <Link 
                href="/myappointments"
                className="flex-1 bg-gray-400 text-white py-2 rounded-md font-semibold hover:bg-gray-500 transition-colors text-center"
              >
                Cancel
              </Link>
            </div>
          </form>
        </div>
      </div>
    </main>
  );
}