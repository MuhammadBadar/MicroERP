import { Injectable } from '@angular/core';
import { AppConstants } from './app.constants';

@Injectable({
    providedIn: 'root'
})
export class StorageService {

    constructor() { }
    setItem(key: string, value: any): void {
        localStorage.setItem(key, value);
    }

    getItem(key: string): any {
        return localStorage.getItem(key);
    }

    removeItem(key: string): void {
        localStorage.removeItem(key);
    }
}