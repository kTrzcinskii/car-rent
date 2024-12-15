export interface ISingleRentResponse {
  rentId: number;
  startDate: string;
  endDate?: string;
  status:
    | "WaitingForConfirmation"
    | "Started"
    | "WaitingForEmployeeApproval"
    | "Finished";
  costPerDay: number;
  insuranceCostPerDay: number;
  modelName: string;
  brandName: string;
  localization: string;
  productionYear: number;
  imageUrl?: string;
}
