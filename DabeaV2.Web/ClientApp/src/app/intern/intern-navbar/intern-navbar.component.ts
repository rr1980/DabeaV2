import { Component, OnInit, Inject } from '@angular/core';
import { AuthService } from '../../shared/services/auth.service';
import { HttpClient } from '@angular/common/http';
import { Router, NavigationError, NavigationEnd, NavigationStart } from '@angular/router';

@Component({
  selector: 'cz-intern-navbar',
  templateUrl: './intern-navbar.component.html',
  styleUrls: ['./intern-navbar.component.scss']
})
export class InternNavbarComponent implements OnInit {

  loading: boolean = false;
  navbarOpen = false;

  constructor(private authService: AuthService, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private router: Router) {

    this.router.events.subscribe((event) => {

      if (event instanceof NavigationStart) {
        this.loading = true;
      }

      if (event instanceof NavigationEnd) {
        this.loading = false;
      }

      if (event instanceof NavigationError) {
        this.loading = false;
      }
    });
  }

  ngOnInit() {
  }

  toggleNavbar() {
    this.navbarOpen = !this.navbarOpen; 
  }

  onClickLogout() {
    this.authService.logout();
  }
}
