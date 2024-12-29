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
import { useToast } from "~/hooks/use-toast";
import { Loader2 } from "lucide-react";

const GoogleAuth = () => {
  const router = useRouter();
  const queryClient = useQueryClient();
  const { toast } = useToast();

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
        toast({
          title: "Finish your profile!",
          description:
            "It's your first time login - you will be redirected in few seconds to finish your profile information...",
          variant: "success",
        });
        setTimeout(() => {
          router.push("/complete-registration");
        }, 2000);
      } else {
        await queryClient.invalidateQueries({
          queryKey: [REACT_QUERY_USER_INFO_KEY],
        });
      }
    },
    onError: (error) => {
      toast({
        title: "Failed to login!",
        description: `An error occured while trying to login (${error}). Try again later.`,
        variant: "destructive",
      });
    },
  });

  const handleLoginSuccess = (credentialResponse: CredentialResponse) => {
    if (credentialResponse.credential) {
      mutation.mutate(credentialResponse.credential);
    }
  };

  const handleLoginFailure = () => {
    toast({
      title: "Failed during google oauth!",
      description: `An error occured while trying to login via google. Try again later.`,
      variant: "destructive",
    });
  };

  return (
    <div>
      <GoogleLogin
        onSuccess={(cr) => handleLoginSuccess(cr)}
        onError={handleLoginFailure}
      />
      {mutation.isPending && <Loader2 className="animate-spin" />}
    </div>
  );
};

export default GoogleAuth;
