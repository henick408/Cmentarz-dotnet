import { useNavigate, useLocation } from "react-router-dom";
import LogoutButton from "./LogoutButton";
import { useAuth } from "./AuthContext";

export default function NavButtons() {
  const navigate = useNavigate();
  const location = useLocation();
  const { role } = useAuth();

  return (
    <div style={{ display: "flex", gap: "10px", marginBottom: "10px" }}>
      {location.pathname !== "/graves" && (
        <button onClick={() => navigate("/graves")}>All Graves</button>
      )}
      {location.pathname !== "/my-graves" && (
        <button onClick={() => navigate("/my-graves")}>My Graves</button>
      )}
      {role === "Employee" && location.pathname !== "/graves/create" && (
        <button onClick={() => navigate("/graves/create")}>Create Grave</button>
      )}
      <LogoutButton />
    </div>
  );
}
