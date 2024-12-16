import { type RentStatus } from "./ISingleRentResponse";

export interface ISingleEmployeeRentInfoResponse {
  carId: number;
  rentId: number;
  brand: string;
  model: string;
  status: RentStatus;
}
