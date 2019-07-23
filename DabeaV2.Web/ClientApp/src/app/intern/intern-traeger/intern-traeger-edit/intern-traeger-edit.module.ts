import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InternTraegerEditRoutingModule, routedComponents } from './intern-traeger-edit-routing.module';
import { SharedModule } from '../../../shared/modules/shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    InternTraegerEditRoutingModule,
    SharedModule
  ],
  declarations: [routedComponents]
})
export class InternTraegerEditModule { }
