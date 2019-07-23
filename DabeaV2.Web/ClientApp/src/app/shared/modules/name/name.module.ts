import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NameComponent } from './name.component';
import { NameService } from './name.service';
import { NameProfileComponent } from './name-profile/name-profile.component';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [
    NameComponent,
    NameProfileComponent
  ],
  providers: [NameService],
  exports: [
    NameComponent,
    NameProfileComponent
  ]
})
export class NameModule { }
