import { UserManager, User } from "oidc-client-ts";

export default class AuthService {
    userManager: UserManager;

    constructor() {
        const settings = {
            authority: "http://localhost:9002",
            client_id: "spa",
            redirect_uri: "",
            client_secret: "secret",
            post_logout_redirect_uri: ""
        };
        this.userManager = new UserManager(settings);
    }

    public getUser(): Promise<User | null> {
        return this.userManager.getUser();
    }

    public login(username: string, password: string): Promise<User> {
        return this.userManager.signinResourceOwnerCredentials({username, password, skipUserInfo: false });
    }

    public logout(): Promise<void> {
        return this.userManager.signoutSilent();
    }
}