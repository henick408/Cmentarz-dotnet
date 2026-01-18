import { Routes, Route, Navigate } from "react-router-dom";
import RequireAuth from "./auth/RequireAuth";
import RequireRole from "./auth/RequireRole";

import LoginPage from "./pages/LoginPage";
import RegisterPage from "./pages/RegisterPage";
import GravesListPage from "./pages/GravesListPage";
import GraveDetailsPage from "./pages/GraveDetailsPage";
import MyGravesPage from "./pages/MyGravesPage";
import CreateGravePage from "./pages/CreateGravePage";
import { useAuth } from "./auth/AuthContext";
import BuryPage from "./pages/BuryPage";

export default function App() {
  const { token } = useAuth();

  return (
    <Routes>
      {/* "/" warunkowo przenosi do /graves jeśli zalogowany, inaczej do /login */}
      <Route path="/" element={token ? <Navigate to="/graves" /> : <Navigate to="/login" />} />

      <Route path="/login" element={<LoginPage />} />
      <Route path="/register" element={<RegisterPage />} />
	  <Route path="/bury/:graveId" element={<BuryPage />} />

      <Route element={<RequireAuth />}>
        <Route path="/graves" element={<GravesListPage />} />
        <Route path="/graves/:id" element={<GraveDetailsPage />} />
        <Route path="/my-graves" element={<MyGravesPage />} />
      </Route>

      <Route element={<RequireRole role="Employee" />}>
        <Route path="/graves/create" element={<CreateGravePage />} />
      </Route>
    </Routes>
  );
}
