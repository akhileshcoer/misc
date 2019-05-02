import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class Config {

  private _special: boolean;
  set special(val: boolean) {
    this._special = val;
  }

  get special() {
    return this._special;
  }

}