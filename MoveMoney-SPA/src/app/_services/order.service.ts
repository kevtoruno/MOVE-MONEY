import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { HttpClient, HttpHeaders, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../_models/User'
import { Agency, AgencyParams } from '../_models/Agency'
import { Customer, CustomerParams } from 'app/_models/Customer';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getCustomersAC(names: string): Observable<CustomerParams> {
    return this.http.get<CustomerParams>(`${this.baseUrl}orders/customer/autocomplete`, {
      observe: 'response',
      params: {
        names : names
      }
    })
    .pipe(
      map(res => {
        return res.body;
      })
    );
  }

  getAgencyAC(name: string): Observable<AgencyParams>{
    return this.http.get<AgencyParams>(`${this.baseUrl}orders/agency/autocomplete`, {
      observe: 'response',
      params: {
        name : name
      }
    })
    .pipe(
      map(res => {
        return res.body;
      })
    );
  }

}

