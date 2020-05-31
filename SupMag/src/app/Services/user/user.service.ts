import { Injectable } from '@angular/core';
import {User} from "../../Class/user/user";
import {ApiService} from "../api/api.service";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  user: User;

  constructor(
      private apiService: ApiService,
      private httpClient: HttpClient
  ) {
    this.user = new User();
  }

  getUserProfile(){
    let url = this.apiService.apiAddress+"/Customer/"+this.user.id;

    let headers = {
      'Content-Type': 'application/json',
      'Access-Control-Allow-Origin': '*',
      'Authorization': 'Bearer '+this.user.token
    };

    this.httpClient.get<User>(url,{headers})
        .subscribe(
            response => {
              this.user.sexe = response.sexe;

              this.user.name = response.name;
              this.user.firstname = response.firstname;

              this.user.birthday = response.birthday;

              this.user.adress = response.adress;
              this.user.postalCode = response.postalCode;
              this.user.city = response.city;

              console.log(this.user);
            },
            error => {
              console.log(error);
            }
        );

  }
}
