import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { GetSeatsDto, SeatsClient } from 'src/shared/api/service-proxies';

@Injectable({
  providedIn: 'root'
})
export class StoreSeatsService {
private subjectSeats = new BehaviorSubject<GetSeatsDto[]>([]);
Seats$ : Observable<GetSeatsDto[]> = this.subjectSeats.asObservable();
private dataCache: { [key: number]: any[] } = {};
private subjectHasNext = new BehaviorSubject<boolean>(false);
private subjectHasPrev = new BehaviorSubject<boolean>(false);
private subjectTotalPages = new BehaviorSubject<number>(0);
has_next$ : Observable<boolean> = this.subjectHasNext.asObservable();
has_prev$ : Observable<boolean> = this.subjectHasPrev.asObservable();
total_pages$ : Observable<number> = this.subjectTotalPages.asObservable();
size =10;
has_next= false;
  constructor(private seatsService : SeatsClient,
    private http: HttpClient) { }

  loadSeats_pageN(eventId,page){

    this.seatsService.getAll(eventId,page,this.size).subscribe((res)=>{
          this.subjectSeats.next(res.data.data);
          this.subjectHasNext.next(res.data.hasNextPage);
          this.subjectHasPrev.next(res.data.hasPreviousPage);
          this.subjectTotalPages.next(res.data.totalPages);
        });
  }
  reloadSeatsN(eventId,page){
   
    this.seatsService.getAll(eventId,page,this.size).subscribe((res)=>{
          this.subjectSeats.next(res.data.data);
          this.subjectHasNext.next(res.data.hasNextPage);
          this.subjectHasPrev.next(res.data.hasPreviousPage);
          this.subjectTotalPages.next(res.data.totalPages);
        });
  }
 
}
