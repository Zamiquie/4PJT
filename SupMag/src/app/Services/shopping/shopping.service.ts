import { Injectable } from '@angular/core';
import {ProductListItem} from "../../Class/productListItem/product-list-item";
import {User} from "../../Class/user/user";
import {ApiService} from "../api/api.service";
import {HttpClient} from "@angular/common/http";
import {UserService} from "../user/user.service";
import {AlertController} from "@ionic/angular";
import {Sales} from "../../Class/sales/sales";
import {Product} from "../../Class/product/product";

@Injectable({
  providedIn: 'root'
})
export class ShoppingService {

  private produitsSales;
  private _products: ProductListItem[];
  private _total: number;
  private _sales: Sales[];

  constructor(
      private apiService: ApiService,
      private httpClient: HttpClient,
      private userService: UserService,
      private alertController: AlertController
  ) {
      this.sales = [];
  }


  get products(): ProductListItem[] {
    return this._products;
  }

  set products(value: ProductListItem[]) {
    this._products = value;
  }

  get total(): number {
    return this._total;
  }

  set total(value: number) {
    this._total = value;
  }

  get sales(): Sales[] {
    return this._sales;
  }

  set sales(value: Sales[]) {
    this._sales = value;
  }

  countSales(){
      return this.sales.length;
  }

  clearProducts(){
    this.products = [];
    this.total = 0;
  }

  pay(){
    let url = this.apiService.apiAddress+"/sales/addsales";//on rajoute les suffixes de la connection

    let headers = {
      'Content-Type': 'application/json',
      'Access-Control-Allow-Origin': '*',
      'Authorization': 'Bearer '+this.userService.user.token
    };

    this.produitsSales = [];

    this.products.forEach(
        productItem => (
            this.produitsSales.push(
                {
                  IdProduct: productItem.product.id,
                  Quantity: productItem.quantity,
                  UnitPrice: productItem.product.price,
                  AmountPromo: productItem.amountPromo
                }
            )
        )
    );

    let data={
      _id: null,
      SaleDate: new Date().toISOString(),
      IdPhone: "",
      IdCustomer: this.userService.user.id,
      IdShop: "Mag737044",
      TotalAmount: this.total,
      isPayed: true,
      ProduitsSales: this.produitsSales
    };

    this.httpClient.post(url,data,{headers: headers})
        .subscribe(
            response => {
              this.popupMessage('Merci de votre achat.<br/> Votre facture est disponible dans l\'onglet "Mes factures" ');
              this.clearProducts();
              this.getAllSales();
            },
            error => {
              console.log(error);
            }
        );
  }

  async popupMessage(text: string) {
    const alert = await this.alertController.create({
      message: text,
      buttons: ['OK']
    });
    await alert.present();
  }

  getAllSales(){
    let url = this.apiService.apiAddress+"/sales/user/"+this.userService.user.id;

    let headers = {
      'Content-Type': 'application/json',
      'Access-Control-Allow-Origin': '*',
      'Authorization': 'Bearer '+this.userService.user.token
    };

    this.httpClient.get(url,{headers : headers, responseType: "text"})
        .subscribe(
            response => {
              response = response.replace(/ISODate\(/g,"");
              response = response.replace(/\),/g,",");
              let jsonResponse = JSON.parse(response);

              jsonResponse.forEach(
                  sale => (
                      this.sales.push(
                          new Sales(
                              sale._id,
                              new Date(sale.SaleDate),
                              sale.IdPhone,
                              sale.IdCustomer,
                              sale.IdShop,
                              sale.TotalAmount,
                              sale.isPayed,
                              sale.ProduitsSales
                          )
                      )
                  )
              );
            },
            error => {
              console.log(error);
            }
        );
  }

  getProductWitQRCode(QRString: string){
      let url = this.apiService.apiAddress+"/qr/decrypte/"+QRString;

      let headers = {
          'Content-Type': 'application/json',
          'Access-Control-Allow-Origin': '*',
          'Authorization': 'Bearer '+this.userService.user.token
      };

      this.httpClient.get(url,{headers : headers, responseType: "text"})
          .subscribe(
              response => {
                  response = response.replace(/ISODate\(/g,"");
                  response = response.replace(/\)/g,"");
                  let jsonResponse = JSON.parse(response);

                  this.addProductToCart(
                      jsonResponse._id,
                      jsonResponse.designation,
                      jsonResponse.description,
                      jsonResponse.weight,
                      jsonResponse.SalePrice,
                      1
                  );
              },
              error => {
                  this.popupMessage("Code barre non reconnu");
                  this.popupMessage(error)
              }
          );
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

    updateTotal(){
        this.total = 0;
        this.products.forEach(product => (this.total += product.price));
    }

}
