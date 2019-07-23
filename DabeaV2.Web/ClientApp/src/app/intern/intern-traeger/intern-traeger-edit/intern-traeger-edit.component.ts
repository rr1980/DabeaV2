import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute, NavigationExtras } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { map } from 'rxjs/operators';

interface ITraegerNameViewModel {
  name: string;
}

@Component({
  selector: 'cz-intern-traeger-edit',
  templateUrl: './intern-traeger-edit.component.html',
  styleUrls: ['./intern-traeger-edit.component.scss']
})
export class InternTraegerEditComponent implements OnInit, OnDestroy {


  tId?: number;
  hId?: number;

  sub: Subscription;

  data: ITraegerNameViewModel = {
    name: ""
  } as ITraegerNameViewModel;


  get queryParams() {
    return { tId: this.tId, hId: this.hId };
  }

  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.sub = this.route.queryParams.subscribe(params => {
      this.tId = +params['tId'] || null;
      this.hId = +params['hId'] || null;
    });
  }

  ngOnDestroy(): void {
    if (this.sub) {
      this.sub.unsubscribe();
    }
  }

}
