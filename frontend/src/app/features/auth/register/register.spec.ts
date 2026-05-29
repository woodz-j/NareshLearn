import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Router } from '@angular/router';
import { provideRouter } from '@angular/router';
import { throwError, of } from 'rxjs';
import { AuthService } from '../../../core/auth/auth.service';

import { Register } from './register';

describe('Register', () => {
  let component: Register;
  let fixture: ComponentFixture<Register>;
  let authService: { register: ReturnType<typeof vi.fn> };
  let router: Router;
  let navigateByUrl: ReturnType<typeof vi.spyOn>;

  beforeEach(async () => {
    authService = {
      register: vi.fn()
    };

    await TestBed.configureTestingModule({
      imports: [Register],
      providers: [
        { provide: AuthService, useValue: authService },
        provideRouter([])
      ]
    }).compileComponents();

    router = TestBed.inject(Router);
    navigateByUrl = vi.spyOn(router, 'navigateByUrl');
    fixture = TestBed.createComponent(Register);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should not submit when the form is invalid', () => {
    component.form.patchValue({
      firstName: '',
      lastName: '',
      email: 'not-an-email',
      password: '',
      role: 1
    });

    component.onSubmit();

    expect(authService.register).not.toHaveBeenCalled();
    expect(component.form.controls.firstName.touched).toBe(true);
  });

  it('should register and navigate to login on success', () => {
    authService.register.mockReturnValue(
      of({ userId: 'user-1', email: 'student@example.com', role: 'Student' })
    );
    component.form.setValue({
      firstName: 'Naresh',
      lastName: 'Student',
      email: 'student@example.com',
      password: 'password',
      role: 1
    });

    component.onSubmit();

    expect(authService.register).toHaveBeenCalledWith({
      firstName: 'Naresh',
      lastName: 'Student',
      email: 'student@example.com',
      password: 'password',
      role: 1
    });
    expect(navigateByUrl).toHaveBeenCalledWith('/login');
  });

  it('should display API errors', () => {
    authService.register.mockReturnValue(
      throwError(() => ({ error: { error: 'Email already exists.' } }))
    );
    component.form.setValue({
      firstName: 'Naresh',
      lastName: 'Instructor',
      email: 'instructor@example.com',
      password: 'password',
      role: 2
    });

    component.onSubmit();

    expect(component.errorMessage).toBe('Email already exists.');
    expect(component.isSubmitting).toBe(false);
  });
});
