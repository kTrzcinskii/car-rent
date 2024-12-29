import { API_BASE_URL, TOKEN_KEY } from "../lib/consts";
import { type IRentsListReponse } from "../responses/IRentsListReponse";
import axios from "axios";

export interface IGetRentsParams {
  page: string;
}

export const getRents = async (params: IGetRentsParams) => {
  const url = `${API_BASE_URL}/rent?page=${params.page}&pageSize=100`;
  const token = sessionStorage.getItem(TOKEN_KEY);
  const response = await axios.get<IRentsListReponse>(url, {
    headers: {
      Authorization: `Bearer ${token}`,
      Accept: "application/json",
    },
  });
  return response.data;
};
