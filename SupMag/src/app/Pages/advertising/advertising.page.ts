import { Component, OnInit } from '@angular/core';
import {AdvertService} from "../../Services/advert/advert.service";

@Component({
  selector: 'app-advertising',
  templateUrl: './advertising.page.html',
  styleUrls: ['./advertising.page.scss'],
})
export class AdvertisingPage implements OnInit {

  constructor(
      private advertService: AdvertService
  ) { }

  ngOnInit() {
  }

}
