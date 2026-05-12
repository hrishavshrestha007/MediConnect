import { Category } from "@/services/categoryServices";

export default function CategoryDropdown({ categories }: { categories: Category[] | null | undefined }) {
    const categoryList = Array.isArray(categories) ? categories : [];
    
    return (
        <div>
            <label htmlFor="appointmentCategoryId" className="block text-sm font-medium text-[#22201e]">
                Appointment Category
            </label>
            <div className="mt-2 relative">
                <select
                    id="appointmentCategoryId"
                    name="appointmentCategoryId"
                    defaultValue=""
                    className="block w-full appearance-none rounded-xl border border-[#d8cec0] bg-white/60 py-3 px-4 pr-10 text-sm text-[#22201e] shadow-sm transition-colors focus:border-[#8b7e6a] focus:outline-none focus:ring-2 focus:ring-[#8b7e6a]/20"
                >
                    <option value="" disabled>
                        Select an appointment category
                    </option>
                    {categoryList.map((category) => (
                        <option key={category.id} value={category.id}>
                            {category.name}
                        </option>
                    ))}
                </select>
                <div className="pointer-events-none absolute inset-y-0 right-0 flex items-center px-3 text-[#5d554d]">
                    <svg className="h-4 w-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                        <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M19 9l-7 7-7-7" />
                    </svg>
                </div>
            </div>
        </div>
    );
}