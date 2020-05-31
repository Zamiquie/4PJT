import { Component, OnInit } from '@angular/core';
import {NavController} from "@ionic/angular";
import {AuthentificationService} from "../../Services/authentification/authentification.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
})
export class LoginPage implements OnInit {

  mail: string;
  password: string;

  constructor(
      private navCtrl: NavController,
      private authService: AuthentificationService
  ) { }

  ngOnInit() {
  }

  goToRegister() {
    this.navCtrl.navigateForward('/register');
  }

  loginWithEmail(){
    this.authService.loginWithEmail(this.mail, this.password);
  }



}
