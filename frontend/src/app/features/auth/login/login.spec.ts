import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ActivatedRoute, Router } from '@angular/router';
import { throwError } from 'rxjs';
import { AuthService } from '../../../core/auth/auth.service';

import { Login } from './login';

describe('Login', () => {
  let component: Login;
  let fixture: ComponentFixture<Login>;
  let authService: { login: ReturnType<typeof vi.fn> };

  beforeEach(async () => {
    authService = {
      login: vi.fn()
    };

    await TestBed.configureTestingModule({
      imports: [Login],
      providers: [
        { provide: AuthService, useValue: authService },
        { provide: Router, useValue: { navigateByUrl: vi.fn() } },
        {
          provide: ActivatedRoute,
          useValue: { snapshot: { queryParamMap: { get: () => null } } }
        }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(Login);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should render accessible validation messages when invalid', () => {
    component.onSubmit();
    fixture.detectChanges();

    const email = fixture.nativeElement.querySelector('#email') as HTMLInputElement;
    const password = fixture.nativeElement.querySelector('#password') as HTMLInputElement;

    expect(email.getAttribute('aria-invalid')).toBe('true');
    expect(email.getAttribute('aria-describedby')).toBe('login-email-error');
    expect(password.getAttribute('aria-invalid')).toBe('true');
    expect(fixture.nativeElement.textContent).toContain('Password is required.');
    expect(authService.login).not.toHaveBeenCalled();
  });

  it('should expose API errors as alerts', () => {
    authService.login.mockReturnValue(
      throwError(() => ({ error: { error: 'Login failed.' } }))
    );
    component.form.setValue({
      email: 'student@example.com',
      password: 'password'
    });

    component.onSubmit();
    fixture.detectChanges();

    const alert = fixture.nativeElement.querySelector('[role="alert"]') as HTMLElement;

    expect(alert.textContent).toContain('Login failed.');
    expect(alert.getAttribute('aria-live')).toBe('assertive');
  });
});
