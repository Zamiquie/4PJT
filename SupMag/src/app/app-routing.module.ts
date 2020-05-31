import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
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
  {
    path: 'advertising',
    loadChildren: () => import('./Pages/advertising/advertising.module').then( m => m.AdvertisingPageModule)
  },
  {
    path: 'scan',
    loadChildren: () => import('./Pages/scan/scan.module').then( m => m.ScanPageModule)
  },
  {
    path: 'shopping',
    loadChildren: () => import('./Pages/shopping/shopping.module').then( m => m.ShoppingPageModule)
  },
  {
    path: 'profile',
    loadChildren: () => import('./Pages/profile/profile.module').then( m => m.ProfilePageModule)
  },

];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
