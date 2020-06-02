import { Component, OnInit, Input } from '@angular/core';
import { FormControl, FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Observable, of, observable, } from 'rxjs';
import { map, startWith, debounceTime, switchMap, tap, catchError } from 'rxjs/operators';
import { OrderService } from 'app/_services/order.service';
import { Customer, Customers } from 'app/_models/Customer';
import { Agencies } from 'app/_models/Agency';
import { RequireMatch as RequireMatch } from '../../_helpers/RequireMatch'
import { AuthService } from 'app/_services/auth.service';
import { ComponentsModule } from 'app/components/components.module';
import { OrderToProcess } from 'app/_models/OrderToProcess';
import { AlertifyService } from 'app/_services/alertify.service';

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

  // public calculatedComission: Observable<number>;
  public comissionControl = new FormControl('', [Validators.required]);
  isProcessed: boolean;
  amountComission: number;
  orderToReturn: any;
  constructor(private orderService: OrderService, private authService: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {

    this.initiateValidation();
    // Detecting value changes on the amount Control
    this.comissionControl.valueChanges.subscribe(value => {
      this.lookupComission(1, 3, value);
    })

    // Detecting value changes on the Sender field
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

    // Detecting value changes on the Receiver field
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

    // Detecting value changes on the Agency Destination field
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

  // Initializing an instance of every control in the form.
  initiateValidation() {

    this.orderForm = new FormGroup({
      deliveryType: new FormControl('Pick up'),
      comissionCalculated: new FormControl()
    });
    this.orderForm.addControl('senderId', this.senderControl);
    this.orderForm.addControl('recipientId', this.receiverControl);
    this.orderForm.addControl('amount', this.comissionControl);
    this.orderForm.addControl('agencyDestinationId', this.agencyControl);
  }
  public hasError = (controlName: string, errorName: string) => {
    return this.orderForm.controls[controlName].hasError(errorName);
  }

  // Function to intiialize the order, if the form validation is correct.
  createOrder() {
    const order = new OrderToProcess();
    if (this.orderForm.valid) {
      order.userId = JSON.parse(localStorage.getItem('user')).id;
      order.senderId = this.orderForm.value.senderId.id;
      order.recipientId = this.orderForm.value.recipientId.id;
      order.amount = this.orderForm.value.amount;
      order.agencyDestinationId = this.orderForm.value.agencyDestinationId.id;
      order.deliveryType = this.orderForm.value.deliveryType;
      order.comission = this.amountComission;

      this.orderService.createOrder(order).subscribe((data) => {
        this.orderToReturn = data;
        this.changeState();
      }, error => {
        this.alertify.error(error.error);
      });
    }
  }
  
  // Function to confirm that the order has been processed, and emmits its value to the order-resume component
  changeState() {
    this.isProcessed = true;
  }

  // Function that receives a communication from the child order-resume component, to check if the order was cancelled/modified
  orderCancelled() {
    this.isProcessed = false;
  }

  // Function that looks on the customer as Auto Complete
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
 // Function that looks on the agency as Auto Complete
  lookupAgency(value: any): Observable<Customers> {
    if (typeof (value) === 'string') {
      return this.orderService.getAgencyAC(value.toLowerCase()).pipe(
        map(results => {
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
  // Function that calculates the comission
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

  // This set a format to display in the customer field
  displayFn(customer): string {
    return customer ? customer.firstName + ' ' + customer.lastName : null;
  }

  // This set a format to display in the agency field
  displayFnAgency(agency): string {
    return agency ? agency.agencyName + '-' + agency.agencyType : null;
  }
}
