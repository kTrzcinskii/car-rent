export type CarRentStatus = "Available" | "Rented" | "Returned";

export interface ISingleEmployeeRentInfoResponse {
  carId: number;
  rentId: number;
  brand: string;
  model: string;
  status: CarRentStatus;
  imageUrl?: string;
}
