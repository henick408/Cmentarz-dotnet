import api from "./axios";
import type { GraveReadDto, GraveCreateDto, DeceasedCreateDto } from "../types/grave";

export const graveApi = {
  getAll: () => api.get<GraveReadDto[]>("/grave"),
  getMyGraves: () => api.get<GraveReadDto[]>("/grave/my-graves"),
  getById: (id: number) => api.get<GraveReadDto>(`/grave/${id}`),
  create: (data: GraveCreateDto) => api.post("/grave", data),
  reserve: (id: number) => api.post(`/grave/${id}/reserve`),
  bury: (data: DeceasedCreateDto) => api.post("/grave/bury", data),
};
