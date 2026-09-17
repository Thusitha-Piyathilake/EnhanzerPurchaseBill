import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import {
  LatestPurchaseOrder,
  OldestPurchaseOrderItem,
  ItemQuantity
} from '../models/dashboard';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  private apiUrl =
    'http://localhost:5145/api/Dashboard';

  constructor(private http: HttpClient) {}

  getLatestOrders(): Observable<LatestPurchaseOrder[]> {
    return this.http.get<LatestPurchaseOrder[]>(
      `${this.apiUrl}/latest-orders`
    );
  }

  getOldestItems(): Observable<OldestPurchaseOrderItem[]> {
    return this.http.get<OldestPurchaseOrderItem[]>(
      `${this.apiUrl}/oldest-items`
    );
  }

  getItemQuantities(): Observable<ItemQuantity[]> {
    return this.http.get<ItemQuantity[]>(
      `${this.apiUrl}/item-quantities`
    );
  }
}