import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { HttpClient, HttpHeaders, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Customer } from '../_models/Customer'
import { TypeIdentification } from '../_models/TypeIdentification'
import { PaginatedResult } from 'app/_models/Pagination';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getCustomers(page?, itemsPerPage?): Observable<PaginatedResult<Customer[]>> {
    const paginatedResult: PaginatedResult<Customer[]> = new PaginatedResult<Customer[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http.get<Customer[]>(`${this.baseUrl}customers`, { observe: 'response', params })
      .pipe(
        map(response => {
          paginatedResult.result = response.body;
          if (response.headers.get('Pagination') != null) {
            paginatedResult.pagination = JSON.parse(response.headers.get('pagination'));
          }
          return paginatedResult;
        })
      );
  }

  createCustomer(customer: Customer) {
    return this.http.post(`${this.baseUrl}customers`, customer);
  }
}
