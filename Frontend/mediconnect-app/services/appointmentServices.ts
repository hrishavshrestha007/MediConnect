import { API_URL } from '@/Libs/constants';
import { cookies } from 'next/headers';

export interface Appointment {
  id: number;
  doctorName: string;
  clinicName: string;
  appointmentDate: string;
  durationMinutes: number;
  status: string;
}

export const getMyAppointments = async (): Promise<Appointment[]> => {
    const cookieStore = await cookies();
    const token = cookieStore.get('token')?.value;
    
    if (!token) {
        throw new Error('User is not authenticated');
    }

  const response = await fetch(`${API_URL}/api/appointment/myappointments`, {
    headers: {
      'Authorization': `Bearer ${token}`
    }
  });
  
  if (!response.ok) {
    throw new Error('Failed to fetch appointments');
  }
  
  return response.json();
};