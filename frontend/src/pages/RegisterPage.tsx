import { useState } from "react";
import api from "../api/axios";
import { useNavigate } from "react-router-dom";

export default function RegisterPage() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const navigate = useNavigate();

  const submit = async () => {
    try {
      await api.post("/auth/register", { email, password });
      alert("Registered successfully. You can now login.");
      navigate("/login");
    } catch (err: any) {
      alert(err.response?.data || "Registration failed");
    }
  };

  return (
    <div>
      <h2>Register</h2>
      <input placeholder="Email" onChange={e => setEmail(e.target.value)} />
      <input
        placeholder="Password"
        type="password"
        onChange={e => setPassword(e.target.value)}
      />
      <button onClick={submit}>Register</button>
    </div>
  );
}
