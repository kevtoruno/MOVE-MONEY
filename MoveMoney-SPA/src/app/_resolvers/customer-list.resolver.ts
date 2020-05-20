import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Customer } from '../_models/Customer';
import { CustomerService } from '../_services/customer.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()

export class CustomerListResolver implements Resolve<Customer[]>{
    pageNumber = 1;
    pageSize = 10;

    constructor(private customerService: CustomerService, private router: Router)  { }

    resolve(route: ActivatedRouteSnapshot): Observable<Customer[]> {
        return this.customerService.getCustomers(this.pageNumber, this.pageSize).pipe(
            catchError(error => {
                this.router.navigate(['']);
                return of(null);
            })
        );
    }
}
