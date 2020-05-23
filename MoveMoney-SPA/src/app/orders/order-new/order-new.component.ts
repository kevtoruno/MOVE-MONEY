import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable, of, } from 'rxjs';
import { map, startWith, debounceTime, switchMap, tap, catchError } from 'rxjs/operators';
import { OrderService } from 'app/_services/order.service';
import { Customer, Customers } from 'app/_models/Customer';
import { Agencies } from 'app/_models/Agency';

@Component({
  selector: 'app-order-new',
  templateUrl: './order-new.component.html',
  styleUrls: ['./order-new.component.scss']
})
export class OrderNewComponent implements OnInit {
  public filteredSender: Observable<Customers> = null;
  public senderControl = new FormControl();

  public filteredReceiver: Observable<Customers> = null;
  public receiverControl = new FormControl();

  public filteredAgency: Observable<Agencies> = null;
  public agencyControl = new FormControl();

  isProcessed: boolean;

  constructor(private orderService: OrderService) { }

  ngOnInit() {
    this.filteredSender = this.senderControl.valueChanges.pipe(
      startWith(''),
      debounceTime(300),
      switchMap(value => {
        if (value !== '') {
          return this.lookupCustomer(value);
        } else {
          return of(null);
        }
      })
    );

    this.filteredReceiver = this.receiverControl.valueChanges.pipe(
      startWith(''),
      debounceTime(300),
      switchMap(value => {
        if (value !== '') {
          return this.lookupCustomer(value);
        } else {
          return of(null);
        }
      })
    );

    this.filteredAgency = this.agencyControl.valueChanges.pipe(
      startWith(''),
      debounceTime(300),
      switchMap(value => {
        if (value !== '') {
          return this.lookupAgency(value);
        } else {
          return of(null);
        }
      })
    );
  }

  changeState() {
    this.isProcessed = true;
  }
  orderCancelled() {
    this.isProcessed = false;
  }

  lookupCustomer(value: any): Observable<Customers> {
    console.log(value);
    if (typeof (value) === 'string') {
      return this.orderService.getCustomersAC(value.toLowerCase()).pipe(
        map(results => {
          return results;
        }),
        catchError(_ => {
          return of(null);
        })
      )
    } else {
      return this.orderService.getCustomersAC(value.firstName + ' ' + value.lastName).pipe(
        map(results => {
          return results;
        }),
        catchError(_ => {
          return of(null);
        })
      )
    }

  }

  lookupAgency(value: any): Observable<Customers> {
    console.log(value);
    if (typeof (value) === 'string') {
      return this.orderService.getAgencyAC(value.toLowerCase()).pipe(
        map(results => {
          console.log(results);
          return results;
        }),
        catchError(_ => {
          return of(null);
        })
      )
    } else {
      return this.orderService.getAgencyAC(value.agencyName).pipe(
        map(results => {
          return results;
        }),
        catchError(_ => {
          return of(null);
        })
      )
    }

  }
  displayFn(customer): string {
    return customer ? customer.firstName + ' ' + customer.lastName : null;
  }

  displayFnAgency(agency): string {
    return agency ? agency.agencyName + '-' + agency.agencyType : null;
  }
}
