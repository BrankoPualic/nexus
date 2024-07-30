export class ModelError {
  constructor(key: string, error: string);

  constructor(key: string, errors: string[]);

  constructor(key: string, errors: string[] | string) {
    this.key = key;

    if (!errors) return;

    if (errors.isArray()) this.errors = errors as string[];
    else this.errors.push(errors as string);
  }

  key: string;
  errors: string[] = [];
  showedKey: string[] = [];

  isShowed(key: string, onlyKey?: boolean): boolean {
    if (this.showedKey.length > 0) {
      if (this.showedKey.indexOf(key) >= 0) return true;
    } else {
      if (this.key === key || (onlyKey && this.key.indexOf(key) === 0)) {
        this.showedKey.push(key);
        return true;
      }
    }
    return false;
  }

  get mesage(): string {
    return `${this.key} : [${this.errors.join(', ')}]`;
  }
}
