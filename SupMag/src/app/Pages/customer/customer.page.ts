import { Component, OnInit } from '@angular/core';
import {ShoppingService} from "../../Services/shopping/shopping.service";

@Component({
  selector: 'app-customer',
  templateUrl: './customer.page.html',
  styleUrls: ['./customer.page.scss'],
})
export class CustomerPage implements OnInit {

  constructor(
      private shoppingService: ShoppingService
  ) { }

  ngOnInit() {
  }

}
