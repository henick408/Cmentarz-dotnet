export interface LoginResponse {
  token: string;
}

export interface AuthState {
  token: string | null;
  role: string | null;
}
