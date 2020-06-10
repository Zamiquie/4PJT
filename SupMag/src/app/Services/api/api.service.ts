import { Injectable } from '@angular/core';
import {AlertController} from "@ionic/angular";
import {Uid} from "@ionic-native/uid/ngx";
import {AndroidPermissions} from "@ionic-native/android-permissions/ngx";


@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private _apiAddress: string;

  constructor(
      private alertController: AlertController,
      private uid : Uid,
      private androidPermissions: AndroidPermissions
  ) {
    this._apiAddress = "http://192.168.1.39:15403" //"http://apisupmagasin.ddns.net";
    //this.getMACAdress()
  }

  get apiAddress(): string {
    return this._apiAddress;
  }

  async errorPopup(error: string) {
    const alert = await this.alertController.create({
      message: error,
      buttons: ['OK']
    });
    await alert.present();
  }

  // async getMACAdress() {
  //   const { hasPermission } = await this.androidPermissions.checkPermission(
  //       this.androidPermissions.PERMISSION.READ_PHONE_STATE
  //   );
  //
  //   if (!hasPermission) {
  //     const result = await this.androidPermissions.requestPermission(
  //         this.androidPermissions.PERMISSION.READ_PHONE_STATE
  //     );
  //
  //     if (!result.hasPermission) {
  //       this.errorPopup("Permission requise.")
  //     }
  //     // ok, a user gave us permission, we can get him identifiers after restart app
  //     return;
  //   }
  //
  //   this.errorPopup(this.uid.MAC);
  // }
}
