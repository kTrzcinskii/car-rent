import { type ICarsListResponse } from "~/responses/ICarsListResponse";
import CarCard from "./CarCard";

interface CarsContainerProps {
  data: ICarsListResponse;
}

const CarsContainer = ({ data }: CarsContainerProps) => {
  return (
    <div className="w-full space-y-7">
      {data.data.map((car) => {
        return <CarCard {...car} key={car.carId} />;
      })}
    </div>
  );
};

export default CarsContainer;
