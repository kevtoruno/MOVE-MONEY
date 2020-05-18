import { Component, OnInit, ViewEncapsulation, OnDestroy } from '@angular/core';
import { AuthService } from 'app/_services/auth.service';
import { AlertifyService } from 'app/_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  model: any = {};
  constructor(private authService: AuthService, private alertify: AlertifyService, private router: Router) { }

  ngOnInit(): void {
    if (this.authService.loggedIn()) {
      this.router.navigate(['']);
    }
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      this.router.navigate(['']);
    }, error => {
      this.alertify.error(error.error);
    });
  }



}
