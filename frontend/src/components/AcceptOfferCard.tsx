import {
  Card,
  CardHeader,
  CardTitle,
  CardContent,
  CardFooter,
} from "~/components/ui/card";
import { Button } from "~/components/ui/button";
import { useMutation } from "@tanstack/react-query";
import acceptOffer, { type IAcceptOfferParams } from "~/api/acceptOffer";

interface IAcceptOfferCardProps {
  offerId: number;
  costPerDay: number;
  insuranceCostPerDay: number;
  providerId: number;
}

const AcceptOfferCard = (props: IAcceptOfferCardProps) => {
  const mutation = useMutation({
    mutationFn: (values: IAcceptOfferParams) => acceptOffer(values),
    onSuccess: () => alert("check your mail"),
    onError: (error) => console.log(error),
  });

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
        <Button
          className="w-full bg-green-500 text-white hover:bg-green-600"
          onClick={() =>
            mutation.mutate({
              offerId: props.offerId,
              providerId: props.providerId,
            })
          }
        >
          Accept Offer
        </Button>
      </CardFooter>
    </Card>
  );
};

export default AcceptOfferCard;