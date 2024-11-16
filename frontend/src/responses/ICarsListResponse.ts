import { type ISingleCarResponse } from "./ISignleCarResponse";

export interface ICarsListResponse {
  cars: ISingleCarResponse[];
  count: number;
  hasNextPage: boolean;
  hasPreviousPage: boolean;
}
