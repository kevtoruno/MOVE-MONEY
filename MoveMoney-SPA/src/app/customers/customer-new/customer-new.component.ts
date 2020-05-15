import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { CustomerService } from 'app/_services/customer.service';
import { Customer } from 'app/_models/Customer';
import { AlertifyService } from 'app/_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-customer-new',
  templateUrl: './customer-new.component.html',
  styleUrls: ['./customer-new.component.scss']
})
export class CustomerNewComponent implements OnInit {
  customer: Customer;
  customerForm: FormGroup;

  constructor(private alertify: AlertifyService, private customerService: CustomerService,
    private fb: FormBuilder, private router: Router) { }

  ngOnInit() {
    this.initiateValidation();
  }

  initiateValidation() {
    this.customerForm = new FormGroup({
      firstName: new FormControl('', [Validators.required]),
      lastName: new FormControl('', [Validators.required]),
      typeIdentificationId: new FormControl('', [Validators.required]),
      identification: new FormControl('', [Validators.required]),
      city: new FormControl('', [Validators.required]),
      country: new FormControl('', [Validators.required]),
      phoneNumber: new FormControl('', [Validators.required]),
      dateOfBirth: new FormControl(),
      address: new FormControl()
    });
  }
  createCustomer() {
    if (this.customerForm.valid) {
      this.customer = this.customerForm.value;
      this.customerService.createCustomer(this.customer).subscribe(() => {
        this.alertify.success('Customer added successfully.');
        this.router.navigate(['/customers']);
      }, error => {
        console.log(error);
        this.alertify.warning(error.error);
      })
    } else {
      this.alertify.warning('Review the errors');
    }
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.customerForm.controls[controlName].hasError(errorName);
  }
}
