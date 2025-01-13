import axios from "axios";
import {
  EMPLOYEE_API_BASE_URL,
  EMPLOYEE_TOKEN_KEY,
  EMPLOYEE_API_KEY,
} from "~/lib/consts";
import { type IEmployeeRentDetailsResponse } from "../responses/IEmployeeRentDetailsResponse";

export interface IGetEmployeeRentDetailsProps {
  carId: string;
}

export const getEmployeeRentDetails = async (
  props: IGetEmployeeRentDetailsProps,
) => {
  const url = `${EMPLOYEE_API_BASE_URL}/cars/worker/details?carId=${props.carId}`;
  const token = sessionStorage.getItem(EMPLOYEE_TOKEN_KEY);
  const response = await axios.get<IEmployeeRentDetailsResponse>(url, {
    headers: {
      Authorization: `Bearer ${token}`,
      "x-api-key": EMPLOYEE_API_KEY,
    },
  });
  return response.data;
};
