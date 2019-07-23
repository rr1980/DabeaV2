import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { InternTraegerService, ITraegerRouteModel } from './intern-traeger.service';


@Component({
  selector: 'cz-intern-traeger',
  templateUrl: './intern-traeger.component.html',
  styleUrls: ['./intern-traeger.component.scss']
})
export class InternTraegerComponent implements OnInit, OnDestroy {

  hes: ITraegerRouteModel[] = [];

  constructor(private internTraegerService: InternTraegerService,  private route: ActivatedRoute) { }

  ngOnInit() {
    this.internTraegerService.traegerObs.subscribe(data => {
      this.hes = data;
    });
  }

  ngOnDestroy(): void {

  }

  onClickGoHe(he: ITraegerRouteModel) {
    this.internTraegerService.go(he);
  }
}
