import { API_URL } from "@/Libs/constants";

export interface Specialist {
  id: number;
  fullName: string;
  specialityName: string;
  clinicName: string;
  clinicId: number;
  specialityId: number;
}

export const getSpecialists = async (): Promise<Specialist[]> => {
  const response = await fetch(`${API_URL}/api/specialists`);
  if (!response.ok) {
    throw new Error("Failed to fetch specialists");
  }
  
  const data: unknown = await response.json();
    if (Array.isArray(data)) {
    return data as Specialist[];
    }
    return [];
};  
