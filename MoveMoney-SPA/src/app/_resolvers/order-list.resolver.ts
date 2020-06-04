import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { OrderToList } from 'app/_models/OrderToList';
import { OrderService } from 'app/_services/order.service';

@Injectable()

export class OrderListResolver implements Resolve<OrderToList[]>{
    pageNumber = 1;
    pageSize = 10;

    constructor(private orderService: OrderService, private router: Router)  { }

    resolve(route: ActivatedRouteSnapshot): Observable<OrderToList[]> {
        return this.orderService.getOrders().pipe(
            catchError(error => {
                this.router.navigate(['']);
                return of(null);
            })
        );
    }
}
