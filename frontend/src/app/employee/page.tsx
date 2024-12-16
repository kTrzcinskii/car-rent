"use client";

import { useQuery } from "@tanstack/react-query";
import { useRouter } from "next/navigation";
import {
  REACT_QUERY_EMPLOYEE_INFO_KEY,
  REACT_QUERY_EMPLOYEE_RENTS_INFO,
} from "~/lib/consts";
import { getEmployeeInfo } from "~/api/getEmployeeInfo";
import { getEmployeeRentsInfo } from "~/api/getEmployeeRentsInfo";

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

  return <div>rents count: {data?.data.length}</div>;
};

export default EmployeeDashboard;
