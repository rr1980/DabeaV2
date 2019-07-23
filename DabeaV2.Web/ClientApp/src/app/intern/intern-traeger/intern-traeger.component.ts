import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

interface ITraegerNameViewModel {
  name: string;
}

@Component({
  selector: 'cz-intern-traeger',
  templateUrl: './intern-traeger.component.html',
  styleUrls: ['./intern-traeger.component.scss']
})
export class InternTraegerComponent implements OnInit {

  data: ITraegerNameViewModel = {
    name:""
  } as ITraegerNameViewModel;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  ngOnInit() {
    this.http.post<ITraegerNameViewModel>(this.baseUrl + 'api/Traeger/Get_Name', { id: 1 }).subscribe(response => {
      this.data = response;
    })
  }

}
