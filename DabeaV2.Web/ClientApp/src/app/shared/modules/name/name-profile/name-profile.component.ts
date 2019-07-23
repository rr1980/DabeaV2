import { Component, OnInit } from '@angular/core';
import { NameService, INameBenutzerComponentModel } from '../name.service';
import { IComponentModel } from '../../../models/component.model';

@Component({
  selector: 'cz-name-profile',
  templateUrl: './name-profile.component.html',
  styleUrls: ['./name-profile.component.scss']
})
export class NameProfileComponent implements OnInit {

  data: IComponentModel<INameBenutzerComponentModel> = {
    result: {
      name: "",
      vorName: "",
      fullName: "",
      userName: ""
    } as INameBenutzerComponentModel
  } as IComponentModel<INameBenutzerComponentModel>;

  constructor(private nameService: NameService) { }

  ngOnInit() {
    this.nameService.getCurrentBenutzer().subscribe(response => {
      this.data = response;
    });
  }
}
