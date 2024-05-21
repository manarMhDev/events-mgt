import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { FirstTitleClient, GetTitleFirstDto } from 'src/shared/api/service-proxies';

@Injectable({
  providedIn: 'root'
})
export class StoreTitles1Service {
private dataCache: { [key: number]: any[] } = {};
private subjectTitles1 = new BehaviorSubject<GetTitleFirstDto[]>([]);
private subjectAllTitles1 = new BehaviorSubject<GetTitleFirstDto[]>([]);

private subjectHasNext = new BehaviorSubject<boolean>(false);
private subjectHasPrev = new BehaviorSubject<boolean>(false);
private subjectTotalPages = new BehaviorSubject<number>(0);

Titles1$ : Observable<GetTitleFirstDto[]> = this.subjectTitles1.asObservable();
AllTitles1$ : Observable<GetTitleFirstDto[]> = this.subjectAllTitles1.asObservable(); 

has_next$ : Observable<boolean> = this.subjectHasNext.asObservable();
has_prev$ : Observable<boolean> = this.subjectHasPrev.asObservable();
total_pages$ : Observable<number> = this.subjectTotalPages.asObservable();
size =10;
has_next= false;
  constructor(private firstTitleService : FirstTitleClient) {
   }
   loadAllTitles1(){
    
    this.firstTitleService.getAllData().subscribe((res)=>{
          this.subjectAllTitles1.next(res.data);
        });
      
  }
  loadFirstTitle_pageN(page){
    if (this.dataCache[page]) {
      this.subjectTitles1.next(this.dataCache[page]);}
    else{
    this.firstTitleService.getAll(page,this.size).subscribe((res)=>{
         this.dataCache[page] = res.data.data;
          this.subjectTitles1.next(res.data.data);
          this.subjectHasNext.next(res.data.hasNextPage);
          this.subjectHasPrev.next(res.data.hasPreviousPage);
          this.subjectTotalPages.next(res.data.totalPages);
        });
      }
  }
  
  reloadTitleFirstN(page){
   
    this.firstTitleService.getAll(page,this.size).subscribe((res)=>{
         this.dataCache[page] = res.data.data;
          this.subjectTitles1.next(res.data.data);
          this.subjectHasNext.next(res.data.hasNextPage);
          this.subjectHasPrev.next(res.data.hasPreviousPage);
          this.subjectTotalPages.next(res.data.totalPages);
        });
  }

}
