import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Router } from '@angular/router';
import { throwError } from 'rxjs';
import { ApiService } from '../../../core/services/api.service';

import { CourseCreate } from './course-create';

describe('CourseCreate', () => {
  let component: CourseCreate;
  let fixture: ComponentFixture<CourseCreate>;
  let apiService: { createCourse: ReturnType<typeof vi.fn> };

  beforeEach(async () => {
    apiService = {
      createCourse: vi.fn()
    };

    await TestBed.configureTestingModule({
      imports: [CourseCreate],
      providers: [
        { provide: ApiService, useValue: apiService },
        { provide: Router, useValue: { navigateByUrl: vi.fn() } }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(CourseCreate);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should render accessible validation messages when invalid', () => {
    component.onSubmit();
    fixture.detectChanges();

    const title = fixture.nativeElement.querySelector('#title') as HTMLInputElement;

    expect(title.getAttribute('aria-invalid')).toBe('true');
    expect(title.getAttribute('aria-describedby')).toBe('course-title-error');
    expect(fixture.nativeElement.textContent).toContain('Title is required and must be 200 characters or less.');
    expect(apiService.createCourse).not.toHaveBeenCalled();
  });

  it('should expose API errors as alerts', () => {
    apiService.createCourse.mockReturnValue(
      throwError(() => ({ error: { error: 'Failed to create course.' } }))
    );
    component.form.setValue({
      title: 'Intro to C#',
      description: 'Basics'
    });

    component.onSubmit();
    fixture.detectChanges();

    const alert = fixture.nativeElement.querySelector('[role="alert"]') as HTMLElement;

    expect(alert.textContent).toContain('Failed to create course.');
    expect(alert.getAttribute('aria-live')).toBe('assertive');
  });
});
