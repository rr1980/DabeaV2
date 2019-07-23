import { Component, OnInit } from '@angular/core';
import { InternTraegerService } from '../intern-traeger.service';

@Component({
  selector: 'cz-intern-traeger-search',
  templateUrl: './intern-traeger-search.component.html',
  styleUrls: ['./intern-traeger-search.component.scss']
})
export class InternTraegerSearchComponent implements OnInit {

  constructor(private internTraegerService: InternTraegerService) { }

  ngOnInit() {
  }

  onClickGo(id: number) {
    this.internTraegerService.go({ id: id, name: id.toString() });

    //this.addHe({ id: id, name: id.toString() } as IHeRouteModel);

  }
}
