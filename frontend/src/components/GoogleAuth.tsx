"use client";

import React from "react";
import { GoogleLogin, type CredentialResponse } from "@react-oauth/google";
import { useMutation } from "@tanstack/react-query";
import axios from "axios";
import { API_BASE_URL } from "~/lib/consts";

interface IBackendResponse {
  Token: string;
  FinishRegistration: boolean;
}

const GoogleAuth = () => {
  const mutation = useMutation({
    mutationFn: async (googleToken: string) => {
      const url = `${API_BASE_URL}/Auth/google/callback`;
      const response = await axios.post<IBackendResponse>(url, googleToken, {
        headers: {
          Accept: "*/*",
          "Content-Type": "application/json",
        },
      });
      return response.data;
    },
    onSuccess: (data) => {
      console.log(data);
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
