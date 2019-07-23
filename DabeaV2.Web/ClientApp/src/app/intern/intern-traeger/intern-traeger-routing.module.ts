import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { InternTraegerComponent } from './intern-traeger.component';


const routes: Routes = [
  {
    path: '', component: InternTraegerComponent,
    //children: [
    //  { path: '', component: InternTraegerComponent },
    //  { path: '**', redirectTo: 'home' }
    //]
  },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class InternTraegerRoutingModule { }

export const routedComponents = [
  InternTraegerComponent
];
