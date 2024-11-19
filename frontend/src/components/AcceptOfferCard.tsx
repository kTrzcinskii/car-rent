import {
  Card,
  CardHeader,
  CardTitle,
  CardContent,
  CardFooter,
} from "~/components/ui/card";
import { Button } from "~/components/ui/button";

interface IAcceptOfferCardProps {
  offerId: number;
  costPerDay: number;
  insuranceCostPerDay: number;
}

const AcceptOfferCard = (props: IAcceptOfferCardProps) => {
  return (
    <Card className="mx-auto min-w-[300px] p-4">
      <CardHeader>
        <CardTitle>Offer Details</CardTitle>
      </CardHeader>
      <CardContent>
        <div className="space-y-4">
          <div className="flex justify-between">
            <span className="font-medium text-gray-700">Cost:</span>
            <span className="text-gray-900">
              ${props.costPerDay.toFixed(2)}
            </span>
          </div>
          <div className="flex justify-between">
            <span className="font-medium text-gray-700">Insurance Cost:</span>
            <span className="text-gray-900">
              ${props.insuranceCostPerDay.toFixed(2)}
            </span>
          </div>
          <p className="text-sm text-gray-500">All costs are per day</p>
        </div>
      </CardContent>
      <CardFooter>
        <Button className="w-full bg-green-500 text-white hover:bg-green-600">
          Accept Offer
        </Button>
      </CardFooter>
    </Card>
  );
};

export default AcceptOfferCard;
