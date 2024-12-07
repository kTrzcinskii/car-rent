"use client";

import SearchCarsForm from "~/components/forms/SearchCarsForm";
import { searchCars, type ISearchCarsParams } from "~/api/searchCars";
import { useSearchParams } from "next/navigation";
import { useQuery } from "@tanstack/react-query";
import { useState } from "react";
import CarsSearchResults from "../../components/CarsSearchResults";
import { REACT_QUERY_SEARCH_KEY } from "~/lib/consts";

const BrowsePage = () => {
  const searchParams = useSearchParams();

  const [brandName, setBrandName] = useState(
    searchParams.get("brandName") ?? "",
  );
  const [modelName, setModelName] = useState(
    searchParams.get("modelName") ?? "",
  );
  const [page, setPage] = useState(searchParams.get("page") ?? "0");

  const params: ISearchCarsParams = { page, brandName, modelName };
  const { data, isLoading, isError } = useQuery({
    queryKey: [REACT_QUERY_SEARCH_KEY, params],
    queryFn: ({ queryKey }) => searchCars(queryKey[1] as ISearchCarsParams),
  });

  return (
    <main className="min-h-[calc(100vh-66px)] w-full py-14">
      <SearchCarsForm
        defaultBrandName={brandName}
        defaultModelName={modelName}
        setBrandName={setBrandName}
        setModelName={setModelName}
        setPage={setPage}
      />
      <div className="mx-auto my-10 flex w-4/5 flex-col items-center justify-center space-y-5 lg:w-[500px]">
        <CarsSearchResults
          data={data}
          isError={isError}
          isLoading={isLoading}
        />
      </div>
    </main>
  );
};

export default BrowsePage;
