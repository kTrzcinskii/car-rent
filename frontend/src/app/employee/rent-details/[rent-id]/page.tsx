const RentDetailsPage = ({ params }: { params: { "rent-id": string } }) => {
  const rentId = params["rent-id"];

  return <div>{rentId}</div>;
};

export default RentDetailsPage;
