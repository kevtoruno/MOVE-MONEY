import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';
import { JwtHelperService } from '@auth0/angular-jwt';
import { User } from 'app/_models/User';
import { map } from 'rxjs/operators';
import { BehaviorSubject } from 'rxjs'
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = environment.apiUrl + 'auth/';
  jwtHelper = new JwtHelperService();
  decodedToken: any;
  currentUser: User;
  initialMoney: number;
  money = new BehaviorSubject<Number>(this.initialMoney);
  currentMoney = this.money.asObservable();

  constructor(private http: HttpClient) {

  }
  updateMoney(money: Number) {
    this.money.next(money);
  }
  login(model: any) {
    return this.http.post(`${this.baseUrl}login`, model)
      .pipe(
        map((response: any) => {
          const user = response;
          if (user) {
            console.log(user);
            localStorage.setItem('token', user.jwtToken);
            localStorage.setItem('user', JSON.stringify(user.user));
            this.decodedToken = this.jwtHelper.decodeToken(user.token);
            this.currentUser = user.user;
            this.initialMoney = user.user.money;
            this.updateMoney(this.currentUser.money);
          }
        })
      );
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }


}
