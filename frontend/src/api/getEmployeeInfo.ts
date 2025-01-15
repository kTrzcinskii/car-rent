import axios from "axios";
import { EMPLOYEE_API_BASE_URL, EMPLOYEE_TOKEN_KEY } from "~/lib/consts";
import { type IEmployeeInfoResponse } from "../responses/IEmployeeInfoResponse";

export const getEmployeeInfo = async () => {
  const url = `${EMPLOYEE_API_BASE_URL}/account/info`;
  const token = sessionStorage.getItem(EMPLOYEE_TOKEN_KEY);
  const response = await axios.get<IEmployeeInfoResponse>(url, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });
  return response.data;
};
