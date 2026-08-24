import { HttpClient } from '@angular/common/http';
import { Component, inject, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AuthService } from './auth/auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  imports: [CommonModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  private authService = inject(AuthService);
  private http = inject(HttpClient);
  logs: any = null;

  onLogin() {
    this.authService.login('sbopche', 'admin@123').subscribe({
      next: (res) => this.logs = { status: 'Login Triggered', response: res },
      error: (err) => this.logs = { status: 'Login Error', error: err.error }
    });
  }

  fetchOrders() {
    // No headers are specified here; the browser attaches them implicitly via the interceptor configuration
    this.http.get('https://localhost:44351/api/order/my-orders').subscribe({
      next: (res) => this.logs = { status: 'Orders Fetched', data: res },
      error: (err) => this.logs = { status: 'Fetch Failed', error: err.message }
    });
  }
}