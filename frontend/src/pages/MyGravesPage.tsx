import { useEffect, useState } from "react";
import api from "../api/axios";
import { useNavigate, Link } from "react-router-dom";
import NavButtons from "../auth/NavButtons";

export default function MyGravesPage() {
  const [graves, setGraves] = useState<any[]>([]);
  const navigate = useNavigate();

  const fetchMyGraves = async () => {
    const res = await api.get("/grave/my-graves");
    setGraves(res.data);
  };

  useEffect(() => {
    fetchMyGraves();
  }, []);

  const getStatusName = (id: number) => {
    switch (id) {
      case 1:
        return "Free";
      case 2:
        return "Reserved";
      case 3:
        return "Occupied";
      default:
        return "Unknown";
    }
  };

  return (
    <div>
      <NavButtons />
      <h2>My Graves</h2>
      {graves.map(g => (
        <div key={g.id} style={{ marginBottom: "5px" }}>
          {g.location} | {g.price} | Status: {getStatusName(g.statusId)}
          {!g.deceased && (
            <button onClick={() => navigate(`/bury/${g.id}`)}>Bury Deceased</button>
          )}
          <Link to={`/graves/${g.id}`}>
            <button>View Details</button>
          </Link>
        </div>
      ))}
    </div>
  );
}
