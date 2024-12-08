import { type ISingleCarResponse } from "./ISignleCarResponse";

export interface ISingleRentHistoryReponse {
  rentId: number;
  providerId: number;
  costPerDay: number;
  insuranceCostPerDay: number;
  startDate: string;
  endDate?: string;
  isFinished: boolean;
  carData: ISingleCarResponse;
}
