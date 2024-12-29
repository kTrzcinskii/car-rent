import { type ISingleCarResponse } from "./ISignleCarResponse";

export interface ICarsListResponse {
  data: ISingleCarResponse[];
  count: number;
  hasNextPage: boolean;
  hasPreviousPage: boolean;
}
