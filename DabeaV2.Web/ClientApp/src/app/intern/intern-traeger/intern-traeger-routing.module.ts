import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { InternTraegerComponent } from './intern-traeger.component';
import { InternTraegerMainComponent } from './intern-traeger-main/intern-traeger-main.component';


const routes: Routes = [
  {
    path: '', component: InternTraegerComponent,
    children: [
      { path: '', redirectTo: 'main', pathMatch: 'full' },
      { path: 'main', component: InternTraegerMainComponent },
      { path: '**', redirectTo: 'main' }
    ]
  },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class InternTraegerRoutingModule { }

export const routedComponents = [
  InternTraegerComponent,
  InternTraegerMainComponent
];
