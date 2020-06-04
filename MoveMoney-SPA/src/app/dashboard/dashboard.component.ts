import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import * as Chartist from 'chartist';
import { AuthService } from 'app/_services/auth.service';
import { UserService } from 'app/_services/user.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  userId: any;
  constructor(private authService: AuthService, private userService: UserService) { }

  ngOnInit() {
    this.updateMoney();
  }

  updateMoney() {
    const userId = JSON.parse(localStorage.getItem('user'));

    this.userService.getUser(userId.id).subscribe(data => {
      this.authService.updateMoney(data.money);
    });
  }
}
