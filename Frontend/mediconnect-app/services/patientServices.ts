import { API_URL } from "@/Libs/constants";

export interface PatientProfile {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  birthdate: string;
  gender: string;
  religion: string;
  socialSecurityNumber: string;
  taxNumber: string;
  driverLicenseNumber: string;
  medicalInsuranceMemberId: string;
}

export const getPatientProfile = async (patientId: number, token: string): Promise<PatientProfile> => {
  const response = await fetch(`${API_URL}/api/patients/${patientId}`, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  if (!response.ok) {
    throw new Error("Failed to fetch patient profile");
  }

  const data: unknown = await response.json();
  return data as PatientProfile;
};