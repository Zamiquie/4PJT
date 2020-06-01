import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { AdvertisingPageRoutingModule } from './advertising-routing.module';

import { AdvertisingPage } from './advertising.page';
import {ComponentModule} from "../../Components/component/component.module";


@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        IonicModule,
        AdvertisingPageRoutingModule,
        ComponentModule
    ],
  declarations: [AdvertisingPage]
})
export class AdvertisingPageModule {}
