import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of, throwError } from 'rxjs';
import { ApiService } from '../../../core/services/api.service';

import { CourseList } from './course-list';

describe('CourseList', () => {
  let component: CourseList;
  let fixture: ComponentFixture<CourseList>;
  let apiService: { getCourses: ReturnType<typeof vi.fn> };

  beforeEach(async () => {
    apiService = {
      getCourses: vi.fn().mockReturnValue(of([]))
    };

    await TestBed.configureTestingModule({
      imports: [CourseList],
      providers: [
        { provide: ApiService, useValue: apiService }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(CourseList);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should render accessible empty state', async () => {
    fixture.detectChanges();
    await fixture.whenStable();
    fixture.detectChanges();

    const emptyState = fixture.nativeElement.querySelector('.empty-state') as HTMLElement;

    expect(emptyState.getAttribute('role')).toBe('status');
    expect(emptyState.textContent).toContain('No courses available yet.');
  });

  it('should render accessible error state', async () => {
    apiService.getCourses.mockReturnValue(
      throwError(() => ({ error: { error: 'Failed to load courses.' } }))
    );
    fixture = TestBed.createComponent(CourseList);
    component = fixture.componentInstance;
    fixture.detectChanges();
    await fixture.whenStable();
    fixture.detectChanges();

    const alert = fixture.nativeElement.querySelector('[role="alert"]') as HTMLElement;

    expect(alert.textContent).toContain('Failed to load courses.');
    expect(alert.getAttribute('aria-live')).toBe('assertive');
  });
});
