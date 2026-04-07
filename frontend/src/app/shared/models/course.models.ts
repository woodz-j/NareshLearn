export interface CreateCourseRequest {
  title: string;
  description: string;
}

export interface CourseResponse {
  id: string;
  title: string;
  description: string;
  instructorId: string;
  isPublished: boolean;
}