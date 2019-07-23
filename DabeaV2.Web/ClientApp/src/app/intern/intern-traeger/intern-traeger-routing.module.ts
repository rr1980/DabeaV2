import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { InternTraegerSearchComponent } from './intern-traeger-search/intern-traeger-search.component';
import { AuthInternGuard } from '../../shared/helper/auth-intern.guard';

//function getData(): string {
//  console.debug("+++");
//  return "edit";
//}

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: '', redirectTo: 'search', pathMatch: 'full'
      },
      { path: 'search', component: InternTraegerSearchComponent },
      { path: 'edit', loadChildren: "./intern-traeger-edit/intern-traeger-edit.module#InternTraegerEditModule", canLoad: [AuthInternGuard], canActivate: [AuthInternGuard] },
      { path: '**', redirectTo: 'search' }
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
  InternTraegerSearchComponent,
];
