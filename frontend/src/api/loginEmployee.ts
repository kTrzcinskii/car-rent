import axios from "axios";
import { EMPLOYEE_API_BASE_URL } from "~/lib/consts";
import { type IEmployeeAuthResponse } from "~/responses/IEmployeeAuthResponse";

export interface ILoginEmployeeProps {
  username: string;
  password: string;
}

export const loginEmployee = async (props: ILoginEmployeeProps) => {
  const url = `${EMPLOYEE_API_BASE_URL}/account/login`;
  const response = await axios.post<IEmployeeAuthResponse>(url, {
    UserName: props.username,
    Password: props.password,
  });
  return response.data;
};
