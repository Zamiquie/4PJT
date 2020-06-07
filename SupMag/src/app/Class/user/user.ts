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
    private _rib: object[];
    private _phones: object[];
    private _photo: string;
    private _lastTime: Date;
    private _annualFrequentation: number;
    private _panierMoyen: number;
    private _gooToken: string;
    private _faceToken: string;

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

    get lastTime(): Date {
        return this._lastTime;
    }

    set lastTime(value: Date) {
        this._lastTime = value;
    }

    get annualFrequentation(): number {
        return this._annualFrequentation;
    }

    set annualFrequentation(value: number) {
        this._annualFrequentation = value;
    }

    get panierMoyen(): number {
        return this._panierMoyen;
    }

    set panierMoyen(value: number) {
        this._panierMoyen = value;
    }

    get gooToken(): string {
        return this._gooToken;
    }

    set gooToken(value: string) {
        this._gooToken = value;
    }

    get faceToken(): string {
        return this._faceToken;
    }

    set faceToken(value: string) {
        this._faceToken = value;
    }


    get rib(): object[] {
        return this._rib;
    }

    set rib(value: object[]) {
        this._rib = value;
    }

    get phones(): object[] {
        return this._phones;
    }

    set phones(value: object[]) {
        this._phones = value;
    }

    get photo(): string {
        return this._photo;
    }

    set photo(value: string) {
        this._photo = value;
    }
}
