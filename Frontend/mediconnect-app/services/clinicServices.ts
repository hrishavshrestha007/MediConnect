import { API_URL } from "@/Libs/constants";

export interface Clinic {
  id: number;
  name: string;
  address: string;
  phoneNumber: string;
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