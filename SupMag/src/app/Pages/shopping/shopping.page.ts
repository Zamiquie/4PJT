import { Component, OnInit } from '@angular/core';
import {BarcodeScanner} from "@ionic-native/barcode-scanner/ngx";
import {Product} from "../../Class/product/product";
import {ProductListItem} from "../../Class/productListItem/product-list-item";

@Component({
  selector: 'app-shopping',
  templateUrl: './shopping.page.html',
  styleUrls: ['./shopping.page.scss'],
})
export class ShoppingPage implements OnInit {

  private products: ProductListItem[];
  private total: number;

  constructor(
      private barcodeScanner: BarcodeScanner
  )
  {
    this.products = [];
    this.total = 0;
  }

  ngOnInit() {
  }

  scanQRCode(){
    this.barcodeScanner.scan().then(barcodeData => {
      // success. barcodeData is the data returned by scanner
    }).catch(err => {
      // error
    });
  }

  isInProductList(name: string){
    let isInList = false;
    this.products.forEach(function(product) {
      if(product.product.designation == name) {
        isInList = true;
      }
    });
    return isInList;
  }

  findIndexByName(name: string){
    let goodProduct = null;
    this.products.forEach(function(product){
      if(product.product.designation == name){
        goodProduct = product
      }
    });
    return this.products.indexOf(goodProduct);
  }

  addProductToCart(
      id: string,
      designation: string,
      description: string,
      weight: number,
      price: number,
      quantity: number
  ){
    if(this.isInProductList(designation)){
      this.products[this.findIndexByName(designation)].quantity += quantity;
    }else{
      this.products.push(new ProductListItem(
          new Product(
              id,
              designation,
              description,
              weight,
              price,
          ),
          quantity
          )
      );
    }
    this.products.forEach(function(product){
      product.updatePrice();
    });
    this.updateTotal();
  }

  testProductList(){

    this.addProductToCart(
        "1",
        "Mangue",
        "Oui",
        10,
        3,
        5
    );
    this.addProductToCart(
        "2",
        "Citron",
        "Oui",
        10,
        4,
        7
    );
    this.addProductToCart(
        "3",
        "Pomme",
        "Oui",
        10,
        5,
        2
    );
  }

  updateTotal(){
    this.total = 0;
    this.products.forEach(product => (this.total += product.price));
  }

  deleteOneProduct(product: ProductListItem){
    let destroy = product.deleteOne();
    if(destroy){
      let index = this.products.indexOf(product);
      if(index > -1){
        this.products.splice(index, 1);
      }
    }
    this.updateTotal();
  }

  addOneProduct(product: ProductListItem){
    product.addOne();
    this.updateTotal();
  }

  clearProduct(){

  }
}
