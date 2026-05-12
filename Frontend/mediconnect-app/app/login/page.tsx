import { login } from '@/modules/auth/actions'

export default function LoginPage() {
  return (
    <section className="flex flex-1 items-center justify-center px-6 py-16">
      <div className="w-full max-w-md rounded-3xl border border-white/70 bg-white/80 p-8 shadow-[0_18px_50px_rgba(15,23,42,0.12)] backdrop-blur-xl">
        <h2 className="text-2xl font-bold text-[#5d554d]">Sign In</h2>
        <p className="mt-4 text-sm leading-6 text-[#5d554d]">Log in to access your appointments and manage your bookings.</p>
        
        <form action={login} className="mt-6 space-y-4">
          <input 
            name="email" 
            type="email" 
            placeholder="Email" 
            required 
            className="w-full rounded-md border border-[#d8cec0] bg-white px-3 py-2 text-sm text-[#5d554d] shadow-sm focus:outline-none focus:ring-2 focus:ring-[#d98a5a]/50" 
          />

          <input 
            name="password" 
            type="password" 
            placeholder="Password" 
            required 
            className="w-full rounded-md border border-[#d8cec0] bg-white px-3 py-2 text-sm text-[#5d554d] shadow-sm focus:outline-none focus:ring-2 focus:ring-[#d98a5a]/50" 
          />

          <div className="flex items-center justify-between text-sm">
            <label className="inline-flex items-center gap-2 text-[#5d554d]">
              <input
                type="checkbox"
                name="remember"
                className="h-4 w-4 rounded border-[#d8cec0] text-[#d98a5a] focus:ring-[#d98a5a]/50"
              />
              Remember me
            </label>
            <a href="#" className="font-medium text-[#d98a5a] hover:text-[#c97948] transition-colors">
              Forgot password?
            </a>
          </div>

          <button
            type="submit"
            className="mt-6 w-full rounded-md bg-[#d98a5a] px-4 py-2 text-sm font-semibold text-white shadow-sm hover:bg-[#c97948] transition-colors"
          >
            Sign In
          </button>
        </form>
      </div>
    </section>
  );
}