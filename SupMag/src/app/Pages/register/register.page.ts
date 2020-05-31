import { Component, OnInit } from '@angular/core';
import {AlertController, NavController} from "@ionic/angular";
import {AuthentificationService} from "../../Services/authentification/authentification.service";

@Component({
  selector: 'app-register',
  templateUrl: './register.page.html',
  styleUrls: ['./register.page.scss'],
})
export class RegisterPage implements OnInit {

  mail: string;
  password: string;
  confirmPassword: string;

  constructor(
      private navCtrl: NavController,
      private alertController: AlertController,
      private authService: AuthentificationService
  ) { }

  ngOnInit() {
  }

  registerWithMail(){
    if(this.password != null && this.password == this.confirmPassword){
      this.authService.registerWithEmail(this.mail, this.password);
    }else{
      this.presentWrongPasswordAlert();
    }
  }

  async presentWrongPasswordAlert() {
    const alert = await this.alertController.create({
      cssClass: 'alertPassword',
      message: 'Vos mots de passe ne sont pas identiques.',
      buttons: ['OK']
    });

    await alert.present();
  }

  returnToLogin(){
    this.navCtrl.navigateBack('/login')
  }
}
