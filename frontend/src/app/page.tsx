"use client";

import axios from "axios";
import { Button } from "~/components/ui/button";
import { API_BASE_URL, TOKEN_KEY } from "~/lib/consts";

export default function HomePage() {
  // TODO: remove it
  const testApiCall = async () => {
    const url = `${API_BASE_URL}/Test/auth`;
    try {
      const token = localStorage.getItem(TOKEN_KEY);
      const response = await axios.get(url, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });
      alert(response.data);
      console.log(response.data);
    } catch (error) {
      alert(error);
    }
  };

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
        <Button onClick={async () => await testApiCall()}>Browse cars</Button>
      </div>
    </main>
  );
}
