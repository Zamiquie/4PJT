import {Product} from "../product/product";

export class ProductListItem {
    private _product: Product;
    private _quantity: number;
    private _price: number;
    private _amountPromo: number;


    constructor(product: Product, quantity: number, amountPromo: number = 0) {
        this._product = product;
        this._quantity = quantity;
        this._amountPromo = amountPromo;
        this._price = this.product.price * this.quantity - this.amountPromo;
    }

    get product(): Product {
        return this._product;
    }

    set product(value: Product) {
        this._product = value;
    }

    get quantity(): number {
        return this._quantity;
    }

    set quantity(value: number) {
        this._quantity = value;
    }

    get price(): number {
        return this._price;
    }

    set price(value: number) {
        this._price = value;
    }

    get amountPromo(): number {
        return this._amountPromo;
    }

    set amountPromo(value: number) {
        this._amountPromo = value;
    }

    public addOne(){
        this.quantity += 1;
        this.updatePrice();
    }

    public deleteOne(){
        this.quantity -= 1;
        this.updatePrice();
        return this.quantity == 0;
    }

    public updatePrice(){
        this.price = this.product.price * this.quantity
    }
}
