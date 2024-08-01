declare global {
  interface String {
    formatString(this: string, ...args: string[]): string;
    capitalize(this: string): string;
  }
}

function formatString(this: string, ...args: string[]): string {
  return this.replace(/{(\d+)}/g, (match, number) => (args[number] ? args[number] : match));
}

function capitalize(this: string): string {
  return this.split(' ')
    .map(word => word.charAt(0).toUpperCase() + word.slice(1).toLowerCase())
    .join(' ');
}

String.prototype.formatString = formatString;
String.prototype.capitalize = capitalize;

export {};
