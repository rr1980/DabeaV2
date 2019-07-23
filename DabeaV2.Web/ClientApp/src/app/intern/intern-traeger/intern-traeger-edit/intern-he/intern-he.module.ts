import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../../../shared/modules/shared/shared.module';
import { InternHeRoutingModule, routedComponents } from './intern-he-routing.module';

@NgModule({
  imports: [
    CommonModule,
    InternHeRoutingModule,
    SharedModule
  ],
  declarations: [routedComponents]
})
export class InternHeModule { }
