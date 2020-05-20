import { Routes } from '@angular/router';

import { DashboardComponent } from '../../dashboard/dashboard.component';
import { IconsComponent } from '../../icons/icons.component';
import { NotificationsComponent } from '../../notifications/notifications.component';
import { CustomerComponent } from 'app/customers/customer-list/customer-list.component';
import { CustomerNewComponent } from 'app/customers/customer-new/customer-new.component';
import { CustomerListResolver } from 'app/_resolvers/customer-list.resolver';
import { UserListComponent } from 'app/users/user-list/user-list.component';
import { UserListResolver } from 'app/_resolvers/user-list.resolver';
import { AgencyDetailComponent } from 'app/Agencies/Agency-detail/Agency-detail.component';

export const AdminLayoutRoutes: Routes = [
    // {
    //   path: '',
    //   children: [ {
    //     path: 'dashboard',
    //     component: DashboardComponent
    // }]}, {
    // path: '',
    // children: [ {
    //   path: 'userprofile',
    //   component: UserProfileComponent
    // }]
    // }, {
    //   path: '',
    //   children: [ {
    //     path: 'icons',
    //     component: IconsComponent
    //     }]
    // }, {
    //     path: '',
    //     children: [ {
    //         path: 'notifications',
    //         component: NotificationsComponent
    //     }]
    // }, {
    //     path: '',
    //     children: [ {
    //         path: 'maps',
    //         component: MapsComponent
    //     }]
    // }, {
    //     path: '',
    //     children: [ {
    //         path: 'typography',
    //         component: TypographyComponent
    //     }]
    // }, {
    //     path: '',
    //     children: [ {
    //         path: 'upgrade',
    //         component: UpgradeComponent
    //     }]
    // }
    {
        path: '', component: DashboardComponent
    },
    {
        path: 'users', component: UserListComponent,
        resolve: {users : UserListResolver}
    },
    {
        path: 'icons', component: IconsComponent
    },
    {
        path: 'notifications', component: NotificationsComponent
    },
    {
        path: 'customers', component: CustomerComponent,
        resolve: { customers: CustomerListResolver }
    },
    {
        path: 'agency', component: AgencyDetailComponent
    },

    { path: 'customers/new', component: CustomerNewComponent }
];
