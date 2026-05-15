import { API_URL } from "@/Libs/constants";

export interface Doctor {
  id: number;
  firstName: string;
  lastName: string;
  specialityId: number;
  clinicId: number;
}

export interface Clinic {
  id: number;
  name: string;
  address: string;
  phoneNumber: string;
  doctors: Doctor[];  // Assuming the API returns doctors as part of the clinic data
}

export const getClinics = async (): Promise<Clinic[]> => {
  const response = await fetch(`${API_URL}/api/clinics`);
  if (!response.ok) {
    throw new Error("Failed to fetch clinics");
  }
  
  const data: unknown = await response.json();
    if (Array.isArray(data)) {
    return data as Clinic[];
    }
    return [];
};