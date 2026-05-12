'use server';

import { searchDoctors } from '@/services/doctorServices';

export async function handleDoctorSearch(formData: FormData) {
    const query = formData.get('query') as string;
    if (!query) {
        throw new Error('Search query is required');
    }

    const doctors = await searchDoctors(query);
    return doctors;
}   