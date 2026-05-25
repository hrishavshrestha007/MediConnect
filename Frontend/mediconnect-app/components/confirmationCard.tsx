'use client';

import { ReactNode } from 'react'; // Importing ReactNode type for defining the type of the icon prop

interface ConfirmationCardProps {
  title: string;
  message?: string;
  details: {
    label: string;
    value: string;
  }[];
  primaryButton?: {
    label: string;
    onClick: () => void;
  };
  secondaryButton?: {
    label: string;
    onClick: () => void;
  };
  icon?: ReactNode;
}

export default function ConfirmationCard({
  title,
  message,
  details,
  primaryButton,
  secondaryButton,
  icon,
}: ConfirmationCardProps) {
  return (
    <div className="flex items-center justify-center min-h-screen bg-[#f3eee6]">
      <div className="w-full max-w-2xl rounded-3xl border border-white/70 bg-white/80 p-8 shadow-lg">
        <div className="text-center">
          {icon && <div className="text-5xl mb-4">{icon}</div>}
          
          <h2 className="text-2xl font-bold text-green-600 mb-2">{title}</h2>
          
          {message && (
            <p className="text-[#5d554d] mb-6">{message}</p>
          )}

          <div className="bg-[#f3eee6] rounded-lg p-6 mb-6 space-y-3 text-left">
            {details.map((detail, index) => (
              <p key={index} className="text-sm text-[#5d554d]">
                <span className="font-semibold">{detail.label}:</span> {detail.value}
              </p>
            ))}
          </div>

          <div className="flex gap-3">
            {secondaryButton && (
              <button
                onClick={secondaryButton.onClick}
                className="flex-1 bg-gray-300 text-gray-800 py-3 rounded-md font-semibold hover:bg-gray-400 transition-colors"
              >
                {secondaryButton.label}
              </button>
            )}
            
            {primaryButton && (
              <button
                onClick={primaryButton.onClick}
                className="flex-1 bg-[#d98a5a] text-white py-3 rounded-md font-semibold hover:bg-[#c97948] transition-colors"
              >
                {primaryButton.label}
              </button>
            )}
          </div>
        </div>
      </div>
    </div>
  );
}