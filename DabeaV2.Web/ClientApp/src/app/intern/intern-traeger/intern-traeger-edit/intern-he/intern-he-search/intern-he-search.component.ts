import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'cz-intern-he-search',
  templateUrl: './intern-he-search.component.html',
  styleUrls: ['./intern-he-search.component.scss']
})
export class InternHeSearchComponent implements OnInit, OnDestroy {

  tId?: number;
  hId?: number;

  sub: Subscription;

  get queryParams() {
    return { tId: this.tId, hId: this.hId ? this.hId : 2 };
  }

  constructor(private route: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    this.sub = this.route.queryParams.subscribe(params => {
      this.tId = +params['tId'] || null;
      this.hId = +params['hId'] || null;

      if (this.hId && this.hId > 0) {
        this.router.navigate(['/intern/traeger/edit/he/edit'], { queryParams: this.queryParams });
      }

    });
  }

  ngOnDestroy(): void {
    if (this.sub) {
      this.sub.unsubscribe();
    }
  }
}
