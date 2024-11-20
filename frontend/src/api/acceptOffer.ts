import axios from "axios";
import { API_BASE_URL, TOKEN_KEY } from "~/lib/consts";

export interface IAcceptOfferParams {
  offerId: number;
  providerId: number;
}

const acceptOffer = async (params: IAcceptOfferParams) => {
  const url = `${API_BASE_URL}/offer/accept?offerId=${params.offerId}&providerId=${params.providerId}`;
  const token = sessionStorage.getItem(TOKEN_KEY);
  await axios.post(url, null, {
    headers: { Authorization: `Bearer ${token}` },
  });
};

export default acceptOffer;
