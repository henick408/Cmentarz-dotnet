import { useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "../api/axios";
import NavButtons from "../auth/NavButtons";

export default function CreateGravePage() {
  const [location, setLocation] = useState("");
  const [price, setPrice] = useState("");
  const navigate = useNavigate();

  const submit = async () => {
    if (!location || !price) {
      alert("Please fill all fields");
      return;
    }

    try {
      await api.post("/grave", {
        location,
        price: parseFloat(price),
      });
      alert("Grave created successfully");
      navigate("/graves");
    } catch (err: any) {
      alert(err.response?.data || "Error creating grave");
    }
  };

  return (
    <div>
      <NavButtons />
      <h2>Create Grave</h2>
      <div>
        <input
          placeholder="Location"
          value={location}
          onChange={(e) => setLocation(e.target.value)}
        />
      </div>
      <div>
        <input
          placeholder="Price"
          type="number"
          value={price}
          onChange={(e) => setPrice(e.target.value)}
        />
      </div>
      <button onClick={submit}>Create Grave</button>
    </div>
  );
}
