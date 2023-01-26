import { UserManager, UserManagerSettings } from "oidc-client-ts";

export default class AuthService {
    userManager: UserManager;

    constructor() {
        const settings: UserManagerSettings = {
            authority: process.env.REACT_APP_STS_URL!,
            client_id: "spa",
            redirect_uri: "http://localhost:3000",
            client_secret: process.env.REACT_APP_CLIENT_SECRET,
            scope: 'spa'
        };
        this.userManager = new UserManager(settings);
    }

    public async isAuthenticated() {
        return !! await this.userManager.getUser();
    }

    public async getUser() {
        return await this.userManager.getUser();
    }

    public async getUserRole() {
        const user = await this.userManager.getUser();
        if (!user) {
            return null
        }
        const token = user.access_token
        const base64Url = token.split('.')[1];
        const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
        const jsonPayload = decodeURIComponent(atob(base64).split('').map(function (c) {
            return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
        }).join(''));

        return JSON.parse(jsonPayload).id;
    }

    public async getToken() {
        const user = await this.userManager.getUser();
        return user && user.access_token
    }

    public async getUserName() {
        const user = await this.userManager.getUser();
        if (!user) {
            return null
        }
        const token = user.access_token
        const base64Url = token.split('.')[1];
        const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
        const jsonPayload = decodeURIComponent(atob(base64).split('').map(function (c) {
            return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
        }).join(''));

        return JSON.parse(jsonPayload).name;
    }

    public async login(username: string, password: string) {
        return await this.userManager.signinResourceOwnerCredentials({ username, password, skipUserInfo: false })
    }

    public logout(): Promise<void> {
        return this.userManager.signoutSilent();
    }
}