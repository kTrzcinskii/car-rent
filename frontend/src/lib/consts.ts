import { env } from "~/env";

export const API_BASE_URL = `${env.NEXT_PUBLIC_API_URL}/api`;
export const TOKEN_KEY = "authToken";
export const REACT_QUERY_SEARCH_KEY = "search";
export const REACT_QUERY_USER_INFO_KEY = "userInfo";
export const REACT_QUERY_GET_RENTS_KEY = "getRents";
export const EMPLOYEE_API_BASE_URL = `${env.NEXT_PUBLIC_EMPLOYEE_API_URL}/api`;
export const EMPLOYEE_API_KEY = env.NEXT_PUBLIC_EMPLOYEE_API_KEY;
export const EMPLOYEE_TOKEN_KEY = "empoloyeeAuthToken";
export const REACT_QUERY_EMPLOYEE_INFO_KEY = "employeeInfo";
export const REACT_QUERY_EMPLOYEE_RENTS_INFO = "employeeRentsInfo";
export const REACT_QUERY_EMPLOYEE_RENT_DETAILS = "employeeRentDetails";
