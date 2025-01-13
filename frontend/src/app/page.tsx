"use client";

import Link from "next/link";
import { Button } from "~/components/ui/button";

export default function HomePage() {
  return (
    <main className="flex min-h-[calc(100vh-66px)] flex-col items-center justify-center">
      <div className="container flex flex-col items-center justify-center gap-12 px-4 py-16">
        <div>
          <h1 className="text-4xl font-extrabold tracking-tight md:text-7xl">
            Car Rent Browser
          </h1>
          <h4 className="text-xl tracking-tight md:text-3xl">
            Find your perfect car with us!
          </h4>
        </div>
        <Link href="/browse">
          <Button data-testid="browse-cars-button">Browse cars</Button>
        </Link>
      </div>
    </main>
  );
}
