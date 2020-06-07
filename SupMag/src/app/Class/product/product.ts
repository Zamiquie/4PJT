export class Product {
    private _id: string;
    private _designation: string;
    private _description: string;
    private _weight: number;
    private _price: number;


    constructor(id: string, designation: string, description: string, weight: number, price: number) {
        this._id = id;
        this._designation = designation;
        this._description = description;
        this._weight = weight;
        this._price = price;
    }

    get id(): string {
        return this._id;
    }

    set id(value: string) {
        this._id = value;
    }

    get designation(): string {
        return this._designation;
    }

    set designation(value: string) {
        this._designation = value;
    }

    get description(): string {
        return this._description;
    }

    set description(value: string) {
        this._description = value;
    }

    get weight(): number {
        return this._weight;
    }

    set weight(value: number) {
        this._weight = value;
    }

    get price(): number {
        return this._price;
    }

    set price(value: number) {
        this._price = value;
    }
}
