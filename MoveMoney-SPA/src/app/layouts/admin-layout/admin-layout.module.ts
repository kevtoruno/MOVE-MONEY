import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AdminLayoutRoutes } from './admin-layout.routing';
import { DashboardComponent } from '../../dashboard/dashboard.component';
import { IconsComponent } from '../../icons/icons.component';
import { NotificationsComponent } from '../../notifications/notifications.component';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatRippleModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatPaginatorModule, MatPaginatorIntl } from '@angular/material/paginator';
import { MatTooltipModule } from '@angular/material/tooltip';
import {MatGridListModule} from '@angular/material/grid-list';

import {MatRadioModule} from '@angular/material/radio';
import { MatSelectModule } from '@angular/material/select';
import {MatAutocompleteModule} from '@angular/material/autocomplete';

import { CustomerComponent } from 'app/customers/customer-list/customer-list.component';
import { CustomerNewComponent } from 'app/customers/customer-new/customer-new.component';
import { CustomerListResolver } from 'app/_resolvers/customer-list.resolver';
import { UserListComponent } from 'app/users/user-list/user-list.component';
import { UserListResolver } from 'app/_resolvers/user-list.resolver';
import { PaginatorOptions } from '../../_helpers/PaginatorOptions'
import { AgencyDetailComponent } from 'app/Agencies/Agency-detail/Agency-detail.component';
import { OrderNewComponent } from 'app/orders/order-new/order-new.component';
import { TypeaheadModule } from 'ngx-bootstrap/typeahead';
import { OrderResumeComponent } from 'app/orders/order-resume/order-resume.component';
import { OrderListComponent } from 'app/orders/order-list/order-list.component';
import { OrderListResolver } from 'app/_resolvers/order-list.resolver';
import { OrderDetailResolver } from 'app/_resolvers/order-detail.resolver';
import { OrderDetailComponent } from 'app/orders/order-detail/order-detail.component';



@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(AdminLayoutRoutes),
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatGridListModule,
    MatRippleModule,
    MatAutocompleteModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatTooltipModule,
    MatPaginatorModule,
    MatRadioModule,
    TypeaheadModule.forRoot()
  ],
  declarations: [
    DashboardComponent,
    IconsComponent,
    NotificationsComponent,
    CustomerComponent,
    CustomerNewComponent,
    UserListComponent,
    AgencyDetailComponent,
    OrderNewComponent,
    OrderResumeComponent,
    OrderListComponent,
    OrderDetailComponent
  ],
  providers: [
    CustomerListResolver,
    UserListResolver,
    OrderListResolver,
    OrderDetailResolver
  ]
})

export class AdminLayoutModule { }
