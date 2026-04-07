import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CourseResponse, CreateCourseRequest } from '../../shared/models/course.models';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private http = inject(HttpClient);
  private readonly baseUrl = 'http://localhost:5149/api';

  getCourses(): Observable<CourseResponse[]> {
    return this.http.get<CourseResponse[]>(`${this.baseUrl}/courses`);
  }

  createCourse(request: CreateCourseRequest): Observable<CourseResponse> {
    return this.http.post<CourseResponse>(`${this.baseUrl}/courses`, request);
  }
}