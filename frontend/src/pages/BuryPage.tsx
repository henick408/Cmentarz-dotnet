import { useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import api from "../api/axios";
import NavButtons from "../auth/NavButtons";

export default function BuryPage() {
  const { graveId } = useParams<{ graveId: string }>();
  const navigate = useNavigate();
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [birthDate, setBirthDate] = useState("");
  const [deathDate, setDeathDate] = useState("");

  const submit = async () => {
    try {
      await api.post("/grave/bury", {
        firstName,
        lastName,
        birthDate,
        deathDate,
        graveId: Number(graveId),
      });
      alert("Burial completed successfully");
      navigate("/my-graves");
    } catch (err: any) {
      alert(err.response?.data || "Error burying deceased");
    }
  };

  return (
    <div>
      <NavButtons />
      <h2>Bury Deceased</h2>
      <input placeholder="First Name" onChange={e => setFirstName(e.target.value)} />
      <input placeholder="Last Name" onChange={e => setLastName(e.target.value)} />
      <input type="date" placeholder="Birth Date" onChange={e => setBirthDate(e.target.value)} />
      <input type="date" placeholder="Death Date" onChange={e => setDeathDate(e.target.value)} />
      <button onClick={submit}>Bury</button>
    </div>
  );
}
