import { useEffect, useState } from "react";
import { getAllStatuses, type GraveStatus } from "./statusApi";

export const useGraveStatuses = () => {
  const [statuses, setStatuses] = useState<GraveStatus[]>([]);

  useEffect(() => {
    getAllStatuses().then(setStatuses);
  }, []);

  const getStatusName = (id: number | undefined | null) => {
    if (!id) return "";
    const status = statuses.find(s => s.id === id);
    return status ? status.name : "";
  };

  return { statuses, getStatusName };
};
