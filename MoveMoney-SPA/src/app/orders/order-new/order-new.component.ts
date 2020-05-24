import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Observable, of, observable, } from 'rxjs';
import { map, startWith, debounceTime, switchMap, tap, catchError } from 'rxjs/operators';
import { OrderService } from 'app/_services/order.service';
import { Customer, Customers } from 'app/_models/Customer';
import { Agencies } from 'app/_models/Agency';
import { RequireMatch as RequireMatch } from '../../_helpers/RequireMatch'

@Component({
  selector: 'app-order-new',
  templateUrl: './order-new.component.html',
  styleUrls: ['./order-new.component.scss']
})
export class OrderNewComponent implements OnInit {
  orderForm: FormGroup;
  public filteredSender: Observable<Customers> = null;
  public senderControl = new FormControl(null, [Validators.required, RequireMatch]);

  public filteredReceiver: Observable<Customers> = null;
  public receiverControl = new FormControl(null, [Validators.required, RequireMatch]);

  public filteredAgency: Observable<Agencies> = null;
  public agencyControl = new FormControl(null, [Validators.required, RequireMatch]);

  public calculatedComission: Observable<number>;
  public comissionControl = new FormControl('', [Validators.required]);

  isProcessed: boolean;
  amountComission: number;
  constructor(private orderService: OrderService) { }

  ngOnInit() {
    this.initiateValidation();
    this.comissionControl.valueChanges.subscribe(value => {
      this.lookupComission(1, 3, value);
    })

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

  initiateValidation() {

    this.orderForm = new FormGroup({
      deliveryType: new FormControl(),
    });
    this.orderForm.addControl('senderId', this.senderControl);
    this.orderForm.addControl('recipientId', this.receiverControl)
    this.orderForm.addControl('amount', this.comissionControl);
    this.orderForm.addControl('agencyDestinationId', this.agencyControl);
  }
  public hasError = (controlName: string, errorName: string) => {
    return this.orderForm.controls[controlName].hasError(errorName);
  }
  
  createOrder() {
      console.log(this.orderForm.controls['senderId'].value.id);
      return console.log('It is valid!');
  }
  changeState() {
    this.isProcessed = true;
  }
  orderCancelled() {
    this.isProcessed = false;
  }

  lookupCustomer(value: any): Observable<Customers> {
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

  lookupComission(senderId: number, recipientId: number, amount: number): Observable<number> {
    senderId = 1;
    recipientId = 3;
    if (amount) {
      this.orderService.getComissionValue(senderId, recipientId, amount).subscribe(data => {
        this.amountComission = data;
      })
    }
    return null;
  }
  displayFn(customer): string {
    return customer ? customer.firstName + ' ' + customer.lastName : null;
  }

  displayFnAgency(agency): string {
    return agency ? agency.agencyName + '-' + agency.agencyType : null;
  }
}
