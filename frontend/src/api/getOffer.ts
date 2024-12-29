import axios from "axios";
import { API_BASE_URL, TOKEN_KEY } from "~/lib/consts";
import { type IOfferResponse } from "~/responses/IOfferResponse";

const getOffer = async (carId: number, providerId: number) => {
  const url = `${API_BASE_URL}/offer?carId=${carId}&providerId=${providerId}`;
  const token = sessionStorage.getItem(TOKEN_KEY);
  const response = await axios.get<IOfferResponse>(url, {
    headers: { Authorization: `Bearer ${token}` },
  });
  return response.data;
};

export default getOffer;
