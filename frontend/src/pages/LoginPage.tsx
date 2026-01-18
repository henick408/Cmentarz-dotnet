import { useState, useEffect } from "react";
import api from "../api/axios";
import { useAuth } from "../auth/AuthContext";
import { useNavigate } from "react-router-dom";

export default function LoginPage() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const { login, token } = useAuth();
  const navigate = useNavigate();

  useEffect(() => {
    if (token) {
      navigate("/graves");
    }
  }, [token]);

  const submit = async () => {
    try {
      const res = await api.post("/auth/login", { email, password });
      const token = res.data.token;
      const payload = JSON.parse(atob(token.split(".")[1]));
      login(token, payload.role);
      navigate("/graves");
    } catch (err: any) {
      alert(err.response?.data || "Login failed");
    }
  };

  const goRegister = () => {
    navigate("/register");
  };

  return (
    <div>
      <h2>Login</h2>
      <input placeholder="Email" onChange={e => setEmail(e.target.value)} />
      <input
        placeholder="Password"
        type="password"
        onChange={e => setPassword(e.target.value)}
      />
      <button onClick={submit}>Login</button>
      <button onClick={goRegister}>Go to Register</button>
    </div>
  );
}
