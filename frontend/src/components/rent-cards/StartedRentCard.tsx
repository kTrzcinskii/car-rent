import {
  Car,
  MapPin,
  CalendarDays,
  Currency,
  Shield,
  PiggyBank,
  Loader2,
} from "lucide-react";
import { type ISingleRentResponse } from "~/responses/ISingleRentResponse";
import {
  Card,
  CardHeader,
  CardTitle,
  CardContent,
  CardFooter,
  CardImage,
} from "../ui/card";
import { Button } from "../ui/button";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { type IFinishRentParams, finishRent } from "../../api/finishRent";
import { useToast } from "~/hooks/use-toast";
import { REACT_QUERY_GET_RENTS_KEY } from "~/lib/consts";

type IStartedRentCardProps = ISingleRentResponse;

const StartedRentCard = (props: IStartedRentCardProps) => {
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

  const { toast } = useToast();
  const queryClient = useQueryClient();

  const mutation = useMutation({
    mutationFn: (values: IFinishRentParams) => finishRent(values),
    onSuccess: async () => {
      toast({
        title: "Rent finished!",
        description:
          "Now you need to wait for car provider employee to approve your requeset.",
        variant: "success",
      });
      await queryClient.invalidateQueries({
        queryKey: [REACT_QUERY_GET_RENTS_KEY],
      });
    },
    onError: (error) =>
      toast({
        title: "Failed to finish rent!",
        description: `An error occured while trying to finish the rent (${error.message}). Try again later.`,
        variant: "destructive",
      }),
  });

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
            <span>
              {formatDate(props.startDate)} - ...
              {props.endDate && ` To: ${formatDate(props.endDate)}`}
            </span>
          </div>
        </div>

        <div className="space-y-2">
          <div className="flex items-center gap-2">
            <Currency className="h-4 w-4" />
            <span className="font-medium">
              Cost: ${totalCost.toFixed(2)} (${props.costPerDay.toFixed(2)} per
              day)
            </span>
          </div>

          <div className="flex items-center gap-2">
            <Shield className="h-4 w-4" />
            <span className="font-medium">
              Insurance: ${totalInsurance.toFixed(2)} ($
              {props.insuranceCostPerDay.toFixed(2)} per day)
            </span>
          </div>

          <div className="flex items-center gap-2 font-bold">
            <PiggyBank className="h-4 w-4" />{" "}
            <span>Total: ${(totalCost + totalInsurance).toFixed(2)}</span>
          </div>
        </div>

        <CardFooter className="mt-auto pt-4">
          <Button
            variant="secondary"
            className="w-full bg-sky-400 text-white transition-colors duration-200 hover:bg-sky-500 active:bg-sky-600"
            onClick={() => mutation.mutate({ rentId: props.rentId })}
            disabled={mutation.isPending}
          >
            {mutation.isPending && <Loader2 className="animate-spin" />}
            Finish rent
          </Button>
        </CardFooter>
      </CardContent>
    </Card>
  );
};

export default StartedRentCard;
