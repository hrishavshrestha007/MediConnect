import { cookies } from "next/headers";
import Link from "next/link";
import { getPatientProfile } from "@/services/patientServices";

export default async function ProfilePage() {
  const cookieStore = await cookies();
  const token = cookieStore.get("token")?.value;
  const patientId = cookieStore.get("patientId")?.value;

  if (!token || !patientId) {
    return (
      <main className="min-h-screen bg-[#f3eee6] flex items-center justify-center">
        <div className="text-center">
          <p className="text-[#5d554d] mb-4">Please log in to view your profile</p>
          <Link href="/login" className="text-[#d98a5a] hover:underline">
            Go to Login
          </Link>
        </div>
      </main>
    );
  }

  try {
    const patient = await getPatientProfile(parseInt(patientId), token);

    return (
      <main className="min-h-screen bg-[#f3eee6]">
        <div className="mx-auto max-w-2xl px-4 py-8">
          <h1 className="text-3xl font-bold text-[#5d554d] mb-8">My Profile</h1>

          <div className="rounded-lg border border-[#d8cec0] bg-white p-6 shadow-sm">
            <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
              <div>
                <p className="text-xs font-medium text-[#8b7d75]">FULL NAME</p>
                <p className="mt-1 text-sm text-[#5d554d]">{patient.firstName} {patient.lastName}</p>
              </div>
              <div className="md:col-span-2">
                <p className="text-xs font-medium text-[#8b7d75]">EMAIL</p>
                <p className="mt-1 text-sm text-[#5d554d]">{patient.email}</p>
              </div>

              <div>
                <p className="text-xs font-medium text-[#8b7d75]">BIRTHDATE</p>
                <p className="mt-1 text-sm text-[#5d554d]">
                  {new Date(patient.birthdate).toLocaleDateString()}
                </p>
              </div>
              <div>
                <p className="text-xs font-medium text-[#8b7d75]">GENDER</p>
                <p className="mt-1 text-sm text-[#5d554d]">{patient.gender}</p>
              </div>
              <div>
                <p className="text-xs font-medium text-[#8b7d75]">RELIGION</p>
                <p className="mt-1 text-sm text-[#5d554d]">{patient.religion || "N/A"}</p>
              </div>

              <div>
                <p className="text-xs font-medium text-[#8b7d75]">SOCIAL SECURITY NUMBER</p>
                <p className="mt-1 text-sm text-[#5d554d]">{patient.socialSecurityNumber}</p>
              </div>
              <div>
                <p className="text-xs font-medium text-[#8b7d75]">TAX NUMBER</p>
                <p className="mt-1 text-sm text-[#5d554d]">{patient.taxNumber}</p>
              </div>
              <div>
                <p className="text-xs font-medium text-[#8b7d75]">DRIVER LICENSE NUMBER</p>
                <p className="mt-1 text-sm text-[#5d554d]">{patient.driverLicenseNumber || "N/A"}</p>
              </div>
              <div>
                <p className="text-xs font-medium text-[#8b7d75]">MEDICAL INSURANCE ID</p>
                <p className="mt-1 text-sm text-[#5d554d]">{patient.medicalInsuranceMemberId || "N/A"}</p>
              </div>
            </div>

            <div className="mt-8 space-y-2 border-t border-[#d8cec0] pt-6">
              <Link
                href="/myappointments"
                className="block rounded-md bg-[#d98a5a] px-4 py-2 text-center text-sm font-semibold text-white hover:bg-[#c97948] transition-colors"
              >
                View Appointments
              </Link>
              <Link
                href="/"
                className="block rounded-md border border-[#d8cec0] px-4 py-2 text-center text-sm font-semibold text-[#5d554d] hover:bg-[#f9f7f3] transition-colors"
              >
                Back Home
              </Link>
            </div>
          </div>
        </div>
      </main>
    );
  } catch (error) {
    console.error("Profile error:", error);
    return (
      <main className="min-h-screen bg-[#f3eee6] flex items-center justify-center">
        <div className="text-center">
          <p className="text-red-600 mb-4">Failed to load profile</p>
          <Link href="/" className="text-[#d98a5a] hover:underline">
            Go Home
          </Link>
        </div>
      </main>
    );
  }
}