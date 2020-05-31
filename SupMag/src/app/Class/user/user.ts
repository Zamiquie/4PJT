import {Rib} from "../rib/rib";
import {Phone} from "../phone/phone";

export class User {
    private _login: string;
    private _password: string;
    private _realyUser: boolean;
    private _id: string;
    private _token:string;

    private _sexe: number;
    private _name: string;
    private _firstname: string;
    private _birthday: Date;
    private _adress: string;
    private _postalCode: string;
    private _city: string;
    private RIBs: Rib[];
    private phones: Phone[];


    get sexe(): number {
        return this._sexe;
    }

    set sexe(value: number) {
        this._sexe = value;
    }

    get name(): string {
        return this._name;
    }

    set name(value: string) {
        this._name = value;
    }

    get firstname(): string {
        return this._firstname;
    }

    set firstname(value: string) {
        this._firstname = value;
    }

    get birthday(): Date {
        return this._birthday;
    }

    set birthday(value: Date) {
        this._birthday = value;
    }

    get adress(): string {
        return this._adress;
    }

    set adress(value: string) {
        this._adress = value;
    }

    get postalCode(): string {
        return this._postalCode;
    }

    set postalCode(value: string) {
        this._postalCode = value;
    }

    get city(): string {
        return this._city;
    }

    set city(value: string) {
        this._city = value;
    }

    get login(): string {
        return this._login;
    }

    set login(value: string) {
        this._login = value;
    }

    get password(): string {
        return this._password;
    }

    set password(value: string) {
        this._password = value;
    }

    get realyUser(): boolean {
        return this._realyUser;
    }

    set realyUser(value: boolean) {
        this._realyUser = value;
    }

    get id(): string {
        return this._id;
    }

    set id(value: string) {
        this._id = value;
    }

    get token(): string {
        return this._token;
    }

    set token(value: string) {
        this._token = value;
    }
}
