import { Injectable } from '@angular/core';
import {User} from "../../Class/user/user";
import {ApiService} from "../api/api.service";
import {HttpClient} from "@angular/common/http";
import {AlertController, NavController} from "@ionic/angular";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  user: User;
  modification: boolean;

  constructor(
      private apiService: ApiService,
      private httpClient: HttpClient,
      private navCtrl: NavController,
      private alertController: AlertController
  ) {
    this.user = new User();
    this.modification = false;
  }

  getUserProfileAndGoToCustomer(){
    let url = this.apiService.apiAddress+"/Customer/"+this.user.id;

    let headers = {
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*',
        'Authorization': 'Bearer '+this.user.token
    };

      this.httpClient.get(url,{headers : headers, responseType: "text"})
        .subscribe(
            response => {
                console.log(response);
                response = response.replace(/ISODate\(/g,"");
                response = response.replace(/\),/g,",");
                console.log(response);
                let jsonResponse = JSON.parse(response);
                console.log(jsonResponse);
                this.user.sexe = jsonResponse.Sexe;
                this.user.password = jsonResponse.Password;
                this.user.name = jsonResponse.Name;
                this.user.firstname = jsonResponse.FirstName;
                this.user.birthday = new Date(jsonResponse.BirthDay);
                this.user.adress = jsonResponse.Adress;
                this.user.postalCode = jsonResponse.Postal_Code;
                this.user.city = jsonResponse.City;
                this.user.rib = jsonResponse.RIB;
                this.user.phones = jsonResponse.Phones;
                this.user.photo = jsonResponse.Photo;
                this.user.lastTime = new Date(jsonResponse.Last_Time);
                this.user.annualFrequentation = jsonResponse.AnnualFrequentation;
                this.user.panierMoyen = jsonResponse.PanierMoyen;

                console.log(this.user);

              this.navCtrl.navigateRoot('/customer');
            },
            error => {
              console.log(error);
            }
        );
  }

  cancelModification(){

  }

  getJSONUserData(){
      let data={
          id: this.user.id,
          sexe: this.user.sexe,
          email: this.user.login,
          password: this.user.password,
          name: this.user.name,
          firstName: this.user.firstname,
          birthdayDay: this.user.birthday.toISOString(),
          adress: this.user.adress,
          postal_Code: this.user.postalCode,
          city: this.user.city,
          banqData: this.user.rib,
          phones: this.user.phones,
          photo: this.user.photo,
          last_Time: this.user.lastTime.toISOString(),
          annualFrequentation: this.user.annualFrequentation,
          panierMoyen: this.user.panierMoyen,
          gooToken: this.user.gooToken,
          faceToken: this.user.faceToken
      };
      return data;
  }

  saveUser(){
      let url = this.apiService.apiAddress+"/Customer/updateCustomer";

      let headers = {
          'Content-Type': 'application/json',
          'Access-Control-Allow-Origin': '*',
          'Authorization': 'Bearer '+this.user.token
      };

      this.httpClient.put(url, this.getJSONUserData(), {headers: headers})
          .subscribe(
              response => {
                  this.goodSaveAlert();
                  this.modification = false;
              },
              error => {
                  console.log(error);
                  this.wrongSaveAlert();
                  this.modification = true;
              }
          );
  }

    async goodSaveAlert() {
        const alert = await this.alertController.create({
            message: 'Vos données ont été modifiées',
            buttons: ['OK']
        });
        await alert.present();
    }

    async wrongSaveAlert() {
        const alert = await this.alertController.create({
            message: 'Erreur lors de la sauvegarde des données',
            buttons: ['OK']
        });

        await alert.present();
    }
}
