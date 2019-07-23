import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { InternHeEditComponent } from './intern-he-edit.component';
import { InternHeEditStammComponent } from './intern-he-edit-stamm/intern-he-edit-stamm.component';
import { InternHeEditPersonalComponent } from './intern-he-edit-personal/intern-he-edit-personal.component';


const routes: Routes = [
  {
    path: '', component: InternHeEditComponent,
    children: [
      { path: '', redirectTo: 'stamm', pathMatch: 'full' },
      { path: 'stamm', component: InternHeEditStammComponent },
      { path: 'personal', component: InternHeEditPersonalComponent },
      { path: '**', redirectTo: 'stamm' } 
    ]
  },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class InternHeEditRoutingModule { }

export const routedComponents = [
  InternHeEditComponent,
  InternHeEditStammComponent,
  InternHeEditPersonalComponent
];
