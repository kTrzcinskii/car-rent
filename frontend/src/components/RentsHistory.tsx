import { useQuery } from "@tanstack/react-query";
import { type IGetRentsParams, getRents } from "~/api/getRents";
import { REACT_QUERY_GET_RENTS_KEY } from "~/lib/consts";
import ErrorAlert from "./ErrorAlert";
import StartedRentCard from "./rent-cards/StartedRentCard";
import { type ReactNode } from "react";
import WaitingForConfirmationRentCard from "./rent-cards/WaitingForConfirmationRentCard";
import { Tooltip, TooltipContent, TooltipProvider } from "./ui/tooltip";
import { TooltipTrigger } from "@radix-ui/react-tooltip";
import { Info } from "lucide-react";

interface IRentGroupContainerProps {
  children: ReactNode;
  text: string;
  additionalInfo: string;
}

const RentGroupContainer = (props: IRentGroupContainerProps) => {
  return (
    <div className="w-[90%] space-y-6">
      <TooltipProvider>
        <Tooltip>
          <TooltipTrigger asChild>
            <h3 className="inline-flex items-center text-2xl font-semibold">
              {props.text}
              <div className="ml-2">
                <Info className="h-5 w-5" />
              </div>
            </h3>
          </TooltipTrigger>
          <TooltipContent>
            <p>{props.additionalInfo}</p>
          </TooltipContent>
        </Tooltip>
      </TooltipProvider>
      <div className="grid w-full grid-cols-1 gap-6 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4">
        {props.children}
      </div>
    </div>
  );
};

const RentsHistory = () => {
  const params: IGetRentsParams = { page: "0" };
  const { data, isLoading, isError } = useQuery({
    queryKey: [REACT_QUERY_GET_RENTS_KEY, params],
    queryFn: ({ queryKey }) => getRents(queryKey[1] as IGetRentsParams),
  });

  if (isLoading) {
    return <div>TODO: loading </div>;
  }

  if (isError) {
    return (
      <div className="w-full">
        <ErrorAlert
          title="Cannot load data"
          message="There was a problem while loading data. Please try again later."
        />
      </div>
    );
  }

  const waitingForConfirmationRents = data?.data.filter(
    (rent) => rent.status === "WaitingForConfirmation",
  );
  const startedRents = data?.data.filter((rent) => rent.status === "Started");

  return (
    <div className="my-10 flex w-full flex-col items-center justify-center space-y-8 lg:space-y-12">
      <RentGroupContainer
        text="Not Confirmed"
        additionalInfo="Rents that have not yet been confirmed. Usually after some time such rents are cancelled."
      >
        {waitingForConfirmationRents?.map((rent) => {
          return <WaitingForConfirmationRentCard {...rent} key={rent.rentId} />;
        })}
      </RentGroupContainer>
      <RentGroupContainer
        text="Started"
        additionalInfo="Rents that are currently active."
      >
        {startedRents?.map((rent) => {
          return <StartedRentCard {...rent} key={rent.rentId} />;
        })}
      </RentGroupContainer>
    </div>
  );
};

export default RentsHistory;
