// This component is rendered while the add movie page is loading. It shows a skeleton UI to indicate that the content is being loaded.
export default function AddLoading() {
    return (
        <section className="flex flex-1 items-center justify-center px-6 py-16">
            <div className="w-full max-w-md rounded-3xl border border-white/70 bg-white/80 p-8 shadow-[0_18px_50px_rgba(15,23,42,0.12)] backdrop-blur-xl">
                <div className="h-4 w-24 animate-pulse rounded bg-zinc-300" />
                <div className="mt-3 h-8 w-32 animate-pulse rounded bg-zinc-300" />
                <div className="mt-2 h-4 w-full animate-pulse rounded bg-zinc-300" />

                <div className="mt-8 space-y-5">
                    {[...Array(5)].map((_, i) => (
                        <div key={i} className="space-y-2">
                            <div className="h-4 w-20 animate-pulse rounded bg-zinc-300" />
                            <div className="h-10 w-full animate-pulse rounded-xl bg-zinc-300" />
                        </div>
                    ))}
                    <div className="h-10 w-full animate-pulse rounded-xl bg-zinc-300" />
                </div>
            </div>
        </section>
    )
}