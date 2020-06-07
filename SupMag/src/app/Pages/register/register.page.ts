import { Component, OnInit } from '@angular/core';
import {AlertController, NavController} from "@ionic/angular";
import {AuthentificationService} from "../../Services/authentification/authentification.service";
import {User} from "../../Class/user/user";

@Component({
  selector: 'app-register',
  templateUrl: './register.page.html',
  styleUrls: ['./register.page.scss'],
})
export class RegisterPage implements OnInit {

  mail: string;
  password: string;
  confirmPassword: string;
  name: string;
  firstname: string;
  sexe: string;
  birthday: Date;
  adresse: string;
  city: string;
  postalCode: string;

  constructor(
      private navCtrl: NavController,
      private alertController: AlertController,
      private authService: AuthentificationService
  ) { }

  ngOnInit() {
  }

  registerWithMail(){
    if(
        this.mail != null &&
        this.password != null &&
        this.name != null &&
        this.firstname != null &&
        this.sexe != null &&
        this.birthday != null &&
        this.adresse != null &&
        this.city != null &&
        this.postalCode != null
    ){
      if(
          this.password == this.confirmPassword
      ){
        let formData = new User();

        formData.login = this.mail;
        formData.password = this.password;
        formData.name = this.name;
        formData.firstname = this.firstname;
        formData.sexe = parseInt(this.sexe);
        formData.birthday = this.birthday;
        formData.adress = this.adresse;
        formData.city = this.city;
        formData.postalCode = this.postalCode;

        this.authService.registerWithEmail(formData);
      }
      else{
        this.presentWrongPasswordAlert();
      }
    }else{
      this.presentMissingFieldAlert();
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

  async presentMissingFieldAlert() {
    const alert = await this.alertController.create({
      cssClass: 'alertPassword',
      message: 'Veuillez renseignez tout les champs',
      buttons: ['OK']
    });

    await alert.present();
  }

  returnToLogin(){
    this.navCtrl.navigateBack('/login')
  }

  changeDate(value: CustomEvent){
    this.birthday = new Date(value.detail.value);
  }
}
