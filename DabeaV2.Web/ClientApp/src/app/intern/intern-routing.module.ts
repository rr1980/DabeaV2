import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { InternComponent } from './intern.component';
import { InternHomeComponent } from './intern-home/intern-home.component';
import { InternNavbarComponent } from './intern-navbar/intern-navbar.component';
import { InternFooterComponent } from './intern-footer/intern-footer.component';


const routes: Routes = [
  {
    path: '', component: InternComponent,
    children: [
      { path: '', redirectTo: 'home' },
      { path: 'home', component: InternHomeComponent },
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
