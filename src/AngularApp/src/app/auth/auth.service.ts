import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface AuthResponse {
  success: boolean;
  message: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private http = inject(HttpClient);
  private readonly API_URL = 'https://localhost:44351/api/auth';

  login(username: string, password: string): Observable<AuthResponse> {
    // 'withCredentials: true' tells the browser to accept and store the incoming Set-Cookie headers
    return this.http.post<AuthResponse>(
      `${this.API_URL}/login`, 
      { username, password }, 
      { withCredentials: true } 
    );
  }

  // Handled completely cookie-to-cookie with an empty body payload
  refreshTokens(): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(
      `${this.API_URL}/refresh`, 
      {}, 
      { withCredentials: true }
    );
  }
}
