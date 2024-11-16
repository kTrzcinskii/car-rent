import CarsContainer from "~/components/CarsContainers";
import SearchCarsForm from "~/components/forms/SearchCarsForm";
import { type ICarsListResponse } from "~/responses/ICarsListResponse";

const BrowsePage = () => {
  const exampleData: ICarsListResponse = {
    count: 5,
    hasNextPage: true,
    hasPreviousPage: true,
    cars: [
      {
        brandName: "Brand",
        modelName: "Model",
        carId: Math.floor(Math.random() * 128),
        metadata: {
          "Additional metadata": "value",
        },
        productionYear: 2010,
        imageUrl: undefined,
        localization: "Warsaw",
      },
      {
        brandName: "Brand",
        modelName: "Model",
        carId: Math.floor(Math.random() * 128),
        metadata: {
          "Additional metadata": "value",
        },
        productionYear: 2010,
        imageUrl: undefined,
        localization: "Warsaw",
      },
      {
        brandName: "Brand",
        modelName: "Model",
        carId: Math.floor(Math.random() * 128),
        metadata: {
          "Additional metadata": "value",
        },
        productionYear: 2010,
        imageUrl: undefined,
        localization: "Warsaw",
      },
      {
        brandName: "Brand",
        modelName: "Model",
        carId: Math.floor(Math.random() * 128),
        metadata: {
          "Additional metadata": "value",
        },
        productionYear: 2010,
        imageUrl: undefined,
        localization: "Warsaw",
      },
      {
        brandName: "Brand",
        modelName: "Model",
        carId: Math.floor(Math.random() * 128),
        metadata: {
          "Additional metadata": "value",
        },
        productionYear: 2010,
        imageUrl: undefined,
        localization: "Warsaw",
      },
    ],
  };

  return (
    <main className="min-h-[calc(100vh-66px)] w-full py-14">
      <SearchCarsForm />
      <CarsContainer data={exampleData} />
    </main>
  );
};

export default BrowsePage;
