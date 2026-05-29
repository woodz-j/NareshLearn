import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/auth/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './register.html',
  styleUrl: './register.scss',
})
export class Register {
  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);

  readonly roleOptions = [
    { label: 'Student', value: 1 },
    { label: 'Instructor', value: 2 }
  ];

  errorMessage = '';
  isSubmitting = false;

  form = this.fb.nonNullable.group({
    firstName: ['', [Validators.required, Validators.maxLength(100)]],
    lastName: ['', [Validators.required, Validators.maxLength(100)]],
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required]],
    role: [1, [Validators.required]]
  });

  onSubmit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.errorMessage = '';
    this.isSubmitting = true;

    this.authService.register(this.form.getRawValue()).subscribe({
      next: () => {
        this.isSubmitting = false;
        this.router.navigateByUrl('/login');
      },
      error: err => {
        this.isSubmitting = false;
        this.errorMessage =
          err?.error?.error ?? 'Registration failed. Please check your details.';
      }
    });
  }
}
