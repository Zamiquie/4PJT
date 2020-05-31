import { Injectable } from '@angular/core';
import {ApiService} from "../api/api.service";
import {UserService} from "../user/user.service";
import {HttpClient} from "@angular/common/http";
import {User} from "../../Class/user/user";
import {NavController} from "@ionic/angular";

@Injectable({
  providedIn: 'root'
})
export class AuthentificationService {

  constructor(
      private apiService: ApiService,
      private userService: UserService,
      private httpClient: HttpClient,
      private navCtrl: NavController
              ) { }

  loginWithEmail(mail: string, password: string){
    this.userService.user.realyUser = true;
    let url = this.apiService.apiAddress+"/user/auth";//on rajoute les suffixes de la connection

    let headers = {
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*'
    };
    let data={
        Login : mail,
        Password : password,
        RealyUser : true
    };

    this.httpClient.post<User>(url,data,{headers: headers})
        .subscribe(
            response => {
                this.userService.user.id = response.id;
                this.userService.user.login = response.login;
                this.userService.user.realyUser = response.realyUser;
                this.userService.user.token = response.token;

                this.userService.getUserProfile();
                this.navCtrl.navigateRoot('/home');

                console.log(this.userService.user);
            },
            error => {
                console.log(error);
            }
        );
  }

  registerWithEmail(mail: string, password: string){
      this.userService.user.realyUser = true;
      let url = this.apiService.apiAddress+"/user/create";//on rajoute les suffixes de la connection

      let headers = {
          'Content-Type': 'application/json',
          'Access-Control-Allow-Origin': '*'
      };
      let data={
          Id: null,
          Sexe: 1,
          Email: mail,
          Password: password,
          Name: "test",
          FirstName: "",
          BirthdayDay: "0001-01-01T00:00:00",
          Adress: "",
          Postal_Code: "",
          City: "",
          BanqData: null,
          Phones: null,
          Photo: null,
          Last_Time: new Date().toISOString(),
          AnnualFrequentation: 0,
          PanierMoyen: 0
      };

      this.httpClient.post<User>(url,data,{headers: headers})
          .subscribe(
              response => {
                  this.userService.user.id = response.id;
                  this.userService.user.login = response.login;
                  this.userService.user.realyUser = response.realyUser;
                  this.userService.user.token = response.token;

                  this.userService.getUserProfile();
                  this.navCtrl.navigateRoot('/home');
              },
              error => {
                  console.log(error);
              }
          );


  }
}
