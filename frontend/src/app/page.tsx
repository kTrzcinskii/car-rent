"use client";

import axios from "axios";
import LoginDialogButton from "~/components/LoginDialogButton";
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
    <main className="flex min-h-screen flex-col items-center justify-center bg-gradient-to-b from-[#2e026d] to-[#15162c]">
      <div className="container flex flex-col items-center justify-center gap-12 px-4 py-16">
        <h1 className="text-5xl font-extrabold tracking-tight text-white sm:text-[5rem]">
          Car Rent Frontend demo
        </h1>
        <LoginDialogButton />
        <Button variant="outline" onClick={async () => await testApiCall()}>
          Test authorization (should only work for logged user with finished
          account)
        </Button>
      </div>
    </main>
  );
}
