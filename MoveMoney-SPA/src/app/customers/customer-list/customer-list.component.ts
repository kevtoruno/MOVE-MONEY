import { Component, OnInit } from '@angular/core';
import { CustomerService } from 'app/_services/customer.service';
import { Customer } from 'app/_models/Customer';
import { ActivatedRoute } from '@angular/router';
import { Pagination, PaginatedResult } from 'app/_models/Pagination';
import { Agency } from 'app/_models/Agency';
import { PageEvent, MatPaginatorIntl } from '@angular/material/paginator';
import { PaginatorOptions } from 'app/_helpers/PaginatorOptions';
import { AuthService } from 'app/_services/auth.service';
import { UserService } from 'app/_services/user.service';

@Component({
  selector: 'app-customer',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.scss'],
  providers: [
    {
      provide: MatPaginatorIntl,
      useClass: PaginatorOptions,
    }]
})
export class CustomerComponent implements OnInit {
  customers: Customer[];
  pagination: Pagination;
  pageSizeOptions: number[] = [10, 20, 50];
  constructor(private paginatorOptions: MatPaginatorIntl, private customerService: CustomerService, private route: ActivatedRoute,
    private authService: AuthService, private userService: UserService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.customers = data.customers.result;
      this.pagination = data.customers.pagination;
    });
    this.paginatorOptions.itemsPerPageLabel = 'Customers per page';

    this.updateMoney();
  }
  pageChanged(e: PageEvent) {
    this.pagination.currentPage = e.pageIndex + 1;
    this.pagination.itemsPerPage = e.pageSize;
    this.loadCustomers();
  }

  loadCustomers() {
    this.customerService.getCustomers(this.pagination.currentPage, this.pagination.itemsPerPage)
      .subscribe((data: PaginatedResult<Customer[]>) => {
        this.pagination = data.pagination;
        this.customers = data.result;
      });
  }

  updateMoney() {
    const userId = JSON.parse(localStorage.getItem('user'));

    this.userService.getUser(userId.id).subscribe(data => {
      this.authService.updateMoney(data.money);
    });
  }
}
