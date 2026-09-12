import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { PurchaseBillItem } from '../models/purchase-bill-item';

@Injectable({
  providedIn: 'root'
})
export class PurchaseBillItemService {

  private apiUrl = 'http://localhost:5145/api/PurchaseBillItems';

  constructor(private http: HttpClient) {}

  getAll(): Observable<PurchaseBillItem[]> {
    return this.http.get<PurchaseBillItem[]>(this.apiUrl);
  }

  getById(id: number): Observable<PurchaseBillItem> {
    return this.http.get<PurchaseBillItem>(`${this.apiUrl}/${id}`);
  }

  create(item: PurchaseBillItem): Observable<PurchaseBillItem> {
    return this.http.post<PurchaseBillItem>(this.apiUrl, item);
  }

  update(id: number, item: PurchaseBillItem): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, item);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}