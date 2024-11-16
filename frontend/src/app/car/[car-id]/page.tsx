const CarPage = ({ params }: { params: { "car-id": string } }) => {
  return <div>{params["car-id"]}</div>;
};

export default CarPage;
