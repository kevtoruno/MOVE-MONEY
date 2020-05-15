import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { HttpClient, HttpHeaders, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Customer} from '../_models/Customer'
import { TypeIdentification} from '../_models/TypeIdentification'

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  baseUrl = environment.apiUrl;

constructor(private http: HttpClient) { }

  getCustomers(): Observable<Customer[]> {
    return this.http.get<Customer[]>(`${this.baseUrl}customers`);
  }

  createCustomer(customer: Customer ) {
    return this.http.post(`${this.baseUrl}customers`, customer);
  }
}
