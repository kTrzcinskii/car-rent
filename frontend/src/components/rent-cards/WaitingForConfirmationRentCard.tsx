import { Car, MapPin, Currency, Shield } from "lucide-react";
import { type ISingleRentResponse } from "~/responses/ISingleRentResponse";
import {
  Card,
  CardHeader,
  CardTitle,
  CardContent,
  CardImage,
  CardFooter,
} from "../ui/card";

type IWaitingForConfirmationRentCard = ISingleRentResponse;

const WaitingForConfirmationRentCard = (
  props: IWaitingForConfirmationRentCard,
) => {
  return (
    <Card className="w-full max-w-md bg-gray-50 opacity-90">
      <CardImage
        alt="Car image"
        src={props.imageUrl ?? "/placeholder.svg"}
        className="h-[200px] object-contain opacity-75"
      />

      <CardHeader className="bg-gray-50">
        <CardTitle className="flex items-center gap-2 text-2xl text-gray-600">
          <Car className="h-5 w-5" />
          {props.brandName} {props.modelName} ({props.productionYear})
        </CardTitle>
      </CardHeader>

      <CardContent className="space-y-2 bg-gray-50">
        <div className="flex items-center gap-2 text-gray-500">
          <MapPin className="h-4 w-4" />
          <span>{props.localization}</span>
        </div>

        <div className="space-y-2">
          <div className="flex items-center gap-2 text-gray-600">
            <Currency className="h-4 w-4" />
            <span className="font-medium">
              Cost: {props.costPerDay.toFixed(2)} PLN per day
            </span>
          </div>

          <div className="flex items-center gap-2 text-gray-600">
            <Shield className="h-4 w-4" />
            <span className="font-medium">
              Insurance: {props.insuranceCostPerDay.toFixed(2)} PLN per day
            </span>
          </div>
        </div>

        <CardFooter className="border-t border-gray-200 pt-4">
          <p className="flex items-center gap-2 font-semibold text-gray-600">
            <span className="mr-3 inline-block h-2 w-2 animate-pulse rounded-full bg-gray-400"></span>
            Please check your email to confirm rent.
          </p>
        </CardFooter>
      </CardContent>
    </Card>
  );
};

export default WaitingForConfirmationRentCard;
