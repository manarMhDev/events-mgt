import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthStatusService {
  isLoginSubject = new BehaviorSubject<boolean>(this.hasToken());
  private hasToken() : boolean {
    return !!localStorage.getItem('event-token');
  }
  isLoggedIn() : Observable<boolean> {
    return this.isLoginSubject.asObservable();
   }
}