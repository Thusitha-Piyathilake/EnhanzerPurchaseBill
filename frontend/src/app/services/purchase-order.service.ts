import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { PurchaseOrder } from '../models/purchase-order';

@Injectable({
  providedIn: 'root'
})
export class PurchaseOrderService {

  private apiUrl =
    'http://localhost:5145/api/PurchaseOrders';

  constructor(private http: HttpClient) {}

  getAll(): Observable<PurchaseOrder[]> {
    return this.http.get<PurchaseOrder[]>(
      this.apiUrl
    );
  }

  getById(id: number): Observable<PurchaseOrder> {
    return this.http.get<PurchaseOrder>(
      `${this.apiUrl}/${id}`
    );
  }

  create(itemIds: number[]): Observable<PurchaseOrder> {
    return this.http.post<PurchaseOrder>(
      this.apiUrl,
      {
        itemIds
      }
    );
  }
}