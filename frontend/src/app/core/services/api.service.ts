import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { CourseResponse, CreateCourseRequest } from '../../shared/models/course.models';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private http = inject(HttpClient);
  private readonly baseUrl = environment.apiUrl;
  //private readonly baseUrl = 'http://localhost:5149/api';
  //private readonly baseUrl = 'https://nareshlearnapi20260408172133-gygfaphwdaccb0cu.canadacentral-01.azurewebsites.net/api';
  
  getCourses(): Observable<CourseResponse[]> {
    console.log('API URL:', this.baseUrl);
    return this.http.get<CourseResponse[]>(`${this.baseUrl}/courses`);
  }

  createCourse(request: CreateCourseRequest): Observable<CourseResponse> {
    return this.http.post<CourseResponse>(`${this.baseUrl}/courses`, request);
  }
}