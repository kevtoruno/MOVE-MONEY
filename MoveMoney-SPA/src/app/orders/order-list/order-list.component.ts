import { Component, OnInit } from '@angular/core';
import { OrderService } from 'app/_services/order.service';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from 'app/_services/auth.service';
import { OrderToList } from 'app/_models/OrderToList';
import { UserService } from 'app/_services/user.service';

@Component({
  selector: 'app-order-list',
  templateUrl: './order-list.component.html',
  styleUrls: ['./order-list.component.scss']
})
export class OrderListComponent implements OnInit {
  ordersToList: OrderToList[];

  constructor(private route: ActivatedRoute, private authService: AuthService,
              private userService: UserService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.ordersToList = data.orders;
    })

    this.updateMoney();
  }

  updateMoney() {
    const userId = JSON.parse(localStorage.getItem('user'));

    this.userService.getUser(userId.id).subscribe(data => {
      this.authService.updateMoney(data.money);
    });
  }
}
