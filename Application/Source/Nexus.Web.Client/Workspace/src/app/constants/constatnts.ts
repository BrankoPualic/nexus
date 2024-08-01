export class Constants {
  // Routes
  static readonly ROUTE_HOME = '/';
  static readonly ROUTE_NOT_FOUND = 'not-found';
  static readonly ROUTE_UNAUTHORIZED = 'unauthorized';
  static readonly ROUTE_AUTH = 'auth';
  static readonly ROUTE_AUTH_SIGNUP = 'signup';
  static readonly ROUTE_AUTH_SIGNIN = 'signin';

  // Route Titles
  static readonly TITLE = 'Nexus';

  // RegEx

  static readonly REGEX_PASSWORD = /^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{8,}$/;
  static readonly REGEX_EMAIL =
    /(?:[a-z0-9!#$%&'*+\/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+\/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])/;

  // Validation Types

  static readonly VALIDATION_TYPE_REQUIRED = 'required';
  static readonly VALIDATION_TYPE_EMAIL = 'email';
  static readonly VALIDATION_TYPE_PATTERN = 'pattern';
  static readonly VALIDATION_TYPE_MAX_LENGTH = 'maxlength';
  static readonly VALIDATION_TYPE_NOT_MATCHING = 'notMatching';
  static readonly VALIDATION_TYPE_MINIMUM_AGE = 'minimumAge';

  // Validation Messages

  static readonly VALIDATION_REQUIRED = 'Required field.';
  static readonly VALIDATION_EMAIL = 'Invalid email format.';
  static readonly VALIDATION_PASSWORD =
    'Password must contain at least one uppercase letter, one lowercase letter, one number, one special character, and be at least 8 characters long.';
  static readonly VALIDATION_MAX_LENGTH = "Field can't be more than {0} characters long.";
  static readonly VALIDATION_NOT_MATCHING = "{0} and {1} fields don't match.";
  static readonly VALIDATION_MINIMUM_AGE = 'Minimum age required is {0}.';
}
