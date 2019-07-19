import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InternRoutingModule, routedComponents } from './intern-routing.module';

@NgModule({
  imports: [
    CommonModule,
    InternRoutingModule
  ],
  declarations: [
    routedComponents
  ]
})
export class InternModule { }
