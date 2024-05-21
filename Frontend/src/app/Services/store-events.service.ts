
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import {  EventsClient, GetEventsDto } from 'src/shared/api/service-proxies';

@Injectable({
  providedIn: 'root'
})
export class StoreEventsService {
  private dataCache: { [key: number]: any[] } = {};
  private subjectEvents = new BehaviorSubject<GetEventsDto[]>([]);
  private subjectAllEvents = new BehaviorSubject<GetEventsDto[]>([]);
  
  private subjectHasNext = new BehaviorSubject<boolean>(false);
  private subjectHasPrev = new BehaviorSubject<boolean>(false);
  private subjectTotalPages = new BehaviorSubject<number>(0);
  
  Events$ : Observable<GetEventsDto[]> = this.subjectEvents.asObservable();
  AllEvents$ : Observable<GetEventsDto[]> = this.subjectAllEvents.asObservable(); 
  
  has_next$ : Observable<boolean> = this.subjectHasNext.asObservable();
  has_prev$ : Observable<boolean> = this.subjectHasPrev.asObservable();
  total_pages$ : Observable<number> = this.subjectTotalPages.asObservable();
  size =10;
  has_next= false;
    constructor(private eventsService : EventsClient,
       private http: HttpClient) {
     }
     loadAllEvents(){
    
      this.eventsService.getAllData().subscribe((res)=>{
            this.subjectAllEvents.next(res.data);
          });
        
    }
  
    loadEvents_pageN(page){
      if (this.dataCache[page]) {
        this.subjectEvents.next(this.dataCache[page]);}
      else{
      this.eventsService.getAll(page,this.size).subscribe((res)=>{
           this.dataCache[page] = res.data.data;
            this.subjectEvents.next(res.data.data);
            this.subjectHasNext.next(res.data.hasNextPage);
            this.subjectHasPrev.next(res.data.hasPreviousPage);
            this.subjectTotalPages.next(res.data.totalPages);
          });
        }
    }
    
    reloadEventsN(page){
     
      this.eventsService.getAll(page,this.size).subscribe((res)=>{
           this.dataCache[page] = res.data.data;
            this.subjectEvents.next(res.data.data);
            this.subjectHasNext.next(res.data.hasNextPage);
            this.subjectHasPrev.next(res.data.hasPreviousPage);
            this.subjectTotalPages.next(res.data.totalPages);
          });
    }
    createEvent(formData :any){
      return this.http.post('https://localhost:7268/events/create-one', formData);
    }
    editEvent(formData :any){
       return this.http.patch('https://localhost:7268/events/edit-one', formData);
    }
  }