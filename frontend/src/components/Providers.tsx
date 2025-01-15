"use client";

import { GoogleOAuthProvider } from "@react-oauth/google";
import { QueryClientProvider, QueryClient } from "@tanstack/react-query";
import { ReactQueryDevtools } from "@tanstack/react-query-devtools";
import { type ReactNode, useState } from "react";
import { Toaster } from "~/components/ui/toaster";

interface Props {
  children: ReactNode;
}

export function Providers({ children }: Props) {
  const [queryClient] = useState(() => new QueryClient());

  return (
    <GoogleOAuthProvider clientId="833683360479-7nbu84jo6r7ftc7acti78hcd1gj9i1rl.apps.googleusercontent.com">
      <QueryClientProvider client={queryClient}>
        {children}
        <Toaster />
        <ReactQueryDevtools initialIsOpen={false} />
      </QueryClientProvider>
    </GoogleOAuthProvider>
  );
}
