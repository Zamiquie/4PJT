import { Component, OnInit } from '@angular/core';
import {UserService} from "../../Services/user/user.service";
import {User} from "../../Class/user/user";
import DateTimeFormat = Intl.DateTimeFormat;

@Component({
  selector: 'app-profile',
  templateUrl: './profile.page.html',
  styleUrls: ['./profile.page.scss'],
})
export class ProfilePage implements OnInit {
  private newUser: User;
  private sexe: string;

  constructor(
      private userService: UserService
  ) {}

  ngOnInit() {
  }

  modifyUser(){
    this.userService.modification = true;

    this.newUser = new User();

    this.newUser.name = this.userService.user.name;
    this.newUser.firstname = this.userService.user.firstname;
    this.sexe = this.userService.user.sexe.toString();
    this.newUser.login = this.userService.user.login;
    this.newUser.birthday = this.userService.user.birthday;
    this.newUser.adress = this.userService.user.adress;
    this.newUser.city = this.userService.user.city;
    this.newUser.postalCode = this.userService.user.postalCode;

  }

  saveUser(){
    this.userService.user.name = this.newUser.name;
    this.userService.user.firstname = this.newUser.firstname;
    this.userService.user.sexe = parseInt(this.sexe);
    this.userService.user.login = this.newUser.login;
    this.userService.user.birthday = this.newUser.birthday;
    this.userService.user.adress = this.newUser.adress;
    this.userService.user.city = this.newUser.city;
    this.userService.user.postalCode = this.newUser.postalCode;

    this.userService.saveUser();

  }

  changeDate(value: CustomEvent){
    this.newUser.birthday = new Date(value.detail.value);
  }
}
