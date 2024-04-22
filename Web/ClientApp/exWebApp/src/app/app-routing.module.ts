import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserComponent } from './component/user/user.component';
import { LoginComponent } from './component/login/login.component';
import { HomeComponent } from './component/home/home.component';

const routes: Routes = [ 
  { path: 'home', component: HomeComponent },
  { path: 'user', component: UserComponent },
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'login', component: LoginComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
