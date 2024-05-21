import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { GetInvitationsDto, InvitationClient } from 'src/shared/api/service-proxies';

@Injectable({
  providedIn: 'root'
})
export class StoreInvitationsService {
  private dataCache: { [key: number]: any[] } = {};
  private subjectInvitations = new BehaviorSubject<GetInvitationsDto[]>([]);
  
  private subjectHasNext = new BehaviorSubject<boolean>(false);
  private subjectHasPrev = new BehaviorSubject<boolean>(false);
  private subjectTotalPages = new BehaviorSubject<number>(0);
  
  Invitations$ : Observable<GetInvitationsDto[]> = this.subjectInvitations.asObservable();
  
  has_next$ : Observable<boolean> = this.subjectHasNext.asObservable();
  has_prev$ : Observable<boolean> = this.subjectHasPrev.asObservable();
  total_pages$ : Observable<number> = this.subjectTotalPages.asObservable();
  size =10;
  has_next= false;
    constructor(private invitationsService : InvitationClient,
       private http: HttpClient) {
     }
     reloadEventsN(page){
     
      this.invitationsService.getAll(page,this.size).subscribe((res)=>{
           this.dataCache[page] = res.data.data;
            this.subjectInvitations.next(res.data.data);
            this.subjectHasNext.next(res.data.hasNextPage);
            this.subjectHasPrev.next(res.data.hasPreviousPage);
            this.subjectTotalPages.next(res.data.totalPages);
          });
    }
    loadInvitations_pageN(page){
      if (this.dataCache[page]) {
        this.subjectInvitations.next(this.dataCache[page]);}
      else{
      this.invitationsService.getAll(page,this.size).subscribe((res)=>{
           this.dataCache[page] = res.data.data;
            this.subjectInvitations.next(res.data.data);
            this.subjectHasNext.next(res.data.hasNextPage);
            this.subjectHasPrev.next(res.data.hasPreviousPage);
            this.subjectTotalPages.next(res.data.totalPages);
          });
        }
    }
    
    reloadInvitationsN(page){
     
      this.invitationsService.getAll(page,this.size).subscribe((res)=>{
           this.dataCache[page] = res.data.data;
            this.subjectInvitations.next(res.data.data);
            this.subjectHasNext.next(res.data.hasNextPage);
            this.subjectHasPrev.next(res.data.hasPreviousPage);
            this.subjectTotalPages.next(res.data.totalPages);
          });
    }
    createInvitation(formData :any){
      return this.http.post('https://localhost:7268/invitation/create-one', formData);
    }
    editInvitation(formData :any){
       return this.http.patch('https://localhost:7268/invitation/edit-one', formData);
    }
  }