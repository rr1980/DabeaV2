import { Component, OnInit, Inject, Input } from '@angular/core';
import { IBaseComponentModel, IComponentModel } from '../../models/component.model';
import { HttpClient } from '@angular/common/http';
import { EntityType } from '../../enums/entityType.enum';

interface INameComponentModel extends IBaseComponentModel {
  name: string;
  vorName: string;
  fullName: string;

}

@Component({
  selector: 'cz-name',
  templateUrl: './name.component.html',
  styleUrls: ['./name.component.scss']
})
export class NameComponent implements OnInit {

  @Input() entityType: EntityType;
  @Input() id: number;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  ngOnInit() {

    this.load();
  }

  load() {
    var request = { id: this.id, entityType: this.entityType } as IComponentModel<INameComponentModel>;

    this.http.post<IComponentModel<INameComponentModel>>(this.baseUrl + 'api/NameComponent/Get', request).subscribe(response => {
      console.log("Response:", response);
    });
  }
}
