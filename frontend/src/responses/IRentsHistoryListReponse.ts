import { type ISingleRentHistoryReponse } from "./ISingleRentHistoryResponse";

export interface IRentsHistoryListResponse {
  data: ISingleRentHistoryReponse[];
  count: number;
  hasNextPage: boolean;
  hasPreviousPage: boolean;
}
