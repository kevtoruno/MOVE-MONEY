import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { AlertifyService } from "app/_services/alertify.service";
import { AuthService } from "app/_services/auth.service";

declare const $: any;
declare interface RouteInfo {
  path: string;
  title: string;
  icon: string;
  class: string;
}
export const ROUTES: RouteInfo[] = [
  { path: "", title: "Dashboard", icon: "dashboard", class: "" },
  { path: "/customers", title: "Customers", icon: "people", class: "" },
  { path: "/users", title: "Users", icon: "person", class: "" },
  { path: "/agency", title: "Agency", icon: "business", class: "" },
  { path: "/orders", title: "Orders", icon: "receipt", class: "" },
];

@Component({
  selector: "app-sidebar",
  templateUrl: "./sidebar.component.html",
  styleUrls: ["./sidebar.component.css"],
})
export class SidebarComponent implements OnInit {
  menuItems: any[];

  constructor(
    private alertify: AlertifyService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit() {
    this.menuItems = ROUTES.filter((menuItem) => menuItem);
  }
  isMobileMenu() {
    if ($(window).width() > 991) {
      return false;
    }
    return true;
  }

  logout() {
    this.alertify.confirm(
      "Confirmation of log out",
      "Are you sure that you want to log out?",
      () => {
        localStorage.removeItem("token");
        localStorage.removeItem("user");
        this.authService.decodedToken = null;
        this.authService.currentUser = null;
        this.router.navigate(["login"]);
      }
    );
  }
}
