import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { CustomerPage} from "./Pages/customer/customer.page";

const routes: Routes = [
  {
    path: '',
    redirectTo: 'customer',
    pathMatch: 'full'
  },
  {
    path: 'home',
    loadChildren: () => import('./Pages/home/home.module').then(m => m.HomePageModule)
  },
  {
    path: 'login',
    loadChildren: () => import('./Pages/login/login.module').then( m => m.LoginPageModule)
  },
  {
    path: 'register',
    loadChildren: () => import('./Pages/register/register.module').then( m => m.RegisterPageModule)
  },
  {
    path: 'customer',
    loadChildren: () => import('./Pages/customer/customer.module').then( m => m.CustomerPageModule)
  },

];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
