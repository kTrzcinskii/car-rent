import axios from "axios";
import { API_BASE_URL, TOKEN_KEY } from "~/lib/consts";
import { type IUserInfoResponse } from "~/responses/IUserInfoResponse";

export const getUserInfo = async () => {
  const url = `${API_BASE_URL}/user/my-info`;
  const token = sessionStorage.getItem(TOKEN_KEY);
  const response = await axios.get<IUserInfoResponse>(url, {
    headers: {
      Authorization: `Bearer ${token}`,
      Accept: "application/json",
    },
  });
  return response.data;
};
