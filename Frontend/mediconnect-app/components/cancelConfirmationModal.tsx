'use client';

import { useState } from 'react';
import { cancelAppointment } from "@/modules/appointments/actions";

interface CancelConfirmationModalProps {
  appointmentId: number;
  doctorName: string;
  appointmentDate: string;
  clinicName: string;
}

export default function CancelConfirmationModal({
  appointmentId,
  doctorName,
  appointmentDate,
  clinicName,
}: CancelConfirmationModalProps) {
  const [isOpen, setIsOpen] = useState(false);
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState('');
  const [isSuccess, setIsSuccess] = useState(false);

  const handleCancel = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setError('');
    setIsLoading(true);

    try {
      const formData = new FormData();
      formData.append('appointmentId', appointmentId.toString());
      await cancelAppointment(formData);
      
      // Show success message
      setIsSuccess(true);
      
      // Close modal after 2 seconds
      setTimeout(() => {
        setIsOpen(false);
        setIsSuccess(false);
      }, 2000);
    } catch (err: any) {
      if (err?.message?.includes('redirect') || err?.digest?.includes('NEXT_REDIRECT')) {
        setIsSuccess(true);
        setTimeout(() => {
          setIsOpen(false);
          setIsSuccess(false);
        }, 2000);
        return;
      }
      setError(err?.message || 'Failed to cancel appointment');
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <>
      <button
        onClick={() => setIsOpen(true)}
        className="w-full rounded-md bg-red-500 px-3 py-2 text-xs font-semibold text-white hover:bg-red-600 transition-colors"
      >
        Cancel
      </button>

      {isOpen && (
        <div className="fixed inset-0 bg-black/50 flex items-center justify-center z-50 p-4">
          <div className="bg-white rounded-lg shadow-lg max-w-md w-full p-6">
            {isSuccess ? (
              <>
                <h3 className="text-lg font-semibold text-green-600 mb-4">✓ Appointment Cancelled</h3>
                <p className="text-sm text-[#5d554d]">Your appointment has been successfully cancelled.</p>
              </>
            ) : (
              <>
                <h3 className="text-lg font-semibold text-[#5d554d] mb-4">Cancel Appointment</h3>
                
                {error && (
                  <div className="mb-4 p-3 bg-red-100 border border-red-400 text-red-700 rounded">
                    {error}
                  </div>
                )}
                
                <div className="bg-[#f3eee6] rounded-lg p-4 mb-6 space-y-2">
                  <p className="text-sm text-[#5d554d]">
                    <span className="font-semibold">Doctor:</span> {doctorName}
                  </p>
                  <p className="text-sm text-[#5d554d]">
                    <span className="font-semibold">Clinic:</span> {clinicName}
                  </p>
                  <p className="text-sm text-[#5d554d]">
                    <span className="font-semibold">Date:</span> {new Date(appointmentDate).toLocaleDateString()}
                  </p>
                </div>

                <p className="text-sm text-[#5d554d] mb-6">Are you sure you want to cancel this appointment? This action cannot be undone.</p>

                <div className="flex gap-3">
                  <button
                    onClick={() => setIsOpen(false)}
                    className="flex-1 bg-gray-300 text-gray-800 py-2 rounded-md font-semibold hover:bg-gray-400 transition-colors disabled:opacity-50"
                    disabled={isLoading}
                  >
                    Keep Appointment
                  </button>
                  <form onSubmit={handleCancel} className="flex-1">
                    <input type="hidden" name="appointmentId" value={appointmentId} />
                    <button
                      type="submit"
                      disabled={isLoading}
                      className="w-full bg-red-500 text-white py-2 rounded-md font-semibold hover:bg-red-600 transition-colors disabled:opacity-50"
                    >
                      {isLoading ? 'Cancelling...' : 'Cancel Appointment'}
                    </button>
                  </form>
                </div>
              </>
            )}
          </div>
        </div>
      )}
    </>
  );
}