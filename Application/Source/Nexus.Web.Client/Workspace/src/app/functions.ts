// Utility

function nameof<T extends object>(exp: ((obj: T) => any) | (new (...params: any[]) => T)): string {
  const fnStr = exp.toString();

  if (fnStr.substring(0, 6) == 'class ' && fnStr.substring(0, 8) != 'class =>') {
    return cleanseAssertionOperators(fnStr.substring('class '.length, fnStr.indexOf(' {')));
  }

  if (fnStr.indexOf('=>') !== -1) {
    return cleanseAssertionOperators(fnStr.substring(fnStr.indexOf('.') + 1));
  }

  throw new Error('ts-simple-nameof: Invalid function');
}

function cleanseAssertionOperators(parsedName: string): string {
  return parsedName.replace(/[?!]/g, '');
}
