import api from "./axios";

export type GraveStatus = {
  id: number;
  name: string;
};

export const getStatusById = async (id: number): Promise<GraveStatus> => {
  const res = await api.get(`/gravestatus/${id}`);
  return res.data;
};

export const getAllStatuses = async (): Promise<GraveStatus[]> => {
  const res = await api.get(`/gravestatus`); // jeśli masz endpoint GET /gravestatus, który zwraca wszystkie
  return res.data;
};
