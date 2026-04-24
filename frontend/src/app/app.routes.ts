import { Routes } from '@angular/router';
import { Login } from './features/auth/login/login';
import { Register } from './features/auth/register/register';
import { CourseList } from './features/courses/course-list/course-list';
import { CourseCreate } from './features/courses/course-create/course-create';
import { Home } from './features/dashboard/home/home';
import { authGuard } from './core/guards/auth-guard';
import { instructorGuard } from './core/guards/instructor-guard';

/*
export const routes: Routes = [
  { path: '', component: Home },
  { path: 'login', component: Login },
  { path: 'register', component: Register },
  { path: 'courses', component: CourseList },
  {
    path: 'courses/create',
    component: CourseCreate,
    canActivate: [authGuard, instructorGuard]
  },
  { path: '**', redirectTo: '' }
];
*/
export const routes: Routes = [
  { path: '', redirectTo: 'courses', pathMatch: 'full' },
  { path: 'login', component: Login },
  { path: 'courses', component: CourseList },
  {
    path: 'courses/create',
    component: CourseCreate,
    canActivate: [authGuard, instructorGuard]
  }
];