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

const RentDetailsPage = ({ params }: { params: { "car-id": string } }) => {
  const carId = params["car-id"];

  const router = useRouter();

  const { isLoading: isEmployeeInfoLoading, isError: isEmployeeInfoError } =
    useQuery({
      queryKey: [REACT_QUERY_EMPLOYEE_INFO_KEY],
      queryFn: getEmployeeInfo,
    });

  const queryParams: IGetEmployeeRentDetailsProps = { carId: carId };

  const { data, isLoading, isError } = useQuery({
    queryKey: [REACT_QUERY_EMPLOYEE_RENT_DETAILS, queryParams],
    queryFn: ({ queryKey }) =>
      getEmployeeRentDetails(queryKey[1] as IGetEmployeeRentDetailsProps),
  });

  if (isEmployeeInfoLoading) {
    return <div>loading employee info</div>;
  }

  if (isEmployeeInfoError) {
    router.push("/employee/login");
    return <></>;
  }

  if (isLoading || !data) {
    return <div>loading message</div>;
  }

  if (isError) {
    return <div>error message</div>;
  }

  return (
    <div className="flex min-h-screen items-center justify-center bg-gray-50 p-4">
      <EmployeeRentDetails {...data} />
    </div>
  );
};

export default RentDetailsPage;
