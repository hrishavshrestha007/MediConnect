import Link from "next/link";
import { cookies } from "next/headers";
import Image from "next/image";
import { logout } from "@/modules/auth/actions"; // Import the logout action to handle user logout functionality

export default async function Header() {
  const cookieStore = await cookies();
  const token = cookieStore.get("token");
  const isLoggedIn = !!token;
  const fullName = cookieStore.get("fullName")?.value;

  return (
    <header className="w-full ">
      <nav className="flex w-full flex-wrap items-center justify-between gap-4 rounded-xl border border-[#d8cec0] bg-[#f3eee6]/90 px-3 py-2 sm:px-4">
        <Link
          href="/"
          className="inline-flex shrink-0 items-center rounded-md py-0.5 transition-opacity hover:opacity-90"
        >
          <Image
            src="/MC.png"
            alt="MediConnect"
            width={200}
            height={44}
            priority
            className="h-9 w-auto sm:h-10"
          />
        </Link>

        <div className="flex items-center justify-center gap-6 text-sm font-medium text-[#5d554d]">
          <Link href="/bookappointment" className="transition-colors hover:text-[#22201e]">
            Book Appointment
          </Link>
          <Link href="/doctors" className="transition-colors hover:text-[#22201e]">
            Find a Doctor
          </Link>
          {isLoggedIn && (
            <Link href="/myappointments" className="transition-colors hover:text-[#22201e]">
              My Appointments
            </Link>
          )}
        </div>

        <div className="flex shrink-0 items-center gap-2">
          {isLoggedIn ? (
            <>
              <Link
                href="/profile"
                className="max-w-[140px] truncate px-2 text-sm font-medium text-[#5d554d] sm:max-w-[200px]"
              >
                {fullName}
              </Link>
              <form action={logout} className="inline-block">
                <button
                  type="submit"
                  className="rounded-md bg-[#d98a5a] px-3 py-1.5 text-sm font-semibold text-white shadow-sm transition-all hover:bg-[#c97948]"
                >
                  Logout
                </button>
              </form>
            </>
          ) : (
            <>
              <Link
                href="/register"
                className="rounded-md border border-[#d8cec0] bg-[#fffaf4] px-3 py-1.5 text-sm font-semibold text-[#5d554d] shadow-sm transition-all hover:bg-white"
              >
                Register
              </Link>
              <Link
                href="/login"
                className="rounded-md bg-[#d98a5a] px-3 py-1.5 text-sm font-semibold text-white shadow-sm transition-all hover:bg-[#c97948]"
              >
                Login
              </Link>
            </>
          )}
        </div>
      </nav>
    </header>
  );
}