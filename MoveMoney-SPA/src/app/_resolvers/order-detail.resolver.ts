import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { OrderToDetail } from 'app/_models/OrderToDetail';
import { OrderService } from 'app/_services/order.service';

@Injectable()

export class OrderDetailResolver implements Resolve<OrderToDetail>{
    pageNumber = 1;
    pageSize = 10;

    constructor(private orderService: OrderService, private router: Router)  { }

    resolve(route: ActivatedRouteSnapshot): Observable<OrderToDetail> {
        return this.orderService.getOrder(route.params.id).pipe(
            catchError(error => {
                this.router.navigate(['']);
                return of(null);
            })
        );
    }
}
