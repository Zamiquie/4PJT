import { Injectable } from '@angular/core';
import {Advert} from "../../Class/advert/advert";

@Injectable({
  providedIn: 'root'
})
export class AdvertService {

  private _advertising: Advert[];

  constructor()
  {
    this._advertising = [];
    this.testAdvertising()
  }

  get advertising(): Advert[] {
    return this._advertising;
  }

  testAdvertising(){
    let advert1 = new Advert("Pub1", null, "Ceci est la 1ere pub", "https://www.dpnews.fr/wp-content/uploads/2018/07/agence-publicite-montpellier.jpeg");
    let advert2 = new Advert("Pub2", "", "Ceci est la 2eme pub", "https://www.dpnews.fr/wp-content/uploads/2018/07/agence-publicite-montpellier.jpeg");
    let advert3 = new Advert("Pub3", "-70%", "Ceci est la 3eme pub", null);
    let advert4 = new Advert("Pub4", null, "Ceci est la 4eme pub", null);

    this._advertising.push(advert1);
    this._advertising.push(advert2);
    this._advertising.push(advert3);
    this._advertising.push(advert4);
  }
}
