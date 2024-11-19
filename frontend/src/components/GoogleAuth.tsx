"use client";

import React from "react";
import { GoogleLogin, type CredentialResponse } from "@react-oauth/google";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import axios from "axios";
import {
  API_BASE_URL,
  REACT_QUERY_USER_INFO_KEY,
  TOKEN_KEY,
} from "~/lib/consts";
import { useRouter } from "next/navigation";
import { type IAuthResponse } from "~/responses/IAuthResponse";

const GoogleAuth = () => {
  const router = useRouter();
  const queryClient = useQueryClient();

  const mutation = useMutation({
    mutationFn: async (googleToken: string) => {
      const url = `${API_BASE_URL}/Auth/google/callback`;
      const response = await axios.post<IAuthResponse>(url, googleToken, {
        headers: {
          "Content-Type": "application/json",
        },
      });
      return response.data;
    },
    onSuccess: async (data) => {
      sessionStorage.setItem(TOKEN_KEY, data.token);
      if (data.finishRegistration) {
        router.push("/complete-registration");
      } else {
        await queryClient.invalidateQueries({
          queryKey: [REACT_QUERY_USER_INFO_KEY],
        });
      }
    },
    onError: (error) => {
      console.error("Authentication failed:", error);
    },
  });

  const handleLoginSuccess = (credentialResponse: CredentialResponse) => {
    if (credentialResponse.credential) {
      mutation.mutate(credentialResponse.credential);
    }
  };

  const handleLoginFailure = () => {
    console.error("Google login failed");
  };

  return (
    <div>
      <GoogleLogin
        onSuccess={(cr) => handleLoginSuccess(cr)}
        onError={handleLoginFailure}
      />
    </div>
  );
};

export default GoogleAuth;
