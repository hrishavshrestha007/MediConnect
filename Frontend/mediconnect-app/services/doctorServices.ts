import { API_URL } from '@/Libs/constants';

export interface Doctor {
  id: number;
  fullName: string;
  specialityName: string;
  clinicName: string;
  clinicId: number;
  specialityId: number;
}

export interface SearchDoctor {
  fullName: string;
  specialityName: string;
  clinicName: string;
}

export const getDoctors = async (): Promise<Doctor[]> => {
  const response = await fetch(`${API_URL}/api/doctors`);
  if (!response.ok) {
    throw new Error('Failed to fetch doctors');
  }
  
  const data: unknown = await response.json();
  
  if (Array.isArray(data)) {
    return data as Doctor[];
  }
  
  return [];
};

export const searchDoctors = async (query: string): Promise<SearchDoctor[]> => {
  const response = await fetch(`${API_URL}/api/doctors/search?term=${encodeURIComponent(query)}`);
  const data: unknown = await response.json();
  if (!response.ok) {
    if (data && typeof data === 'object' && 'message' in data) {
      throw new Error((data as { message: string }).message);
    }
    throw new Error(data as string || 'Failed to search doctors');
  }
  
  if (Array.isArray(data)) {
    return data as SearchDoctor[];
  }
  
  return [];
};

export const filterDoctors = async (specialityId?: number, clinicId?: number): Promise<Doctor[]> => {
  const params = new URLSearchParams();
  if (specialityId) params.append('specialityId', specialityId.toString());
  if (clinicId) params.append('clinicId', clinicId.toString());

  const response = await fetch(`${API_URL}/api/doctors/filter?${params.toString()}`);
  if (!response.ok) {
    throw new Error('Failed to filter doctors');
  }
  
  const data: unknown = await response.json();
  
  if (Array.isArray(data)) {
    return data as Doctor[];
  }
  
  return [];

  
};