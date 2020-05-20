import { Component, OnInit } from '@angular/core';
import { UserService } from 'app/_services/user.service';
import { User } from 'app/_models/User';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from 'app/_services/auth.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {
  users: User[];
  constructor(private userService: UserService, private route: ActivatedRoute, private authService: AuthService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.users = data.users;
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
