declare global {
  interface Object {
    isObjectEmpty(this: any): boolean;
    isArray(this: any): boolean;
  }
}

function isObjectEmpty(this: any): boolean {
  return this && Object.keys(this).length === 0 && this.constructor === Object;
}

function isArray(this: any): boolean {
  return Array.isArray(this);
}

Object.prototype.isObjectEmpty = isObjectEmpty;
Object.prototype.isArray = isArray;

export {};
