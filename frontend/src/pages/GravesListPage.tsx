import { useEffect, useState } from "react";
import api from "../api/axios";
import { Link } from "react-router-dom";
import NavButtons from "../auth/NavButtons";
import { useAuth } from "../auth/AuthContext";

export default function GravesListPage() {
  const [graves, setGraves] = useState<any[]>([]);
  const { role } = useAuth();

  const fetchGraves = async () => {
    const res = await api.get("/grave");
    setGraves(res.data);
  };

  useEffect(() => {
    fetchGraves();
  }, []);

  const reserveGrave = async (id: number) => {
    try {
      await api.post(`/grave/${id}/reserve`);
      alert("Grave reserved successfully");
      fetchGraves();
    } catch (err: any) {
      alert(err.response?.data || "Error reserving grave");
    }
  };

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
      <h2>All Graves</h2>
      {graves.map(g => (
        <div key={g.id} style={{ marginBottom: "5px" }}>
          {g.location} | {g.price} | Status: {getStatusName(g.statusId)}
          {g.ownerId === null && role === "User" && (
            <button onClick={() => reserveGrave(g.id)}>Reserve</button>
          )}
          {role === "Employee" && (
            <Link to={`/graves/${g.id}`}>
              <button>View Details</button>
            </Link>
          )}
        </div>
      ))}
    </div>
  );
}
