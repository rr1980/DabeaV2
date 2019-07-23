import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InternTraegerRoutingModule, routedComponents } from './intern-traeger-routing.module';
import { SharedModule } from '../../shared/modules/shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    InternTraegerRoutingModule,
    SharedModule
  ],
  declarations: [routedComponents]
})
export class InternTraegerModule { }
