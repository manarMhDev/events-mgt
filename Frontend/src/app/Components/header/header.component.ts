import { Component } from '@angular/core';
import { AuthStatusService } from 'src/app/Services/auth-status.service';
import { AppAuthService } from 'src/shared/auth/app-auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent {
   username;
   isLoggedIn;
constructor(private auth: AppAuthService,
  private authState : AuthStatusService){  }
ngOnInit(){
  let name = localStorage.getItem('event-username');
  this.username = `مرحبا ${ name }`;
  this.isLoggedIn = this.authState.isLoggedIn();
}
logout(){
this.auth.logout(true);
}
}
