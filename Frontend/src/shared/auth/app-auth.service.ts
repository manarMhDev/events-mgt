import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AuthClient, AuthenticationResponse  , AuthenticationResponseResponse, LoginRequest} from '../api/service-proxies';
import { AuthStatusService } from 'src/app/Services/auth-status.service';
@Injectable()
export class AppAuthService {
    authenticateModel: LoginRequest;
    authenticateResult: AuthenticationResponse;
    rememberMe: boolean;

    constructor(
        private authService: AuthClient,
        private _router: Router,
        private authStatus:AuthStatusService,
    ) {
        this.clear();
    }

    logout(reload?: boolean): void {
        localStorage.clear();
        let tempId = Math.floor(Math.random() * 1000000);
        localStorage.setItem("tempId", tempId.toString());
        this.authStatus.isLoginSubject.next(false);
        if (reload) {
            this._router.navigate(['/login']);
        }
    }

    authenticate(finallyCallback?: () => void): void {
        finallyCallback = finallyCallback || (() => { });

         this.authService.login(this.authenticateModel).subscribe((result: AuthenticationResponseResponse) => {
                console.log('result ', result);               
                this.processAuthenticateResult(result);
            });
    }

    private async processAuthenticateResult(authenticateResult) {
        this.authenticateResult = authenticateResult;
        if (authenticateResult.succeeded) {
            // Successfully logged in
            this.login(
                authenticateResult.data,
                this.rememberMe
            );
        } else {
            // Unexpected result!
            //await this.presentAlert('فشل', 'هناك خطأ في اسم المستخدم أو كلمة المرور', null)
            console.log('Unexpected authenticateResult!');
          
            this._router.navigate(['/login']);
        }
    }


    private login(
        authenticateResult: AuthenticationResponse,
        rememberMe?: boolean
    ): void {
        localStorage.setItem("event-token", authenticateResult.token);
        localStorage.setItem("event-email", authenticateResult.email);
        localStorage.setItem("event-username", authenticateResult.username);
        localStorage.setItem("event-id", authenticateResult.id);
        this.authStatus.isLoginSubject.next(true);
        this._router.navigate(['home']);
    }

    private clear(): void {
        this.authenticateModel = new LoginRequest();
        this.authenticateResult = null;
        this.rememberMe = false;
    }

   
}