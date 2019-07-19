import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './shared/components/login/login.component';
import { AuthInternGuard } from './shared/guards/auth-intern.guard';
import { AuthExternGuard } from './shared/guards/auth-extern.guard';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'intern', loadChildren: "./intern/intern.module#InternModule", canLoad: [AuthInternGuard], canActivate: [AuthInternGuard] },
  { path: 'extern', loadChildren: "./extern/extern.module#ExternModule", canLoad: [AuthExternGuard], canActivate: [AuthExternGuard] },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: '**', redirectTo: '/login' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

export const routedComponents = [
  LoginComponent
];
