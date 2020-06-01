import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import {AdvertComponent} from "../advert/advert.component";
import {IonicModule} from "@ionic/angular";

@NgModule({
  declarations: [
      AdvertComponent
  ],
  imports: [
    CommonModule,
    IonicModule
  ],
  exports:[
      AdvertComponent
  ],
  entryComponents:[
      AdvertComponent
  ]
})
export class ComponentModule { }
