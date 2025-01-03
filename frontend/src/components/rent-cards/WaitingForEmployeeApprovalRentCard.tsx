import {
  Car,
  MapPin,
  CalendarDays,
  Currency,
  Shield,
  PiggyBank,
} from "lucide-react";
import { type ISingleRentResponse } from "~/responses/ISingleRentResponse";
import {
  Card,
  CardHeader,
  CardTitle,
  CardContent,
  CardImage,
  CardFooter,
} from "../ui/card";

type IWaitingForEmployeeApprovalRentCardProps = ISingleRentResponse;

const WaitingForEmployeeApprovalRentCard = (
  props: IWaitingForEmployeeApprovalRentCardProps,
) => {
  const formatDate = (dateString: string) => {
    return new Date(dateString).toLocaleDateString();
  };

  const start = new Date(props.startDate);
  const end = new Date();
  const days = Math.ceil(
    (end.getTime() - start.getTime()) / (1000 * 60 * 60 * 24),
  );
  const totalCost = days * props.costPerDay;
  const totalInsurance = days * props.insuranceCostPerDay;

  return (
    <Card className="w-full max-w-md">
      <CardImage
        alt="Car image"
        src={props.imageUrl ?? "/placeholder.svg"}
        className="h-[200px] object-contain"
      />

      <CardHeader>
        <CardTitle className="flex items-center gap-2 text-2xl">
          <Car className="h-5 w-5" />
          {props.brandName} {props.modelName} ({props.productionYear})
        </CardTitle>
      </CardHeader>

      <CardContent className="space-y-2">
        <div className="flex w-full flex-row items-center justify-between">
          <div className="flex items-center gap-2 text-muted-foreground">
            <MapPin className="h-4 w-4" />
            <span>{props.localization}</span>
          </div>

          <div className="flex items-center gap-2 text-muted-foreground">
            <CalendarDays className="h-4 w-4" />
            <span>{formatDate(props.startDate)} - ...</span>
          </div>
        </div>

        <div className="space-y-2">
          <div className="flex items-center gap-2">
            <Currency className="h-4 w-4" />
            <span className="font-medium">
              Cost: {totalCost.toFixed(2)} PLN ({props.costPerDay.toFixed(2)}{" "}
              PLN per day)
            </span>
          </div>

          <div className="flex items-center gap-2">
            <Shield className="h-4 w-4" />
            <span className="font-medium">
              Insurance: {totalInsurance.toFixed(2)} PLN (
              {props.insuranceCostPerDay.toFixed(2)} PLN per day)
            </span>
          </div>

          <div className="flex items-center gap-2 font-bold">
            <PiggyBank className="h-4 w-4" />{" "}
            <span>Total: {(totalCost + totalInsurance).toFixed(2)} PLN</span>
          </div>
        </div>

        <CardFooter className="pt-4">
          <p className="flex items-center gap-2 font-semibold">
            <span className="mr-3 inline-block h-2 w-2 animate-pulse rounded-full bg-gray-400"></span>
            Wait for car provider employee to check your return.
          </p>
        </CardFooter>
      </CardContent>
    </Card>
  );
};

export default WaitingForEmployeeApprovalRentCard;
