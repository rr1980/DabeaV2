import { Component, OnInit, Inject, Input } from '@angular/core';
import { EntityType } from '../../enums/entityType.enum';
import { NameService, INamePersonComponentModel } from './name.service';
import { IComponentModel } from '../../models/component.model';



@Component({
  selector: 'cz-name',
  templateUrl: './name.component.html',
  styleUrls: ['./name.component.scss']
})
export class NameComponent implements OnInit {

  @Input() entityType: EntityType;
  @Input() id: number;

  data: IComponentModel<INamePersonComponentModel> = {
    result: {
      name: "",
      vorName: "",
      fullName: "" 
    } as INamePersonComponentModel
  } as IComponentModel<INamePersonComponentModel>;

  constructor(private nameService: NameService) { }
   
  ngOnInit() { 
    switch (this.entityType) {
      case EntityType.Person:
        this.nameService.getPerson(this.id).subscribe(response => {
          this.data = response;
        });
        break; 
      default:
    }
  }
}
