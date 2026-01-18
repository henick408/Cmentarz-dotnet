export interface DeceasedReadDto {
  id: number;
  firstName: string;
  lastName: string;
  birthDate: string; // DateOnly comes as string from API
  deathDate: string;
  graveId: number;
}

export interface GraveReadDto {
  id: number;
  location: string;
  price: number;
  statusId: number;
  ownerId: number | null;
  deceased?: DeceasedReadDto | null;
}

export interface GraveCreateDto {
  location: string;
  price: number;
}

export interface DeceasedCreateDto {
  firstName: string;
  lastName: string;
  birthDate: string;
  deathDate: string;
  graveId: number;
}
