import React from "react";
import { Skeleton } from "../components/ui/skeleton";

const CarCardSkeleton = () => {
  return (
    <div className="w-full space-y-3">
      <Skeleton className="h-[200px] rounded-xl" />
      <Skeleton className="h-[18px] w-[200px]" />
      <Skeleton className="h-[10px] w-[160px]" />
      <Skeleton className="h-[10px] w-[160px]" />
    </div>
  );
};

export default CarCardSkeleton;
