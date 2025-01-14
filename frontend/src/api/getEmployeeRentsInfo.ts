import axios from "axios";
import {
  EMPLOYEE_API_BASE_URL,
  EMPLOYEE_TOKEN_KEY,
  EMPLOYEE_API_KEY,
} from "~/lib/consts";
import { type IEmployeeRentsInfoListResponse } from "~/responses/IEmployeeRentsInfoListResponse";

export const getEmployeeRentsInfo = async () => {
  const url = `${EMPLOYEE_API_BASE_URL}/cars/worker`;
  const token = sessionStorage.getItem(EMPLOYEE_TOKEN_KEY);
  const response = await axios.get<IEmployeeRentsInfoListResponse>(url, {
    headers: {
      Authorization: `Bearer ${token}`,
      "x-api-key": EMPLOYEE_API_KEY,
    },
  });
  return response.data;
};
