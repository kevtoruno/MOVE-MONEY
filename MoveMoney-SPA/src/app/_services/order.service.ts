import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { HttpClient, HttpHeaders, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../_models/User'
import { Agency, AgencyParams } from '../_models/Agency'
import { Customer, CustomerParams } from 'app/_models/Customer';
import { map } from 'rxjs/operators';
import { OrderToProcess } from 'app/_models/OrderToProcess';
import { OrderToList } from 'app/_models/OrderToList';
import { OrderToDetail } from 'app/_models/OrderToDetail';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getCustomersAC(names: string): Observable<CustomerParams> {
    return this.http.get<CustomerParams>(`${this.baseUrl}customers/GetCustomerAutoComplete`, {
      observe: 'response',
      params: {
        names: names
      }
    })
      .pipe(
        map(res => {
          return res.body;
        })
      );
  }

  getAgencyAC(name: string): Observable<AgencyParams> {
    return this.http.get<AgencyParams>(`${this.baseUrl}orders/agency/autocomplete`, {
      observe: 'response',
      params: {
        name: name
      }
    })
      .pipe(
        map(res => {
          return res.body;
        })
      );
  }

  getComissionValue(senderId, recipientId, amount): Observable<number> {
    let params = new HttpParams();
    if (!amount) {
      amount = 1;
    }
    params = params.append('senderId', senderId);
    params = params.append('recipientId', recipientId);
    params = params.append('amount', amount);
    return this.http.get<number>(`${this.baseUrl}orders/comission?countrySenderId=${senderId}&countryRecipientId=${recipientId
      }&amount=${amount}`);
  }

  createOrder(order: OrderToProcess) {
    return this.http.post(`${this.baseUrl}orders`, order);
  }

  getOrders(): Observable<OrderToList[]> {
    return this.http.get<OrderToList[]>(`${this.baseUrl}orders`);
  }

  getOrder(id: number): Observable<OrderToDetail> {
    return this.http.get<OrderToDetail>(`${this.baseUrl}orders/${id}`);
  }

  processOrder(userId: number, id: number) {
    return this.http.post(`${this.baseUrl}orders/${userId}/${id}`, {});
  }

  getCountryId(agencyId: number) {
    return this.http.get(`${this.baseUrl}orders/country/${agencyId}`);
  }
}

