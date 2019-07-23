import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

interface ITraegerNameViewModel {
  name: string;
}

@Component({
  selector: 'cz-intern-traeger-edit',
  templateUrl: './intern-traeger-edit.component.html',
  styleUrls: ['./intern-traeger-edit.component.scss']
})
export class InternTraegerEditComponent implements OnInit {

  data: ITraegerNameViewModel = {
    name: ""
  } as ITraegerNameViewModel;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  ngOnInit() {
    this.http.post<ITraegerNameViewModel>(this.baseUrl + 'api/InternTraegerComponent/Get_Name', { id: 1 }).subscribe(response => {
      this.data = response;
    })
  }

}
