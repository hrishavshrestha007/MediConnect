'use client';

import { useState } from 'react';
import { searchDoctors, type SearchDoctor } from "@/services/doctorServices";

export default function SearchDoctor() {
    const [doctors, setDoctors] = useState<SearchDoctor[]>([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState('');
    const [searched, setSearched] = useState(false);

    const handleSearch = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        const formData = new FormData(e.currentTarget);
        const searchTerm = formData.get('query') as string;
        
        if (!searchTerm.trim()) {
            setDoctors([]);
            setSearched(false);
            return;
        }

        setLoading(true);
        setError('');
        setSearched(true);
        
        try {
            const results = await searchDoctors(searchTerm);
            setDoctors(results);
        } catch (err) {
            setError(err instanceof Error ? err.message : 'An unexpected error occurred');
            setDoctors([]);
        } finally {
            setLoading(false);
        }
    };

    return (
        <div>
            <form onSubmit={handleSearch} className="mb-2">
                <div className="flex gap-2">
                <input
                    name="query"
                    type="text"
                    placeholder="Search doctors"
                    className=" mt-2 px-4 py-2 border border-[#d8cec0] rounded text-[#5d554d] placeholder-[#a89d95]"
                />
                <button 
                    type="submit" 
                    className="mt-2 px-4 py-2 bg-[#d98a5a] text-white rounded font-semibold hover:bg-[#c97948] transition-colors"
                >
                    Search
                </button>
                </div>
            </form>

            {error && <p className="text-red-600 mb-4">{error}</p>}
            {loading && <p className="text-[#5d554d] mb-4">Searching...</p>}

            {searched && !loading && doctors.length === 0 && !error && (
                <div className="flex flex-col items-center justify-center rounded-lg border border-[#d8cec0] bg-white p-12 text-center mb-8">
                    <p className="text-[#5d554d]">No doctors found. Try a different search.</p>
                </div>
            )}

            {doctors.length > 0 && (
                <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4 mb-8">
                    {doctors.map((doctor, index) => (
                        <div
                            key={`${doctor.fullName}-${index}`}
                            className="flex flex-col rounded-lg border border-[#d8cec0] bg-white p-6 shadow-sm hover:shadow-md transition-shadow"
                        >
                            <h3 className="text-lg font-semibold text-[#5d554d]">
                                {doctor.fullName}
                            </h3>
        
                            <div className="mt-4 space-y-3">
                                <div>
                                    <p className="text-xs font-medium text-[#8b7d75]">SPECIALTY</p>
                                    <p className="mt-1 text-sm text-[#5d554d]">{doctor.specialityName}</p>
                                </div>
            
                                <div>
                                    <p className="text-xs font-medium text-[#8b7d75]">CLINIC</p>
                                    <p className="mt-1 text-sm text-[#5d554d]">{doctor.clinicName}</p>
                                </div>
                            </div>

                            <a
                                href="/bookappointment"
                                className="mt-6 inline-block rounded-md bg-[#d98a5a] px-4 py-2 text-sm font-semibold text-white hover:bg-[#c97948] transition-colors"
                            >
                                Book Appointment
                            </a>
                        </div>
                    ))}
                </div>
            )}
        </div>
    );
}
