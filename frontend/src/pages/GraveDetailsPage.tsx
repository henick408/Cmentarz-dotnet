import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { graveApi } from "../api/graveApi";
import type { GraveReadDto } from "../types/grave";

export default function GraveDetailsPage() {
  const { id } = useParams<{ id: string }>();
  const [grave, setGrave] = useState<GraveReadDto | null>(null);

  useEffect(() => {
    if (!id) return;
    graveApi.getById(Number(id)).then(res => setGrave(res.data));
  }, [id]);

  if (!grave) return <div>Loading...</div>;

  return (
    <div>
      <h2>Grave Details</h2>
      <p>Location: {grave.location}</p>
      <p>Price: {grave.price}</p>
      <p>StatusId: {grave.statusId}</p>
      {grave.deceased ? (
        <div>
          <h3>Deceased:</h3>
          <p>{grave.deceased.firstName} {grave.deceased.lastName}</p>
          <p>Birth: {grave.deceased.birthDate}</p>
          <p>Death: {grave.deceased.deathDate}</p>
        </div>
      ) : (
        <p>No deceased</p>
      )}
    </div>
  );
}
