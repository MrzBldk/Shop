import { UserManager, UserManagerSettings, WebStorageStateStore } from "oidc-client-ts";
import { decodeJWT } from "./utils";

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
            scope: 'spa aggregator ordering_api catalog_api store_api',
            userStore: new WebStorageStateStore({ store: localStorage })
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
        return decodeJWT(token).role;
    }

    public async getUserId() {
        const user = await this.userManager.getUser();
        if (!user) {
            return null
        }
        const token = user.access_token
        return decodeJWT(token).sub;
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
        return decodeJWT(token).name
    }

    public async login(username: string, password: string) {
        const user = await this.userManager.signinResourceOwnerCredentials({ username, password, skipUserInfo: false })
        this.notifySubscribers()
        return user
    }

    public async logout() {
        await this.userManager.removeUser();
        this.notifySubscribers()
    }
}

const authService = new AuthService()

export default authService