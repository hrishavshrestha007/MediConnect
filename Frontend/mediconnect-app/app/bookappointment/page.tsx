
import { getClinics } from "@/services/clinicServices";
import { getCategories } from "@/services/categoryServices";
import { cookies } from "next/headers";
import BookAppointmentFormClient from "@/components/BookAppointmentForm";

export default async function BookAppointmentPage() {
  const clinics = await getClinics();
  const categories = await getCategories();

  const cookieStore = await cookies();
  const token = cookieStore.get('token')?.value;
  const email = cookieStore.get('email')?.value;
  const fullName = cookieStore.get('fullName')?.value;
  const isLoggedIn = !!token;

  // Parse fullName into firstName and lastName
  const [firstName, lastName] = fullName ? fullName.split(' ') : ['', ''];

  return (
    <BookAppointmentFormClient
      clinics={clinics}
      categories={categories}
      isLoggedIn={isLoggedIn}
      firstName={firstName}
      lastName={lastName}
      email={email || ''}
    />
  );
}