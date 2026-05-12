import { API_URL } from "@/Libs/constants";

export interface Category {
  id: number;
  name: string;
  defaultDurationMinutes?: number | null;
}

export const getCategories = async (): Promise<Category[]> => {
  const response = await fetch(`${API_URL}/api/appointmentcategories`);
  if (!response.ok) {
    throw new Error("Failed to fetch categories");
  }

  const data: unknown = await response.json();

  if (Array.isArray(data)) {
    return data as Category[];
  }

  return [];
};  