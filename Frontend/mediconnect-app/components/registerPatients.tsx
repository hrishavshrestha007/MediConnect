import { registerPatient } from "@/modules/patients/actions";

export default  async function RegisterPage() {
    return (
        <section className="flex flex-1 items-center justify-center px-6 py-16">
            <div className="w-full max-w-2xl rounded-3xl border border-[#d8cec0]/70 bg-[#fffaf4]/80 p-8 shadow-[0_18px_50px_rgba(15,23,42,0.12)] backdrop-blur-xl">
                <h2 className="text-2xl font-bold text-[#5d554d]">Register</h2>
                <p className="mt-4 text-sm leading-6 text-[#5d554d]">Create an account to access your appointment history and manage your bookings.</p>
                <form action={registerPatient} className="mt-6 space-y-4">
                    {/* Basic Information */}
                    <div className="grid grid-cols-2 gap-4">
                        <input name="firstName" type="text" placeholder="First Name" required className="w-full rounded-md border border-[#d8cec0] bg-white px-3 py-2 text-sm text-[#5d554d] shadow-sm focus:outline-none focus:ring-2 focus:ring-[#d98a5a]/50" />
                        <input name="lastName" type="text" placeholder="Last Name" required className="w-full rounded-md border border-[#d8cec0] bg-white px-3 py-2 text-sm text-[#5d554d] shadow-sm focus:outline-none focus:ring-2 focus:ring-[#d98a5a]/50" />
                    </div>

                    <input name="email" type="email" placeholder="Email" required className="w-full rounded-md border border-[#d8cec0] bg-white px-3 py-2 text-sm text-[#5d554d] shadow-sm focus:outline-none focus:ring-2 focus:ring-[#d98a5a]/50" />
                    <input name="password" type="password" placeholder="Password" required className="w-full rounded-md border border-[#d8cec0] bg-white px-3 py-2 text-sm text-[#5d554d] shadow-sm focus:outline-none focus:ring-2 focus:ring-[#d98a5a]/50" />

                    <hr className="border-t border-[#d8cec0] my-4" />

                    {/* Personal Information */}
                    <div className="grid grid-cols-2 gap-4">
                        <input name="birthdate" type="date" placeholder="Birthdate" required className="w-full rounded-md border border-[#d8cec0] bg-white px-3 py-2 text-sm text-[#5d554d] shadow-sm focus:outline-none focus:ring-2 focus:ring-[#d98a5a]/50" />
                        <select name="gender" required className="w-full rounded-md border border-[#d8cec0] bg-white px-3 py-2 text-sm text-[#5d554d] shadow-sm focus:outline-none focus:ring-2 focus:ring-[#d98a5a]/50">
                            <option value="">Select Gender</option>
                            <option value="Male">Male</option>
                            <option value="Female">Female</option>
                            <option value="Other">Other</option>
                        </select>
                    </div>

                    <input name="religion" type="text" placeholder="Religion" className="w-full rounded-md border border-[#d8cec0] bg-white px-3 py-2 text-sm text-[#5d554d] shadow-sm focus:outline-none focus:ring-2 focus:ring-[#d98a5a]/50" />

                    <hr className="border-t border-[#d8cec0] my-4" />

                    {/* Official Documents */}
                    <div className="grid grid-cols-2 gap-4">
                        <input name="socialSecurityNumber" type="text" placeholder="Social Security Number" required className="w-full rounded-md border border-[#d8cec0] bg-white px-3 py-2 text-sm text-[#5d554d] shadow-sm focus:outline-none focus:ring-2 focus:ring-[#d98a5a]/50" />
                        <input name="taxNumber" type="text" placeholder="Tax Number" required className="w-full rounded-md border border-[#d8cec0] bg-white px-3 py-2 text-sm text-[#5d554d] shadow-sm focus:outline-none focus:ring-2 focus:ring-[#d98a5a]/50" />
                    </div>

                    <div className="grid grid-cols-2 gap-4">
                        <input name="driverLicenseNumber" type="text" placeholder="Driver License Number" className="w-full rounded-md border border-[#d8cec0] bg-white px-3 py-2 text-sm text-[#5d554d] shadow-sm focus:outline-none focus:ring-2 focus:ring-[#d98a5a]/50" />
                        <input name="medicalInsuranceMemberId" type="text" placeholder="Medical Insurance Member ID" className="w-full rounded-md border border-[#d8cec0] bg-white px-3 py-2 text-sm text-[#5d554d] shadow-sm focus:outline-none focus:ring-2 focus:ring-[#d98a5a]/50" />
                    </div>

                    <button type="submit" className="mt-6 w-full rounded-md bg-[#d98a5a] px-4 py-2 text-sm font-semibold text-white shadow-sm hover:bg-[#c97948] transition-colors">
                        Register
                    </button>
                </form>
            </div>
        </section>
    )
}