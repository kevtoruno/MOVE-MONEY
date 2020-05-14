import { Component, OnInit } from '@angular/core';

declare const $: any;
declare interface RouteInfo {
    path: string;
    title: string;
    icon: string;
    class: string;
}
export const ROUTES: RouteInfo[] = [
    { path: '', title: 'Dashboard',  icon: 'dashboard', class: '' },
    { path: '/customers', title: 'Customers',  icon:'people', class: '' },
    { path: '/users', title: 'Users',  icon:'person', class: '' },
    { path: '/agency', title: 'Agencies',  icon:'business', class: '' },
    { path: '/orders', title: 'Orders',  icon:'receipt', class: '' },
];

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {
  menuItems: any[];

  constructor() { }

  ngOnInit() {
    this.menuItems = ROUTES.filter(menuItem => menuItem);
  }
  isMobileMenu() {
      if ($(window).width() > 991) {
          return false;
      }
      return true;
  };

  logout(){
    console.log("logged out!");
  }
}
