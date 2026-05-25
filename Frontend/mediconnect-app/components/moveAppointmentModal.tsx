'use client';

import { useState } from 'react';
import { moveAppointment } from "@/modules/appointments/actions";
import { toast } from "sonner";

interface MoveAppointmentModalProps {
  appointmentId: number;
  doctorName: string;
  clinicName: string;
  currentDate: string;
  durationMinutes: number;
}

export default function MoveAppointmentModal({
  appointmentId,
  doctorName,
  clinicName,
  currentDate,
  durationMinutes,
}: MoveAppointmentModalProps) {
  const [isOpen, setIsOpen] = useState(false);
  const [newDate, setNewDate] = useState('');
  const [newTime, setNewTime] = useState('');
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState('');

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
  e.preventDefault();
  
  if (!newDate || !newTime) {
    setError('Please select both date and time');
    return;
  }

  setIsLoading(true);
  try {
    const formData = new FormData();
    formData.append('appointmentId', appointmentId.toString());
    formData.append('newDate', newDate);
    formData.append('newTime', newTime);
    await moveAppointment(formData);
    toast.success('Appointment rescheduled successfully!', {
      description: 'Reloading your appointments...',
    });
    // Close modal after showing success
    setTimeout(() => {
      setIsOpen(false);
      setNewDate('');
      setNewTime('');
      setError('');
    }, 800);
  } catch (err: any) {
    if (err?.message?.includes('redirect') || err?.digest?.includes('NEXT_REDIRECT')) {
      toast.success('Appointment rescheduled successfully!', {
        description: 'Reloading your appointments...',
      });
      setTimeout(() => {
        setIsOpen(false);
        setNewDate('');
        setNewTime('');
        setError('');
      }, 800);
      return;
    }
    console.error('Error rescheduling appointment:', err);
    setError(err?.message || 'Failed to reschedule appointment. Please try again.');
  } finally {
    setIsLoading(false);
  }
};

  return (
    <>
      <button
        onClick={() => setIsOpen(true)}
        className="flex-1 rounded-md bg-[#4a90e2] px-3 py-2 text-xs font-semibold text-white hover:bg-[#357ab8] transition-colors"
      >
        Reschedule
      </button>

      {isOpen && (
        <div className="fixed inset-0 bg-black/50 flex items-center justify-center z-50 p-4">
          <div className="bg-white rounded-lg shadow-lg max-w-md w-full p-6">
            <h3 className="text-lg font-semibold text-[#5d554d] mb-4">Reschedule Appointment</h3>

            {error && (
              <div className="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded-lg mb-4">
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
                <span className="font-semibold">Current Date:</span> {new Date(currentDate).toLocaleDateString()}
              </p>
              <p className="text-sm text-[#5d554d]">
                <span className="font-semibold">Duration:</span> {durationMinutes} minutes
              </p>
            </div>

            <form onSubmit={handleSubmit} className="space-y-4">
              <div>
                <label className="block text-sm font-medium text-[#22201e] mb-2">New Date</label>
                <input 
                  type="date" 
                  value={newDate}
                  onChange={(e) => setNewDate(e.target.value)}
                  required
                  className="w-full px-3 py-2 border border-[#d8cec0] rounded-lg text-[#22201e] focus:outline-none focus:ring-2 focus:ring-[#8b7e6a]/20"
                />
              </div>

              <div>
                <label className="block text-sm font-medium text-[#22201e] mb-2">New Time</label>
                <input 
                  type="time" 
                  value={newTime}
                  onChange={(e) => setNewTime(e.target.value)}
                  required
                  className="w-full px-3 py-2 border border-[#d8cec0] rounded-lg text-[#22201e] focus:outline-none focus:ring-2 focus:ring-[#8b7e6a]/20"
                />
              </div>

              <div className="flex gap-3 pt-4">
                <button
                  type="button"
                  onClick={() => {
                    setIsOpen(false);
                    setNewDate('');
                    setNewTime('');
                    setError('');
                }}
                  className="flex-1 bg-gray-300 text-gray-800 py-2 rounded-md font-semibold hover:bg-gray-400 transition-colors"
                  disabled={isLoading}
                >
                  Cancel
                </button>
                <button
                  type="submit"
                  className="flex-1 bg-[#4a90e2] text-white py-2 rounded-md font-semibold hover:bg-[#357ab8] transition-colors disabled:opacity-50"
                  disabled={isLoading}
                >
                  {isLoading ? 'Rescheduling...' : 'Reschedule'}
                </button>
              </div>
            </form>
          </div>
        </div>
      )}
    </>
  );
}