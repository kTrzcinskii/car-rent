export interface ISingleCarResponse {
  carId: number;
  providerId: number;
  modelName: string;
  brandName: string;
  productionYear: number;
  metadata?: Record<string, unknown>;
  imageUrl?: string;
  localization: string;
}
