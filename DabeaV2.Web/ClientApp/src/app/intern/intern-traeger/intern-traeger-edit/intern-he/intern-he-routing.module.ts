import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthInternGuard } from '../../../../shared/helper/auth-intern.guard';
import { InternHeSearchComponent } from './intern-he-search/intern-he-search.component';


const routes: Routes = [
  {
    path: '', 
    children: [
      { path: '', redirectTo: 'search', pathMatch: 'full' },
      { path: 'search', component: InternHeSearchComponent },
      { path: 'edit', loadChildren: "./intern-he-edit/intern-he-edit.module#InternHeEditModule", canLoad: [AuthInternGuard], canActivate: [AuthInternGuard] },
      { path: '**', redirectTo: 'search' }
    ]
  },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class InternHeRoutingModule { }

export const routedComponents = [
  InternHeSearchComponent,
];
