import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OrderToDetail } from 'app/_models/OrderToDetail';
import { UserService } from 'app/_services/user.service';
import { AuthService } from 'app/_services/auth.service';

@Component({
  selector: 'app-order-detail',
  templateUrl: './order-detail.component.html',
  styleUrls: ['./order-detail.component.scss']
})
export class OrderDetailComponent implements OnInit {
  order: OrderToDetail;
  constructor(private route: ActivatedRoute, private userService: UserService, private authService: AuthService) { }

  ngOnInit() {
    this.route.data.subscribe((data) => {
      this.order = data.order;
      console.log(this.order);
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
