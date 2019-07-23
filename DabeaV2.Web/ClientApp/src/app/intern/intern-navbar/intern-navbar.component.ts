import { Component, OnInit, Inject } from '@angular/core';
import { AuthService } from '../../shared/services/auth.service';
import { HttpClient } from '@angular/common/http';


interface ITest {

}

@Component({
  selector: 'cz-intern-navbar',
  templateUrl: './intern-navbar.component.html',
  styleUrls: ['./intern-navbar.component.scss']
})
export class InternNavbarComponent implements OnInit {
  navbarOpen = false;
  constructor(private authService: AuthService, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  ngOnInit() {
  }

  toggleNavbar() {
    this.navbarOpen = !this.navbarOpen; 
  }

  onClickLogout() {
    this.authService.logout();
  }

  onClickTest() {
    //throw new Error("Na Nö");

    this.http.post<ITest>(this.baseUrl + 'api/Test/Test1', { }).subscribe(response => {
      console.log("Response:", response);
    });
  }


}
