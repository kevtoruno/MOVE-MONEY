import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { HttpClient, HttpHeaders, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../_models/User'
import { Agency, AgencyParams } from '../_models/Agency'
import { Customer, CustomerParams } from 'app/_models/Customer';
import { map } from 'rxjs/operators';
import { OrderToProcess } from 'app/_models/OrderToProcess';

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
    return this.http.get<number>(`${this.baseUrl}orders/comission?senderId=${senderId}&recipientId=${recipientId
      }&amount=${amount}`);
  }

  createOrder(order: OrderToProcess) {
    return this.http.post(`${this.baseUrl}orders`, order);
  }

}

