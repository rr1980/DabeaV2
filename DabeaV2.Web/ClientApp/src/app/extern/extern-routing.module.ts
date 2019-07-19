import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ExternComponent } from './extern.component';


const routes: Routes = [
  { path: '', component: ExternComponent },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ExternRoutingModule { }

export const routedComponents = [
  ExternComponent
];
