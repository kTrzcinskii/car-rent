import { API_BASE_URL } from "../lib/consts";
import { type ICarsListResponse } from "../responses/ICarsListResponse";
import axios from "axios";

export interface ISearchCarsParams {
  brandName?: string;
  modelName?: string;
  page: string;
}

export const searchCars = async (params: ISearchCarsParams) => {
  let url = `${API_BASE_URL}/car/search?page=${params.page}`;
  if (params.brandName && params.brandName.length > 0) {
    url = `${url}&brandName=${params.brandName}`;
  }
  if (params.modelName && params.modelName.length > 0) {
    url = `${url}&modelName=${params.modelName}`;
  }
  const response = await axios.get<ICarsListResponse>(url);
  return response.data;
};
