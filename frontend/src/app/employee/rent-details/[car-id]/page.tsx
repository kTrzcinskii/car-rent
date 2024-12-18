"use client";

import { useQuery } from "@tanstack/react-query";
import { useRouter } from "next/navigation";
import { getEmployeeInfo } from "~/api/getEmployeeInfo";
import {
  REACT_QUERY_EMPLOYEE_INFO_KEY,
  REACT_QUERY_EMPLOYEE_RENT_DETAILS,
} from "~/lib/consts";

import {
  type IGetEmployeeRentDetailsProps,
  getEmployeeRentDetails,
} from "~/api/getEmployeeRentDetails";
import EmployeeRentDetails from "~/components/EmployeeRentDetails";
import { Loader2 } from "lucide-react";
import ErrorAlert from "~/components/ErrorAlert";

const RentDetailsPage = ({ params }: { params: { "car-id": string } }) => {
  const carId = params["car-id"];

  const router = useRouter();

  const { isLoading: isEmployeeInfoLoading, isError: isEmployeeInfoError } =
    useQuery({
      queryKey: [REACT_QUERY_EMPLOYEE_INFO_KEY],
      queryFn: getEmployeeInfo,
      retry: false,
    });

  const queryParams: IGetEmployeeRentDetailsProps = { carId: carId };

  const { data, isLoading, isError } = useQuery({
    queryKey: [REACT_QUERY_EMPLOYEE_RENT_DETAILS, queryParams],
    queryFn: ({ queryKey }) =>
      getEmployeeRentDetails(queryKey[1] as IGetEmployeeRentDetailsProps),
  });

  if (isEmployeeInfoLoading) {
    return (
      <div className="flex w-full items-center justify-center py-5">
        <Loader2 className="animate-spin" />
      </div>
    );
  }

  if (isEmployeeInfoError) {
    router.push("/employee/login");
    return <></>;
  }

  if (isLoading || !data) {
    return (
      <div className="flex w-full items-center justify-center py-5">
        <Loader2 className="animate-spin" />
      </div>
    );
  }

  if (isError) {
    return (
      <div className="w-full">
        <ErrorAlert
          title="Cannot load data"
          message="There was a problem while loading data. Please try again later."
        />
      </div>
    );
  }

  return (
    <div className="flex min-h-screen flex-col items-center justify-center space-y-5 bg-gray-50 p-4">
      <EmployeeRentDetails {...data} />
    </div>
  );
};

export default RentDetailsPage;
