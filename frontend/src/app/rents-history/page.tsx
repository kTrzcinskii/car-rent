"use client";

import { REACT_QUERY_USER_INFO_KEY } from "~/lib/consts";
import { getUserInfo } from "~/api/getUserInfo";
import { useQuery } from "@tanstack/react-query";
import { Loader2 } from "lucide-react";
import { useRouter } from "next/navigation";
import { useToast } from "~/hooks/use-toast";
import { useEffect, useState } from "react";
import ErrorAlert from "~/components/ErrorAlert";
import RentsHistory from "~/components/RentsHistory";

const RentsHistoryPage = () => {
  const router = useRouter();
  const { toast } = useToast();
  const [isAccessDenied, setIsAccessDenied] = useState(false);

  const { data: userData, isLoading: isUserDataLoading } = useQuery({
    queryKey: [REACT_QUERY_USER_INFO_KEY],
    queryFn: getUserInfo,
  });

  useEffect(() => {
    if (!userData && !isUserDataLoading) {
      toast({
        title: "Unauthorized",
        description:
          "This page is only for authenticated users. You will be redirected to main page.",
        variant: "destructive",
      });
      setTimeout(() => {
        router.push("/");
      }, 1000);
      setIsAccessDenied(true);
    }
  }, [isUserDataLoading, userData, router, toast]);

  if (isUserDataLoading) {
    return (
      <div className="flex w-full items-center justify-center py-5">
        <Loader2 className="animate-spin" />
      </div>
    );
  }

  if (isAccessDenied) {
    return (
      <div className="mx-auto max-w-[350px] lg:max-w-[450px]">
        <ErrorAlert
          title="Access denied"
          message="This page is for authenticated users only."
        />
      </div>
    );
  }

  return <RentsHistory />;
};

export default RentsHistoryPage;
