import { Component } from '@angular/core';
import { StoreTitles1Service } from './Services/store-titles1.service';
import { AuthStatusService } from './Services/auth-status.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'events managements';
  constructor(private store : StoreTitles1Service,
    private auth : AuthStatusService){
    auth.isLoggedIn().subscribe((res)=>{
      // if(res){
      //   this.loadStore();
      // }
  });
    
  }

   
}

