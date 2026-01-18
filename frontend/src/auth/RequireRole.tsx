import { Navigate, Outlet } from "react-router-dom";
import { useAuth } from "./AuthContext";

export default function RequireRole({ role }: { role: string }) {
  const { role: userRole } = useAuth();
  return userRole === role ? <Outlet /> : <Navigate to="/graves" />;
}
