'use server'

import { API_URL } from '@/Libs/constants'
import { redirect } from 'next/navigation'
import { revalidatePath } from 'next/cache'

export async function registerPatient(formData: FormData) {

    const patient = {
        firstName: formData.get('firstName'),
        lastName: formData.get('lastName'),
        email: formData.get('email'),
        password: formData.get('password'),
        socialSecurityNumber: formData.get('socialSecurityNumber'),
        birthdate: formData.get('birthdate'),
        gender: formData.get('gender'),
        taxNumber: formData.get('taxNumber'),
        religion: formData.get('religion'),
        driverLicenseNumber: formData.get('driverLicenseNumber'),
        medicalInsuranceMemberId: formData.get('medicalInsuranceMemberId'),
    }

    const response = await fetch(`${API_URL}/api/patients`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(patient),
    })

    if (!response.ok) {
        throw new Error('Failed to register')
    }

    revalidatePath('/patients')
    redirect('/login')
}