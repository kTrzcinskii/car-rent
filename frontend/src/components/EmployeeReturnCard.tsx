import {
  type CarRentStatus,
  type ISingleEmployeeRentInfoResponse,
} from "~/responses/ISingleEmployeeRentInfoResponse";
import React from "react";
import { Card, CardContent, CardImage } from "~/components/ui/card";
import { Button } from "~/components/ui/button";
import { Badge } from "~/components/ui/badge";
import { Car } from "lucide-react";
import { useRouter } from "next/navigation";

type IEmployeeReturnCardProps = ISingleEmployeeRentInfoResponse;

const EmployeeReturnCard = (props: IEmployeeReturnCardProps) => {
  const router = useRouter();
  const getStatusColor = (status: CarRentStatus) => {
    switch (status) {
      case "Rented":
        return "bg-green-500";
      case "Returned":
        return "bg-yellow-500";
      default:
        return "bg-gray-500";
    }
  };

  return (
    <Card className="w-full max-w-md">
      <CardImage
        alt="Car image"
        src={props.imageUrl ?? "/placeholder.svg"}
        className="h-[200px] object-contain"
      />

      <CardContent className="p-6">
        <div className="flex items-center justify-between">
          <div className="flex items-center space-x-4">
            <div className="rounded-full bg-slate-100 p-2">
              <Car className="h-6 w-6 text-slate-600" />
            </div>
            <div>
              <h3 className="text-lg font-medium">
                {props.brand} {props.model}
              </h3>
              <div className="mt-1 flex items-center space-x-2">
                <Badge variant="secondary">ID: {props.carId}</Badge>
                <Badge className={getStatusColor(props.status)}>
                  {props.status}
                </Badge>
              </div>
            </div>
          </div>

          {props.status === "Returned" && (
            <Button
              onClick={() =>
                router.push(`/employee/rent-details/${props.carId}`)
              }
              className="ml-4"
            >
              See details
            </Button>
          )}
        </div>
      </CardContent>
    </Card>
  );
};

export default EmployeeReturnCard;
