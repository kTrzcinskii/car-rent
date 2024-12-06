"use client";

import { useQuery, useQueryClient } from "@tanstack/react-query";
import { REACT_QUERY_SEARCH_KEY } from "~/lib/consts";
import { type ISingleCarResponse } from "~/responses/ISignleCarResponse";
import Image from "next/image";
import { Button } from "~/components/ui/button";
import { useEffect, useState } from "react";
import getOffer from "~/api/getOffer";
import AcceptOfferCard from "~/components/AcceptOfferCard";
import { useToast } from "~/hooks/use-toast";
import { Loader2 } from "lucide-react";
import { REACT_QUERY_USER_INFO_KEY } from "~/lib/consts";
import { getUserInfo } from "~/api/getUserInfo";

const TitleValue = ({ title, value }: { title: string; value: string }) => {
  return (
    <p className="text-lg">
      <span className="font-semibold">{title}</span>: {value}
    </p>
  );
};

const CarPage = ({ params }: { params: { "car-id": string } }) => {
  const carId = params["car-id"];
  const queryClient = useQueryClient();
  const queryClientData = queryClient.getQueriesData({
    queryKey: [REACT_QUERY_SEARCH_KEY],
  });
  const [shouldGetOffers, setShouldGetOffers] = useState(false);
  // TODO: add error handling for this mess
  const carDataSearchGroup = queryClientData
    .map(
      // eslint-disable-next-line @typescript-eslint/no-explicit-any, @typescript-eslint/no-unsafe-member-access, @typescript-eslint/no-unsafe-return
      (group: any) => group[1]!.data!,
    )
    .flat() as ISingleCarResponse[];

  const carData = carDataSearchGroup.find(
    (car: ISingleCarResponse) => car.carId == Number(carId),
  );

  const { data, isError, error, isLoading } = useQuery({
    queryKey: ["getOffer", carData?.carId, carData?.providerId],
    queryFn: ({ queryKey }) =>
      getOffer(Number(queryKey[1]), Number(queryKey[2])),
    enabled: shouldGetOffers,
  });

  const { toast } = useToast();

  const { data: userInfoData } = useQuery({
    queryKey: [REACT_QUERY_USER_INFO_KEY],
    queryFn: getUserInfo,
  });

  useEffect(() => {
    if (isError) {
      if (shouldGetOffers) {
        toast({
          title: "Failed to generate an offer!",
          description: `An error occurred while trying to generate the offer (${error?.message}). Try again later.`,
          variant: "destructive",
        });
      }
      setShouldGetOffers(false);
    }
  }, [isError, error, toast, shouldGetOffers]);

  // TODO: handle when car is undefined
  if (carData == undefined) {
    return <div>couldnt load car</div>;
  }

  const handleOnGenerateOfferClick = () => {
    if (!userInfoData) {
      toast({
        title: "Unauthorize!",
        description:
          "This action is available only for authenticated users. Please log in to continue and access this feature.",
        variant: "destructive",
      });
      return;
    }
    setShouldGetOffers(true);
  };

  return (
    <div className="flex w-full flex-col items-center justify-center space-y-10 lg:space-y-16">
      <div className="flex flex-col space-y-10 py-10 lg:flex-row lg:space-x-36 lg:space-y-0">
        <div className="space-y-8">
          <h1 className="text-3xl font-semibold">Car details</h1>
          <div>
            <TitleValue title="Brand" value={carData.brandName} />
            <TitleValue title="Model" value={carData.modelName} />
            <TitleValue
              title="Production year"
              value={String(carData.productionYear)}
            />
            <TitleValue title="Localization" value={carData.localization} />
            {carData.metadata &&
              Object.entries(carData.metadata).map(([title, value]) => {
                return (
                  <TitleValue key={title} title={title} value={String(value)} />
                );
              })}
          </div>
        </div>
        <div className="relative max-w-[300px] lg:max-w-[400px]">
          <Image
            src={carData.imageUrl ?? "/placeholder.svg"}
            alt="Car image"
            width={0}
            height={0}
            sizes="100vw"
            style={{ width: "100%", height: "auto" }}
          />
        </div>
      </div>
      {data ? (
        <AcceptOfferCard
          costPerDay={data.costPerDay}
          insuranceCostPerDay={data.insuranceCostPerDay}
          offerId={data.offerId}
          providerId={data.providerId}
        />
      ) : (
        <Button
          onClick={() => handleOnGenerateOfferClick()}
          disabled={isLoading}
        >
          {isLoading && <Loader2 className="animate-spin" />}
          Generate offers
        </Button>
      )}
    </div>
  );
};

export default CarPage;
