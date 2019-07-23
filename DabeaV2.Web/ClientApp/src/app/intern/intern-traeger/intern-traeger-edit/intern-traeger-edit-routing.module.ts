import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { InternTraegerEditComponent } from './intern-traeger-edit.component';
import { InternTraegerEditStammdatenComponent } from './intern-traeger-edit-stammdaten/intern-traeger-edit-stammdaten.component';
import { InternTraegerEditPersonalComponent } from './intern-traeger-edit-personal/intern-traeger-edit-personal.component';


const routes: Routes = [
  {
    path: '', component: InternTraegerEditComponent,
    children: [
      { path: '', redirectTo: 'stamm', pathMatch: 'full' },
      { path: 'stamm', component: InternTraegerEditStammdatenComponent },
      { path: 'personal', component: InternTraegerEditPersonalComponent },
      { path: '**', redirectTo: 'stamm' }
    ]
  },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class InternTraegerEditRoutingModule { }

export const routedComponents = [
  InternTraegerEditComponent,
  InternTraegerEditStammdatenComponent,
  InternTraegerEditPersonalComponent
];
