export interface IEmployeeRentDetailsResponse {
  carId: number;
  rentId: number;
  brand: string;
  model: string;
  productionYear: number;
  localization: string;
  costPerDay: number;
  insuranceCostPerDay: number;
  startDate: string;
  imageUrl?: string;
}
