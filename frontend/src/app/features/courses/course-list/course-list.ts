import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
//import { finalize } from 'rxjs';
import { Observable, catchError, map, of, tap } from 'rxjs';
import { ApiService } from '../../../core/services/api.service';
import { CourseResponse } from '../../../shared/models/course.models';

interface CourseListState {
  courses: CourseResponse[];
  errorMessage: string;
}

@Component({
  selector: 'app-course-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './course-list.html',
  styleUrl: './course-list.scss'
})
export class CourseList {
  private apiService = inject(ApiService);

  coursesState$: Observable<CourseListState> = this.apiService.getCourses().pipe(
    tap(courses => console.log('Courses from API:', courses)),
    map(courses => ({ courses, errorMessage: '' })),
    catchError(err => {
      console.error('Failed to load courses:', err);
      return of({ courses: [], errorMessage: 'Failed to load courses.' });
    })
  );
}
/*
export class CourseList implements OnInit {
  private apiService = inject(ApiService);

  courses: CourseResponse[] = [];
  isLoading = true;
  errorMessage = '';

  ngOnInit(): void {
    this.apiService.getCourses()
      .pipe(
        finalize(() => {
          this.isLoading = false;
        })
      )
      .subscribe({
        next: (response) => {
          this.courses = response;
        },
        error: (err) => {
          console.error('Failed to load courses:', err);
          this.errorMessage = 'Failed to load courses.';
        }
      });
  }
}*/
