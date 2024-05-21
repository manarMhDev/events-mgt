import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { GetSeatsTypeDto, SeatsTypesClient } from 'src/shared/api/service-proxies';

@Injectable({
  providedIn: 'root'
})
export class StoreSeatsTypesService {
private subjectSeatTypes = new BehaviorSubject<GetSeatsTypeDto[]>([]);
SeatTypes$ : Observable<GetSeatsTypeDto[]> = this.subjectSeatTypes.asObservable();
private dataCache: { [key: number]: any[] } = {};
private subjectHasNext = new BehaviorSubject<boolean>(false);
private subjectHasPrev = new BehaviorSubject<boolean>(false);
private subjectTotalPages = new BehaviorSubject<number>(0);
has_next$ : Observable<boolean> = this.subjectHasNext.asObservable();
has_prev$ : Observable<boolean> = this.subjectHasPrev.asObservable();
total_pages$ : Observable<number> = this.subjectTotalPages.asObservable();
size =10;
has_next= false;
  constructor(private seatTypesService : SeatsTypesClient,
    private http: HttpClient) { }

  loadSeatTypes_pageN(page){
    if (this.dataCache[page]) {
      this.subjectSeatTypes.next(this.dataCache[page]);}
    else{
    this.seatTypesService.getAll(page,this.size).subscribe((res)=>{
         this.dataCache[page] = res.data.data;
          this.subjectSeatTypes.next(res.data.data);
          this.subjectHasNext.next(res.data.hasNextPage);
          this.subjectHasPrev.next(res.data.hasPreviousPage);
          this.subjectTotalPages.next(res.data.totalPages);
        });
      }
  }
  loadAllSeatType(){
    
    this.seatTypesService.getAllData().subscribe((res)=>{
          this.subjectSeatTypes.next(res.data);
        });
      
  }
  reloadSeatTypesN(page){
   
    this.seatTypesService.getAll(page,this.size).subscribe((res)=>{
         this.dataCache[page] = res.data.data;
          this.subjectSeatTypes.next(res.data.data);
          this.subjectHasNext.next(res.data.hasNextPage);
          this.subjectHasPrev.next(res.data.hasPreviousPage);
          this.subjectTotalPages.next(res.data.totalPages);
        });
  }
  createSeatType(formData :any){
    return this.http.post('https://localhost:7268/seats-types/create-one', formData);
  }
  editSeatType(formData :any){
     return this.http.patch('https://localhost:7268/seats-types/edit-one', formData);
  }
}
