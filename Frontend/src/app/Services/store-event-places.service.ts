
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { EventPlaceClient, EventPlaceDto } from 'src/shared/api/service-proxies';

@Injectable({
  providedIn: 'root'
})
export class StoreEventPlacesService {
  private dataCache: { [key: number]: any[] } = {};
  private subjectEventPlaces = new BehaviorSubject<EventPlaceDto[]>([]);
  private subjectAllEventPlaces = new BehaviorSubject<EventPlaceDto[]>([]);

  private subjectHasNext = new BehaviorSubject<boolean>(false);
  private subjectHasPrev = new BehaviorSubject<boolean>(false);
  private subjectTotalPages = new BehaviorSubject<number>(0);
  
  EventPlaces$ : Observable<EventPlaceDto[]> = this.subjectEventPlaces.asObservable();
  AllEventPlaces$ : Observable<EventPlaceDto[]> = this.subjectAllEventPlaces.asObservable(); 
  
  has_next$ : Observable<boolean> = this.subjectHasNext.asObservable();
  has_prev$ : Observable<boolean> = this.subjectHasPrev.asObservable();
  total_pages$ : Observable<number> = this.subjectTotalPages.asObservable();
  size =10;
  has_next= false;
    constructor(private eventPlaceService : EventPlaceClient,
       private http: HttpClient) {
     }
  
    loadEventPlaces_pageN(page){
      if (this.dataCache[page]) {
        this.subjectEventPlaces.next(this.dataCache[page]);}
      else{
      this.eventPlaceService.getAll(page,this.size).subscribe((res)=>{
           this.dataCache[page] = res.data.data;
            this.subjectEventPlaces.next(res.data.data);
            this.subjectHasNext.next(res.data.hasNextPage);
            this.subjectHasPrev.next(res.data.hasPreviousPage);
            this.subjectTotalPages.next(res.data.totalPages);
          });
        }
    }
    loadAllEventPlaces(){
    
      this.eventPlaceService.getAllData().subscribe((res)=>{
            this.subjectAllEventPlaces.next(res.data);
          });
        
    }
    reloadEventPlacesN(page){
     
      this.eventPlaceService.getAll(page,this.size).subscribe((res)=>{
           this.dataCache[page] = res.data.data;
            this.subjectEventPlaces.next(res.data.data);
            this.subjectHasNext.next(res.data.hasNextPage);
            this.subjectHasPrev.next(res.data.hasPreviousPage);
            this.subjectTotalPages.next(res.data.totalPages);
          });
    }
    createEventPlace(formData :any){
      return this.http.post('https://localhost:7268/event-place/create-one', formData);
    }
    editEventPlace(formData :any){
       return this.http.patch('https://localhost:7268/event-place/edit-one', formData);
    }
  }
  