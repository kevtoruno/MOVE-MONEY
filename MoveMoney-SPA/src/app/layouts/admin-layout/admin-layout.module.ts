import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AdminLayoutRoutes } from './admin-layout.routing';
import { DashboardComponent } from '../../dashboard/dashboard.component';
import { IconsComponent } from '../../icons/icons.component';
import { NotificationsComponent } from '../../notifications/notifications.component';
import {MatButtonModule} from '@angular/material/button';
import {MatInputModule} from '@angular/material/input';
import {MatRippleModule} from '@angular/material/core';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatPaginatorModule, MatPaginatorIntl} from '@angular/material/paginator';
import {MatTooltipModule} from '@angular/material/tooltip';
import {MatSelectModule} from '@angular/material/select';
import { CustomerComponent } from 'app/customers/customer-list/customer-list.component';
import { CustomerNewComponent } from 'app/customers/customer-new/customer-new.component';
import { CustomerListResolver } from 'app/_resolvers/customer-list.resolver';
import { UserListComponent } from 'app/users/user-list/user-list.component';
import { UserListResolver } from 'app/_resolvers/user-list.resolver';
import { PaginatorOptions } from '../../_helpers/PaginatorOptions'
import { AgencyDetailComponent } from 'app/Agencies/Agency-detail/Agency-detail.component';
@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(AdminLayoutRoutes),
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatRippleModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatTooltipModule,
    MatPaginatorModule,
  ],
  declarations: [
    DashboardComponent,
    IconsComponent,
    NotificationsComponent,
    CustomerComponent,
    CustomerNewComponent,
    UserListComponent,
    AgencyDetailComponent
  ],
  providers: [
    CustomerListResolver,
    UserListResolver
  ]
})

export class AdminLayoutModule {}
