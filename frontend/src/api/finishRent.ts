import axios from "axios";
import { API_BASE_URL, TOKEN_KEY } from "~/lib/consts";

export interface IFinishRentParams {
  rentId: number;
}

export const finishRent = async (params: IFinishRentParams) => {
  const url = `${API_BASE_URL}/rent/finish-rent?rentId=${params.rentId}`;
  const token = sessionStorage.getItem(TOKEN_KEY);
  await axios.put(url, null, {
    headers: { Authorization: `Bearer ${token}` },
  });
};
