import { Component, OnInit } from '@angular/core';
import { CustomerService } from 'app/_services/customer.service';
import { Customer } from 'app/_models/Customer';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-customer',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.scss']
})
export class CustomerComponent implements OnInit {
  customers: Customer[];
  constructor(private customerService: CustomerService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.customers = data.customers;
    });
  }

}
