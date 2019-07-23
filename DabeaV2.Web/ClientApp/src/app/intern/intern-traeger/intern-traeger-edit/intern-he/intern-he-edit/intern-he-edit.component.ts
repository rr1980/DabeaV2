import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'cz-intern-he-edit',
  templateUrl: './intern-he-edit.component.html',
  styleUrls: ['./intern-he-edit.component.scss']
})
export class InternHeEditComponent implements OnInit, OnDestroy {

  tId?: number;
  hId?: number;

  sub: Subscription;

  get queryParams() {
    return { tId: this.tId, hId: this.hId };
  }

  get queryParamsWithoutHeId() {
    return { tId: this.tId };
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
