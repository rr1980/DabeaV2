import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { InternComponent } from './intern.component';
import { InternHomeComponent } from './intern-home/intern-home.component';
import { InternNavbarComponent } from './intern-navbar/intern-navbar.component';
import { InternFooterComponent } from './intern-footer/intern-footer.component';
import { AuthInternGuard } from '../shared/helper/auth-intern.guard';


const routes: Routes = [
  {
    path: '', component: InternComponent,
    children: [
      { path: '', redirectTo: 'home', pathMatch: 'full' },
      { path: 'home', component: InternHomeComponent },
      { path: 'traeger', loadChildren: "./intern-traeger/intern-traeger.module#InternTraegerModule", canLoad: [AuthInternGuard], canActivate: [AuthInternGuard] },
      { path: '**', redirectTo: 'home' }
    ]
  },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class InternRoutingModule { }

export const routedComponents = [
  InternComponent,
  InternNavbarComponent,
  InternHomeComponent,
  InternFooterComponent
];
