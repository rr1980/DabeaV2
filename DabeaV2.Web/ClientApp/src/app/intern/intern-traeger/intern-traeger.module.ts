import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InternTraegerRoutingModule, routedComponents } from './intern-traeger-routing.module';
import { SharedModule } from '../../shared/modules/shared/shared.module';
import { InternTraegerService } from './intern-traeger.service';

@NgModule({
  imports: [
    CommonModule,
    InternTraegerRoutingModule,
    SharedModule
  ],
  declarations: [routedComponents],
  providers: [InternTraegerService]
})
export class InternTraegerModule { }
