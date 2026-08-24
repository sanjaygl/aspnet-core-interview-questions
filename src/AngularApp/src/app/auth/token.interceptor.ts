import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, switchMap, throwError } from 'rxjs';

export const tokenInterceptor: HttpInterceptorFn = (req, next) => {
  const http = inject(HttpClient);

  const cookieReq = req.clone({
    withCredentials: true
  });

  return next(cookieReq).pipe(
    catchError((error) => {
      // If the request that failed with 401 was already the refresh endpoint, STOP immediately!
      if (req.url.includes('/api/auth/refresh')) {
        console.error('Refresh token cookie is also invalid or dead. Force user to log in again.');
        return throwError(() => error);
      }

      if (error instanceof HttpErrorResponse && error.status === 401) {
        console.warn('Access Token cookie expired. Running silent background cookie rotation...');

        return http.post<any>('https://localhost:44351/api/auth/refresh', {}, { withCredentials: true }).pipe(
          switchMap(() => {
            console.log('Cookies rotated successfully! Retrying original user request.');
            return next(cookieReq);
          }),
          catchError((refreshErr) => {
            return throwError(() => refreshErr);
          })
        );
      }
      return throwError(() => error);
    })
  );
};
