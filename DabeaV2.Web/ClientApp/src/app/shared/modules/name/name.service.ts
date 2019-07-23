import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IBaseComponentModel, IComponentModel } from '../../models/component.model';
import { EntityType } from '../../enums/entityType.enum';
import { Observable } from 'rxjs';

export class INamePersonComponentModel extends IBaseComponentModel {
  name: string;
  vorName: string;
  fullName: string;
}

export class INameBenutzerComponentModel extends INamePersonComponentModel {
  userName: string;
}

@Injectable()
export class NameService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getPerson(id: number): Observable<IComponentModel<INamePersonComponentModel>> {
    var request = { id, entityType: EntityType.Person } as IComponentModel<INamePersonComponentModel>;
    return this.http.post<IComponentModel<INamePersonComponentModel>>(this.baseUrl + 'api/NameComponent/Get_Person', request);
  }

  getCurrentBenutzer(): Observable<IComponentModel<INameBenutzerComponentModel>> {
    var request = { entityType: EntityType.Benutzer } as IComponentModel<INameBenutzerComponentModel>;
    return this.http.post<IComponentModel<INameBenutzerComponentModel>>(this.baseUrl + 'api/NameComponent/Get_Benutzer', request);
  }
}
