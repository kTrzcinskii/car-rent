export type RentStatus =
  | "WaitingForConfirmation"
  | "Started"
  | "WaitingForEmployeeApproval"
  | "Finished";

export interface ISingleRentResponse {
  rentId: number;
  startDate: string;
  endDate?: string;
  status: RentStatus;
  costPerDay: number;
  insuranceCostPerDay: number;
  modelName: string;
  brandName: string;
  localization: string;
  productionYear: number;
  imageUrl?: string;
}
