import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Router } from '@angular/router';

//interface IHesRouteModel {
//  hes: IHeRouteModel[];
//}

export interface ITraegerRouteModel {
  id: number;
  name: string;
  //queryParams: any;
}

@Injectable()
export class InternTraegerService {

  private traeger_container: BehaviorSubject<ITraegerRouteModel[]> = new BehaviorSubject<ITraegerRouteModel[]>([]);

  readonly traegerObs = this.traeger_container.asObservable();

  constructor(private router: Router) { }
  
  public go(traeger: ITraegerRouteModel) {

    var _t = this._traeger.find(__t => __t.id === traeger.id);
    if (!_t) {
      this.addTraeger(traeger);
      _t = traeger;
    }
    this.router.navigate(['/intern/traeger/edit/he/edit'], { queryParams: { tId: _t.id } });

  }

  private get _traeger(): ITraegerRouteModel[] {
    return this.traeger_container.getValue();
  }

  private set _traeger(val: ITraegerRouteModel[]) {
    this.traeger_container.next(val);
  }

  addTraeger(he: ITraegerRouteModel) {
    this._traeger = [
      ...this._traeger, he
    ];
  }

  removeTraeger(traeger: ITraegerRouteModel) {
    this._traeger = this._traeger.filter(_R => _R.id !== traeger.id);
  }
}
