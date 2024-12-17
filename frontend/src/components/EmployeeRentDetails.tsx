import { MapPinIcon, CalendarIcon, BanknoteIcon } from "lucide-react";
import { type IEmployeeRentDetailsResponse } from "~/responses/IEmployeeRentDetailsResponse";
import { Card, CardContent, CardImage } from "./ui/card";

type IEmployeeRentDetailsProps = IEmployeeRentDetailsResponse;

const EmployeeRentDetails = (props: IEmployeeRentDetailsProps) => {
  const formatDate = (dateString: string) => {
    return new Date(dateString).toLocaleDateString();
  };

  const calculateDays = () => {
    const start = new Date(props.startDate);
    const current = new Date();
    const diffTime = Math.abs(current.getTime() - start.getTime());
    const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));
    return diffDays;
  };

  const numberOfDays = calculateDays();
  const totalDailyCost = props.costPerDay + props.insuranceCostPerDay;
  const totalCost = totalDailyCost * numberOfDays;

  return (
    <Card className="w-full max-w-lg bg-white shadow-lg">
      <CardImage
        alt="Car image"
        src={props.imageUrl ?? "/placeholder.svg"}
        className="h-[200px] object-contain"
      />

      <CardContent className="space-y-6 p-6">
        <div className="flex flex-col justify-between lg:flex-row">
          <div>
            <h1 className="text-2xl font-bold text-gray-900">
              {props.brand} {props.model}
            </h1>
            <p className="text-gray-500">Year: {props.productionYear}</p>
          </div>
          <div className="text-sm text-gray-500">
            <p>Rent ID: {props.rentId}</p>
            <p>Car ID: {props.carId}</p>
          </div>
        </div>

        <div className="space-y-4">
          <div className="flex items-center gap-2">
            <MapPinIcon className="h-5 w-5 text-gray-500" />
            <span>{props.localization}</span>
          </div>

          <div className="flex flex-col justify-between space-y-4 lg:flex-row">
            <div className="flex items-center gap-2">
              <CalendarIcon className="h-5 w-5 text-gray-500" />
              <div className="space-y-1">
                <p>Start Date: {formatDate(props.startDate)}</p>
                <p>Rental Duration: {numberOfDays} days</p>
              </div>
            </div>

            <div className="flex items-center gap-2">
              <BanknoteIcon className="h-5 w-5 text-gray-500" />
              <div className="space-y-1">
                <p>Base Rate: ${props.costPerDay}/day</p>
                <p>Insurance: ${props.insuranceCostPerDay}/day</p>
                <div className="my-2 h-px bg-gray-200" />
                <p>Daily Total: ${totalDailyCost}</p>
              </div>
            </div>
          </div>
          <p className="text-lg font-bold">
            Total Cost ({numberOfDays} days): ${totalCost.toFixed(2)}
          </p>
        </div>
      </CardContent>
    </Card>
  );
};

export default EmployeeRentDetails;
