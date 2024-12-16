"use client";

import { useQuery } from "@tanstack/react-query";
import { useRouter } from "next/navigation";
import {
  REACT_QUERY_EMPLOYEE_INFO_KEY,
  REACT_QUERY_EMPLOYEE_RENTS_INFO,
} from "~/lib/consts";
import { getEmployeeInfo } from "~/api/getEmployeeInfo";
import { getEmployeeRentsInfo } from "~/api/getEmployeeRentsInfo";
import EmployeeReturnCard from "~/components/EmployeeReturnCard";

const EmployeeDashboard = () => {
  const router = useRouter();

  const { isLoading: isEmployeeInfoLoading, isError: isEmployeeInfoError } =
    useQuery({
      queryKey: [REACT_QUERY_EMPLOYEE_INFO_KEY],
      queryFn: getEmployeeInfo,
    });

  const { data } = useQuery({
    queryKey: [REACT_QUERY_EMPLOYEE_RENTS_INFO],
    queryFn: getEmployeeRentsInfo,
  });

  if (isEmployeeInfoLoading) {
    return <div>loading employee info</div>;
  }

  if (isEmployeeInfoError) {
    router.push("/employee/login");
    return <></>;
  }

  return (
    <div className="container mx-auto px-4 py-8">
      <h1 className="my-6 text-4xl font-semibold">List of ongoing rents</h1>
      <div className="grid grid-cols-1 gap-6 md:grid-cols-2 lg:grid-cols-3">
        {data?.data.map((rent) => {
          return (
            <div key={rent.rentId} className="w-full">
              <EmployeeReturnCard {...rent} />
            </div>
          );
        })}
      </div>
    </div>
  );
};

export default EmployeeDashboard;
