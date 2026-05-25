'use server'

import { cookies } from 'next/headers' // Importing the cookies function from Next.js to manage cookies on the server side
import { redirect } from 'next/navigation' // Importing the redirect function from Next.js to redirect users after login/logout actions
import { API_URL } from '@/Libs/constants' // Base URL for the API, imported from constants

//Function to handle user login, takes form data as input, sends a request to the API, and sets cookies on successful login
export async function login(formData: FormData) { 
    const email = formData.get('email')
    const password = formData.get('password')

    if (!email || !password) {
        throw new Error('Email and password are required')
    }

    const res = await fetch(`${API_URL}/api/auth/login`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ email, password }),
    })
    
    if (!res.ok) throw new Error('Invalid credentials')

    const { token, fullName, patientId } = await res.json()

    const cookieStore = await cookies()
    cookieStore.set('token', token, {
        httpOnly: true,
        path: '/',
    })

    cookieStore.set('email', email as string, {
        httpOnly: true,
        path: '/',
    })

    cookieStore.set('fullName', fullName as string, { 
        httpOnly: true,
        path: '/',
    })

    cookieStore.set('patientId', patientId.toString(), {
        httpOnly: true,
        path: '/',
    })

    redirect('/myappointments')
}
// Function to handle user logout, deletes the authentication cookies and redirects to the login page
export async function logout() {
    const cookieStore = await cookies()
    const token = cookieStore.get('token')?.value

    if (!token) throw new Error('No token found')

    const res = await fetch(`${API_URL}/api/auth/logout`, {
        method: 'POST',
        headers: { 
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        },
    })

    if (!res.ok) throw new Error('Logout failed')

    cookieStore.delete('token')
    cookieStore.delete('email')
    cookieStore.delete('fullName')
    cookieStore.delete('patientId')
    redirect('/login')
}