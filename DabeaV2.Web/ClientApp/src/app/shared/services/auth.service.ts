import { Injectable, Inject } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';

export class User {
  id: number;
  isExtern: boolean;
  username: string;
  password: string;
  firstName: string;
  lastName: string;
  token?: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private router: Router) {
  }

  public get currentUserValue(): User {
    return JSON.parse(sessionStorage.getItem('currentUser'));
  }

  login(username: string, password: string) {
    return this.http.post<User>(this.baseUrl + `api/account/login`, { username, password })
      .pipe(map(user => {
        sessionStorage.setItem('currentUser', JSON.stringify(user));
        return user;
      }));
  }

  logout() {
    sessionStorage.removeItem('currentUser');
    this.router.navigate(['/login']);
  }
}
