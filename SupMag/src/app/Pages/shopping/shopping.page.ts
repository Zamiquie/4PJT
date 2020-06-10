import { Component, OnInit } from '@angular/core';
import {BarcodeScanner} from "@ionic-native/barcode-scanner/ngx";
import {Product} from "../../Class/product/product";
import {ProductListItem} from "../../Class/productListItem/product-list-item";
import {ShoppingService} from "../../Services/shopping/shopping.service";
import {AlertController} from "@ionic/angular";

@Component({
  selector: 'app-shopping',
  templateUrl: './shopping.page.html',
  styleUrls: ['./shopping.page.scss'],
})
export class ShoppingPage implements OnInit {

  constructor(
      private barcodeScanner: BarcodeScanner,
      private shoppingService: ShoppingService,
      private alertController: AlertController
  )
  {
    this.shoppingService.products = [];
    this.shoppingService.total = 0;
  }

  ngOnInit() {
  }

  testProductList(){
    this.shoppingService.addProductToCart(
        "Man0544784445",
        "Mangue",
        "Bon Produit qui se mangen avec les doigts",
        0.20000000298023224,
        1.5,
        5
    );
  }

  scanQRCode(){
    this.barcodeScanner.scan().then(barcodeData => {
      this.shoppingService.getProductWitQRCode(barcodeData.text);
    }).catch(err => {
      this.scanQRcodeModal(err);
    });
  }

  async scanQRcodeModal(text : string) {
    const alert = await this.alertController.create({
      message: text,
      buttons: ['OK']
    });
    await alert.present();
  }

  deleteOneProduct(product: ProductListItem){
    let destroy = product.deleteOne();
    if(destroy){
      let index = this.shoppingService.products.indexOf(product);
      if(index > -1){
        this.shoppingService.products.splice(index, 1);
      }
    }
    this.shoppingService.updateTotal();
  }

  addOneProduct(product: ProductListItem){
    product.addOne();
    this.shoppingService.updateTotal();
  }

}
