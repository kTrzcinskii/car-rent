import { type ICarsListResponse } from "~/responses/ICarsListResponse";
import CarCard from "./CarCard";
import {
  Pagination,
  PaginationContent,
  PaginationItem,
  PaginationNext,
  PaginationPrevious,
} from "./ui/pagination";

interface CarsContainerProps {
  data: ICarsListResponse;
}

const CarsContainer = ({ data }: CarsContainerProps) => {
  return (
    <div className="mx-auto my-10 flex w-4/5 flex-col items-center justify-center space-y-5 lg:w-[500px]">
      <div className="w-full space-y-7">
        {data.cars.map((car) => {
          return <CarCard {...car} key={car.carId} />;
        })}
      </div>
      <Pagination>
        <PaginationContent>
          <PaginationItem>
            <PaginationPrevious
              href="/browse"
              aria-disabled={!data.hasPreviousPage}
              tabIndex={data.hasPreviousPage ? undefined : -1}
              className={
                data.hasPreviousPage
                  ? undefined
                  : "pointer-events-none opacity-50"
              }
            />
          </PaginationItem>
          <PaginationItem>
            <PaginationNext
              href="/browse"
              aria-disabled={!data.hasNextPage}
              tabIndex={data.hasNextPage ? undefined : -1}
              className={
                data.hasNextPage ? undefined : "pointer-events-none opacity-50"
              }
            />
          </PaginationItem>
        </PaginationContent>
      </Pagination>
    </div>
  );
};

export default CarsContainer;
