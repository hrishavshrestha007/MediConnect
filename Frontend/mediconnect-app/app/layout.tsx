import type { Metadata } from "next";
import { Geist, Geist_Mono } from "next/font/google";
import Header from "@/components/header";
import "./globals.css";

const geistSans = Geist({
  variable: "--font-geist-sans",
  subsets: ["latin"],
});

const geistMono = Geist_Mono({
  variable: "--font-geist-mono",
  subsets: ["latin"],
});

export const metadata: Metadata = {
  title: "MediConnect",
  description: "Get connected with healthcare",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  const year = new Date().getFullYear();

  return (
    <html
      lang="en"
      className={`${geistSans.variable} ${geistMono.variable} h-full w-full antialiased bg-[#f3eee6]`}
    >
      <body className="flex min-h-screen w-full flex-col overflow-x-hidden">
        <div className="w-full">
          <Header />
        </div>
        <div className="flex-1">{children}</div>
        <footer className="w-full border-t border-[#d8cec0] bg-[#f3eee6] py-4 text-center text-sm text-[#5d554d]">
          © {year} MediConnect. All rights reserved.
        </footer>
      </body>
    </html>
  );
}
