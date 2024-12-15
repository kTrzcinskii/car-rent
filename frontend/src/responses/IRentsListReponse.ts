import { type ISingleRentResponse } from "./ISingleRentResponse";

export interface IRentsListReponse {
  data: ISingleRentResponse[];
  count: number;
  hasNextPage: boolean;
  hasPreviousPage: boolean;
}
