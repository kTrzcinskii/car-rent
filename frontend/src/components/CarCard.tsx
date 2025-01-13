import { type ISingleCarResponse } from "~/responses/ISignleCarResponse";
import { Card, CardContent, CardHeader, CardImage } from "./ui/card";
import Link from "next/link";

type CarCardProps = ISingleCarResponse;

const TitleValueElement = ({
  title,
  value,
}: {
  title: string;
  value: string;
}) => {
  return (
    <p>
      <span className="font-semibold">{title}</span>: {value}
    </p>
  );
};

const CarCard = ({
  brandName,
  carId,
  metadata,
  modelName,
  productionYear,
  imageUrl,
  localization,
}: CarCardProps) => {
  return (
    <div>
      <Link href={`/car/${carId}`}>
        <Card>
          <CardImage
            alt="Car image"
            src={imageUrl ?? "/placeholder.svg"}
            className="h-[200px] object-contain"
          />
          <CardHeader
            className="text-2xl font-semibold"
            data-testid="car-card-header"
          >
            {brandName} {modelName}
          </CardHeader>
          <CardContent>
            <TitleValueElement
              title="Production year"
              value={String(productionYear)}
            />
            <TitleValueElement title="Localization" value={localization} />
            {metadata &&
              Object.entries(metadata).map(([title, value]) => {
                return (
                  <TitleValueElement
                    key={title}
                    title={title}
                    value={String(value)}
                  />
                );
              })}
          </CardContent>
        </Card>
      </Link>
    </div>
  );
};

export default CarCard;
