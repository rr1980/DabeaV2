import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ExternRoutingModule, routedComponents } from './extern-routing.module';

@NgModule({
  imports: [
    CommonModule,
    ExternRoutingModule
  ],
  declarations: [
    routedComponents
  ]
})
export class ExternModule { }
