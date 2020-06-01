import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CustomerPage } from './customer.page';

const routes: Routes = [
  {
    path: 'customer',
    component: CustomerPage,
    children:
      [
        {
          path: 'advertising',
          loadChildren: () => import('../advertising/advertising.module').then(m => m.AdvertisingPageModule)
        },
        {
          path: 'scan',
          loadChildren: () => import('../scan/scan.module').then(m => m.ScanPageModule)
        },
        {
          path: 'shopping',
          loadChildren: () => import('../shopping/shopping.module').then(m => m.ShoppingPageModule)
        },
        {
          path: 'profile',
          loadChildren: () => import('../profile/profile.module').then(m => m.ProfilePageModule)
        },
        {
          path: '',
          redirectTo: 'customer/advertising',
          pathMatch: 'full'
        }
      ]
  },
  {
    path: '',
    redirectTo: 'customer/advertising',
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CustomerPageRoutingModule {}
