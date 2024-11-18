"use client";

import React from "react";
import { type ICarsListResponse } from "../responses/ICarsListResponse";
import CarCardSkeleton from "../components/CarCardSkeleton";
import ErrorAlert from "./ErrorAlert";
import {
  Pagination,
  PaginationContent,
  PaginationItem,
  PaginationNext,
  PaginationPrevious,
} from "../components/ui/pagination";
import CarsContainer from "../components/CarsContainers";
import { useSearchParams } from "next/navigation";

interface ICarsSearchResultProps {
  data: ICarsListResponse | undefined;
  isLoading: boolean;
  isError: boolean;
}

const CarsSearchResult = (props: ICarsSearchResultProps) => {
  const searchParams = useSearchParams();

  if (props.isLoading) {
    return (
      <div className="w-full space-y-7">
        {Array.from({ length: 5 }, (_, index) => (
          <CarCardSkeleton key={index} />
        ))}
      </div>
    );
  }

  if (props.isError) {
    return (
      <div className="w-full">
        <ErrorAlert
          title="Cannot load data"
          message="There was a problem while loading data. Please try again later."
        />
      </div>
    );
  }

  const page = searchParams.get("page") ?? "0";
  const paramsNextPage = new URLSearchParams(searchParams.toString());
  paramsNextPage.set("page", (Number(page) + 1).toString());
  const paramsPreviousPage = new URLSearchParams(searchParams.toString());
  paramsPreviousPage.set("page", (Number(page) - 1).toString());

  return (
    <>
      <CarsContainer data={props.data!} />
      <Pagination>
        <PaginationContent>
          <PaginationItem>
            <PaginationPrevious
              href={`/browse?${paramsPreviousPage}`}
              aria-disabled={!props.data?.hasPreviousPage}
              tabIndex={props.data?.hasPreviousPage ? undefined : -1}
              className={
                props.data?.hasPreviousPage
                  ? undefined
                  : "pointer-events-none opacity-50"
              }
            />
          </PaginationItem>
          <PaginationItem>
            <PaginationNext
              href={`/browse?${paramsNextPage}`}
              aria-disabled={!props.data?.hasNextPage}
              tabIndex={props.data?.hasNextPage ? undefined : -1}
              className={
                props.data?.hasNextPage
                  ? undefined
                  : "pointer-events-none opacity-50"
              }
            />
          </PaginationItem>
        </PaginationContent>
      </Pagination>
    </>
  );
};

export default CarsSearchResult;
