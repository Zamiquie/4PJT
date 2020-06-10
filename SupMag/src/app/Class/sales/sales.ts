export class Sales {
    private _id: string;
    private _saleDate: Date;
    private _idPhone: string;
    private _idCustomer: string;
    private _idShop: string;
    private _totalAmount: number;
    private _isPayed: boolean;
    private _produitsSales: object[];


    constructor(id: string, saleDate: Date, idPhone: string, idCustomer: string, idShop: string, totalAmount: number, isPayed: boolean, produitsSales: object[]) {
        this._id = id;
        this._saleDate = saleDate;
        this._idPhone = idPhone;
        this._idCustomer = idCustomer;
        this._idShop = idShop;
        this._totalAmount = totalAmount;
        this._isPayed = isPayed;
        this._produitsSales = produitsSales;
    }


    get id(): string {
        return this._id;
    }

    set id(value: string) {
        this._id = value;
    }

    get saleDate(): Date {
        return this._saleDate;
    }

    set saleDate(value: Date) {
        this._saleDate = value;
    }

    get idPhone(): string {
        return this._idPhone;
    }

    set idPhone(value: string) {
        this._idPhone = value;
    }

    get idCustomer(): string {
        return this._idCustomer;
    }

    set idCustomer(value: string) {
        this._idCustomer = value;
    }

    get idShop(): string {
        return this._idShop;
    }

    set idShop(value: string) {
        this._idShop = value;
    }

    get totalAmount(): number {
        return this._totalAmount;
    }

    set totalAmount(value: number) {
        this._totalAmount = value;
    }

    get isPayed(): boolean {
        return this._isPayed;
    }

    set isPayed(value: boolean) {
        this._isPayed = value;
    }

    get produitsSales(): object[] {
        return this._produitsSales;
    }

    set produitsSales(value: object[]) {
        this._produitsSales = value;
    }
}
