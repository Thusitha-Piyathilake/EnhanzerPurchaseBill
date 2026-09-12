import { Routes } from '@angular/router';

import { LoginComponent } from './login/login';
import { LocationDetailsComponent } from './location-details/location-details';
import { PurchaseBill } from './purchase-bill/purchase-bill';

import { authGuard } from './guards/auth-guard';

export const routes: Routes = [

  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },

  {
    path: 'login',
    component: LoginComponent
  },

  {
    path: 'location-details',
    component: LocationDetailsComponent
  },

  {
    path: 'purchase-bill',
    component: PurchaseBill,
    canActivate: [authGuard]
  },

  {
    path: '**',
    redirectTo: 'login'
  }

];