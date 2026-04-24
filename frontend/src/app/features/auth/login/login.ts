import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from '../../../core/auth/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './login.html',
  styleUrl: './login.scss'
})
export class Login {
  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);

  errorMessage = '';
  isSubmitting = false;

  form = this.fb.nonNullable.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required]]
  });

  onSubmit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.errorMessage = '';
    this.isSubmitting = true;

    /*
    this.authService.login(this.form.getRawValue()).subscribe({
      next: response => {
        this.isSubmitting = false;

        if (response.role === 'Instructor' || response.role === 'Admin') {
          this.router.navigateByUrl('/courses/create');
          return;
        }

        this.router.navigateByUrl('/courses');
      },*/
    this.authService.login(this.form.getRawValue()).subscribe({
      next: (response) => {
        this.isSubmitting = false;
        const returnUrl = this.route.snapshot.queryParamMap.get('returnUrl');

        if (returnUrl) {
          this.router.navigateByUrl(returnUrl);
          return;
        }
        // as a fallback
        if (response.role === 'Instructor' || response.role === 'Admin') {
          this.router.navigateByUrl('/courses/create');
          return;
        }

        this.router.navigateByUrl('/courses');
      },
      error: err => {
        this.isSubmitting = false;
        this.errorMessage =
          err?.error?.error ?? 'Login failed. Please check your credentials.';
      }
    });
  }
}
