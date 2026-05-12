import Link from "next/link";

export default function Home() {
  return (
    <main className="w-full bg-[#f3eee6] text-[#212121]">
      <div className="mx-auto w-full max-w-[1500px] px-4 sm:px-6 lg:px-10">
        <section className="flex items-center justify-center py-16">
          <div className="max-w-md text-center">
            <h1 className="font-serif text-5xl font-semibold leading-tight text-[#22201e]">Welcome to MediConnect</h1>
            <p className="mt-4 text-sm leading-6 text-[#5d554d]">
              We are a team of dedicated professionals who are passionate about helping you find the best care for your health.
            </p>
          </div>
        </section>

        <section className="flex items-center justify-center py-16 bg-[#d98a5a]">
          <div className="max-w-md text-center">
            <h1 className="font-serif text-5xl font-semibold leading-tight text-[#22201e]">Ready to get started?</h1>
            <p className="mt-4 text-sm leading-6 text-[#5d554d]">No account needed - book as a guest or register for full access to your appointment history.</p>
            <div className="mt-6 flex justify-center gap-4">
              <Link
                href="/bookappointment"
                className="inline-block rounded-md border border-[#d8cec0] bg-[#fffaf4] px-5 py-2 text-sm font-semibold text-[#5d554d] shadow-sm transition-all hover:bg-white"
              >
                Book Appointment
              </Link>
              <Link
                href="/doctors"
                className="inline-block rounded-md bg-[#d98a5a] px-5 py-2 text-sm font-semibold text-white shadow-sm transition-all hover:bg-[#c97948]"
              >
                Find a Doctor
              </Link>
            </div>
          </div>
        </section>
        <section className="flex items-center justify-center py-16">
          <div className="w-full">
            <div className="mb-12 text-center">
              <h1 className="font-serif text-5xl font-semibold leading-tight text-[#22201e]">Why Choose MediConnect</h1>
            </div>
            <div className="grid grid-cols-1 gap-8 sm:grid-cols-2 lg:grid-cols-4">
              <Link href="/clinics" className="rounded-lg border border-[#d8cec0] bg-white p-8 text-center shadow-sm transition-all hover:shadow-md hover:bg-[#f9f7f3] cursor-pointer">
                <div className="mb-4 text-5xl">🏥</div>
                <h3 className="font-serif text-xl font-semibold text-[#22201e]">3 Clinics</h3>
                <p className="mt-2 text-sm leading-6 text-[#5d554d]">Multiple convenient locations across the city</p>
              </Link>
              <Link href="/doctors" className="rounded-lg border border-[#d8cec0] bg-white p-8 text-center shadow-sm transition-all hover:shadow-md hover:bg-[#f9f7f3] cursor-pointer">
                <div className="mb-4 text-5xl">👨‍⚕️</div>
                <h3 className="font-serif text-xl font-semibold text-[#22201e]">9 Specialists</h3>
                <p className="mt-2 text-sm leading-6 text-[#5d554d]">Board-certified doctors across all specialties</p>
              </Link>
              <Link href="/book" className="rounded-lg border border-[#d8cec0] bg-white p-8 text-center shadow-sm transition-all hover:shadow-md hover:bg-[#f9f7f3] cursor-pointer">
                <div className="mb-4 text-5xl">📅</div>
                <h3 className="font-serif text-xl font-semibold text-[#22201e]">Easy Booking</h3>
                <p className="mt-2 text-sm leading-6 text-[#5d554d]">Book as a guest or registered patient in minutes</p>
              </Link>
              <Link href="/about" className="rounded-lg border border-[#d8cec0] bg-white p-8 text-center shadow-sm transition-all hover:shadow-md hover:bg-[#f9f7f3] cursor-pointer">
                <div className="mb-4 text-5xl">🔒</div>
                <h3 className="font-serif text-xl font-semibold text-[#22201e]">Secure & Private</h3>
                <p className="mt-2 text-sm leading-6 text-[#5d554d]">Your medical data is always safe with us</p>
              </Link>
            </div>
          </div>
        </section>
      </div>
    </main>
  );
}
