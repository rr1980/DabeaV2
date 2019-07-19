import { Component, OnInit } from '@angular/core';
import { AuthService } from '../shared/services/auth.service';

@Component({
  selector: 'cz-extern',
  templateUrl: './extern.component.html',
  styleUrls: ['./extern.component.css']
})
export class ExternComponent implements OnInit {

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  onClickLogout() {
    this.authService.logout();
  }
}
