import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../auth/auth.service';

export const instructorGuard: CanActivateFn = (state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  if (authService.isInstructorOrAdmin()) {
    return true;
  }

  //return router.createUrlTree(['/']);
  return router.createUrlTree(['/login'], {
    queryParams: { returnUrl: state.url }
  });
};