import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class PageLoaderService {
  private loaderSubject = new BehaviorSubject<boolean>(false);
  loaderState$ = this.loaderSubject.asObservable();

  constructor() {}

  showLoader() {
    this.loaderSubject.next(true);
  }

  hideLoader() {
    this.loaderSubject.next(false);
  }
}
