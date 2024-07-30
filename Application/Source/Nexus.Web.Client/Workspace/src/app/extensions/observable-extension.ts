import { Observable, lastValueFrom } from 'rxjs';

declare module 'rxjs' {
  interface Observable<T> {
    toResult<T>(this: Observable<T>): Promise<T>;
  }
}

function toResult<T>(this: Observable<T>): Promise<T> {
  return lastValueFrom(this);
}

Observable.prototype.toResult = toResult;

export {};
