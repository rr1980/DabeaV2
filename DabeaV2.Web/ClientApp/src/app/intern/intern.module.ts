import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InternRoutingModule, routedComponents } from './intern-routing.module';
import { SharedModule } from '../shared/modules/shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    InternRoutingModule,
    SharedModule
  ],
  declarations: [
    routedComponents
  ]
})
export class InternModule { }
