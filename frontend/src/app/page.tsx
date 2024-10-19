"use client";

import { Button } from "~/components/ui/button";

export default function HomePage() {
  return (
    <main className="flex min-h-screen flex-col items-center justify-center bg-gradient-to-b from-[#2e026d] to-[#15162c]">
      <div className="container flex flex-col items-center justify-center gap-12 px-4 py-16">
        <h1 className="text-5xl font-extrabold tracking-tight text-white sm:text-[5rem]">
          Car Rent Frontend demo
        </h1>
        <Button variant="outline" onClick={() => alert("OnClick action")}>
          This is custom button from shadcn
        </Button>
      </div>
    </main>
  );
}
