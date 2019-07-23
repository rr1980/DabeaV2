import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { InternTraegerSearchComponent } from './intern-traeger-search/intern-traeger-search.component';
import { AuthInternGuard } from '../../shared/helper/auth-intern.guard';
import { InternTraegerComponent } from './intern-traeger.component';

//function getData(): string {
//  console.debug("+++");
//  return "edit";
//}

const routes: Routes = [
  {
    path: '', component: InternTraegerComponent,
    children: [
      { path: 'search', component: InternTraegerSearchComponent, children:[] },
      { path: 'edit', loadChildren: "./intern-traeger-edit/intern-traeger-edit.module#InternTraegerEditModule", canLoad: [AuthInternGuard], canActivate: [AuthInternGuard] },
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
  InternTraegerSearchComponent,
];
