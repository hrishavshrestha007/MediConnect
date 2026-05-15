'use server';

import { API_URL } from "@/Libs/constants";
import { redirect } from "next/navigation";
import { revalidatePath } from "next/cache";
import { cookies } from "next/headers";

export async function bookAppointment(formData: FormData) {
    try {
        const cookieStore = await cookies();
        const token = cookieStore.get('token')?.value;

        const appointmentDate = formData.get('appointmentDate') as string;
        const appointmentTime = formData.get('appointmentTime') as string;
        
        const dateTimeString = `${appointmentDate}T${appointmentTime}:00`;

        const body: any = {
            doctorId: parseInt(formData.get('doctorId') as string),
            appointmentDate: new Date(dateTimeString).toISOString(),
            durationMinutes: parseInt(formData.get('durationMinutes') as string),
            appointmentCategoryId: parseInt(formData.get('appointmentCategoryId') as string),
            firstName: formData.get('firstName'),
            lastName: formData.get('lastName'),
            email: formData.get('email'),
            birthdate: formData.get('birthdate'),
        };

        const headers: any = {
            'Content-Type': 'application/json',
        };

        // Send token if logged in
        if (token) {
            headers['Authorization'] = `Bearer ${token}`;
        }

        const response = await fetch(`${API_URL}/api/appointment/book`, {
            method: 'POST',
            headers,
            body: JSON.stringify(body),
        });

        if (!response.ok) {
            const errorData = await response.json();
            console.error('API Error:', errorData);
            throw new Error(errorData.message || `Failed to book appointment: ${response.statusText}`);
        }

        revalidatePath('/bookappointment');
        redirect('/myappointments');
    } catch (error) {
        console.error('Booking error:', error);
        throw error;
    }
}

export async function cancelAppointment(formData: FormData) {
    try {
        console.log('Canceling appointment with form data:');
        const cookieStore = await cookies();
        const token = cookieStore.get('token')?.value;
        const appointmentId = formData.get('appointmentId');

        if (!token) {
            throw new Error('User is not authenticated');
        }
        console.log(`Attempting to cancel appointment with ID: ${appointmentId}`);

        const response = await fetch(`${API_URL}/api/appointment/${appointmentId}/cancel`, {
            method: 'PUT',
            headers: {
                'Authorization': `Bearer ${token}`,
            },
        });

        if (!response.ok) {
            const errorData = await response.json();
            console.error('API Error:', errorData);
            throw new Error(errorData.message || `Failed to cancel appointment: ${response.statusText}`);
        }

        revalidatePath('/myappointments');
    } catch (error) {
        console.error('Cancellation error:', error);
        throw error;
    }
}

export async function moveAppointment(formData: FormData) {
    try {
        const cookieStore = await cookies();
        const token = cookieStore.get('token')?.value;
        const appointmentId = formData.get('appointmentId') as string;
        const newDate = formData.get('newDate') as string;
        const newTime = formData.get('newTime') as string;

        if (!token) {
            throw new Error('You must be logged in to move an appointment');
        }

        if (!newDate || !newTime) {
            throw new Error('Please select a new date and time');
        }

        const isoDateTime = `${newDate}T${newTime}:00`;

        const response = await fetch(`${API_URL}/api/appointment/move`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`,
            },
            body: JSON.stringify({
                appointmentId: parseInt(appointmentId),
                newDate: isoDateTime,
            }),
        });

        if (!response.ok) {
            const errorData = await response.json();
            throw new Error(errorData.message || 'Failed to move appointment');
        }

    } catch (error) {
        console.error('Move error:', error);
        throw error;
    }

    revalidatePath('/myappointments');
    redirect('/myappointments');
}