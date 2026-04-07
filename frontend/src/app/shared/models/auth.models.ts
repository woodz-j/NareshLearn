export interface RegisterRequest {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
  role: number;
}

export interface RegisterResponse {
  userId: string;
  email: string;
  role: string;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface LoginResponse {
  userId: string;
  email: string;
  role: string;
  accessToken: string;
}

export interface CourseResponse {
  id: string;
  title: string;
  description: string;
  instructorId: string;
  isPublished: boolean;
}