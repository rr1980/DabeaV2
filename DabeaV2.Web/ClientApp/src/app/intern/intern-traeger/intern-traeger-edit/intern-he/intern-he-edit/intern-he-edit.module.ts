import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InternHeEditRoutingModule, routedComponents } from './intern-he-edit-routing.module';
import { SharedModule } from '../../../../../shared/modules/shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    InternHeEditRoutingModule,
    SharedModule
  ],
  declarations: [routedComponents]
})
export class InternHeEditModule { }
