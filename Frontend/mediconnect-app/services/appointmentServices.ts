import { API_URL } from '@/Libs/constants';

export interface Appointment {
  id: number;
  doctorName: string;
  appointmentDate: string;
  durationMinutes: number;
  status: string;
}

export const getMyAppointments = async (): Promise<Appointment[]> => {
  const response = await fetch(`${API_URL}/api/appointment/myappointments`, {
    headers: {
      'Authorization': `Bearer ${process.env.NEXT_PUBLIC_AUTH_TOKEN}`
    }
  });
  
  if (!response.ok) {
    throw new Error('Failed to fetch appointments');
  }
  
  return response.json();
};