import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { LocationDetail } from '../models/location-detail';

@Injectable({
  providedIn: 'root'
})
export class LocationDetailService {

  private apiUrl = 'http://localhost:5145/api/LocationDetails';

  constructor(private http: HttpClient) {}

  getAll(): Observable<LocationDetail[]> {
    console.log('GET:', this.apiUrl);

    return this.http.get<LocationDetail[]>(this.apiUrl);
  }

  getById(id: number): Observable<LocationDetail> {
    return this.http.get<LocationDetail>(
      `${this.apiUrl}/${id}`
    );
  }

  create(location: {
    locationCode: string;
    locationName: string;
  }): Observable<LocationDetail> {

    console.log('POST:', location);

    return this.http.post<LocationDetail>(
      this.apiUrl,
      location
    );
  }

  update(
    id: number,
    location: {
      locationCode: string;
      locationName: string;
    }
  ): Observable<void> {

    return this.http.put<void>(
      `${this.apiUrl}/${id}`,
      location
    );
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(
      `${this.apiUrl}/${id}`
    );
  }

  sync(
    locations: {
      locationCode: string;
      locationName: string;
    }[]
  ): Observable<any> {

    return this.http.post(
      `${this.apiUrl}/sync`,
      locations
    );
  }
}