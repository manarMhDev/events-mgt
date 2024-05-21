import { Injectable } from '@angular/core';
import { AuthenticationResponse } from '../api/service-proxies';

@Injectable()
export class AppSessionService {

    constructor() {
    }

    get user(): AuthenticationResponse {
        const user = new AuthenticationResponse();
        user.init({
            // name : localStorage.getItem("username"),
            email : localStorage.getItem("event-email"),
            id: localStorage.getItem("event-id")
        });
        return user.id ? user : undefined;
    }

    get userId(): string {
        return localStorage.getItem("event-id") ? localStorage.getItem("event-id") : null;
    }

    get tempId(): string {
        return localStorage.getItem("tempId") ? localStorage.getItem("tempId") : null;
    }

}
