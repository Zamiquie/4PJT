import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private _apiAddress: string;

  constructor() {
    this._apiAddress = "http://apisupmagasin.ddns.net";
  }

  get apiAddress(): string {
    return this._apiAddress;
  }
}
