'use server';

import { API_URL } from "@/Libs/constants";
import { redirect } from "next/navigation";
import { revalidatePath } from "next/cache";

export async function bookAppointment(formData: FormData) {
    try {
        const appointmentDate = formData.get('appointmentDate') as string;
        const appointmentTime = formData.get('appointmentTime') as string;
        
        const dateTimeString = `${appointmentDate}T${appointmentTime}:00`;

        const body = {
            doctorId: parseInt(formData.get('doctorId') as string),
            appointmentDate: new Date(dateTimeString).toISOString(),
            durationMinutes: parseInt(formData.get('durationMinutes') as string),
            appointmentCategoryId: parseInt(formData.get('appointmentCategoryId') as string),
            firstName: formData.get('firstName'),
            lastName: formData.get('lastName'),
            email: formData.get('email'),
            birthdate: formData.get('birthdate'),
        };

        console.log('Booking appointment with data:', body);

        const response = await fetch(`${API_URL}/api/appointment/book`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(body),
        });

        if (!response.ok) {
            const errorData = await response.json();
            console.error('API Error:', errorData);
            throw new Error(errorData.message || `Failed to book appointment: ${response.statusText}`);
        }

        revalidatePath('/bookappointment');
        redirect('myappointments');
    } catch (error) {
        console.error('Booking error:', error);
        throw error;
    }
}