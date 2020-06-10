import { Component, OnInit } from '@angular/core';
import {ShoppingService} from "../../Services/shopping/shopping.service";

@Component({
  selector: 'app-invoices',
  templateUrl: './invoices.page.html',
  styleUrls: ['./invoices.page.scss'],
})
export class InvoicesPage implements OnInit {

  constructor(
      private shoppingService: ShoppingService
  ) { }

  ngOnInit() {
  }

}
