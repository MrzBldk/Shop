import { UserManager, UserManagerSettings } from "oidc-client-ts";

class AuthService {
    userManager: UserManager
    #callbacks: Array<{ callback: Function, subscription: number }> = []
    #nextSubscriptionId = 0;

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

    subscribe(callback: Function) {
        this.#callbacks.push({ callback, subscription: this.#nextSubscriptionId++ });
        return this.#nextSubscriptionId - 1;
    }

    unsubscribe(subscriptionId: number) {
        const subscriptionIndex = this.#callbacks
            .map((element, index) => element.subscription === subscriptionId ? { found: true, index } : { found: false })
            .filter(element => element.found === true);

        if (subscriptionIndex.length !== 1) {
            throw new Error(`Found an invalid number of subscriptions ${subscriptionIndex.length}`);
        }
        this.#callbacks.splice(subscriptionIndex[0].index!, 1);
    }

    notifySubscribers() {
        for (let i = 0; i < this.#callbacks.length; i++) {
            const callback = this.#callbacks[i].callback;
            callback();
        }
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

        return JSON.parse(jsonPayload).role;
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
        const user = await this.userManager.signinResourceOwnerCredentials({ username, password, skipUserInfo: false })
        this.notifySubscribers()
        return user
    }

    public async logout() {
        await this.userManager.signoutSilent();
        this.notifySubscribers()
    }
}

const authService = new AuthService()

export default authService