import {Product} from "../product/product";

export class ProductListItem {
    private _product: Product;
    private _quantity: number;
    private _price: number;

    constructor(product: Product, quantity: number) {
        this._product = product;
        this._quantity = quantity;
        this._price = this.product.price * quantity
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

    public addOne(){
        this.quantity += 1;
        this.updatePrice();
    }

    public deleteOne(){
        this.quantity -= 1;
        this.updatePrice();
        if (this.quantity == 0){
            return true;
        }else return false
    }

    public updatePrice(){
        this.price = this.product.price * this.quantity
    }
}
