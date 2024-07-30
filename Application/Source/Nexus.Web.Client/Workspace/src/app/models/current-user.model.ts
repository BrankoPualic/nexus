export interface ICurrentUser {
  id?: string;
  username?: string;
  email?: string;
  roles?: string[];
  token?: string;
  tokenExpiryDate?: Date;
}
