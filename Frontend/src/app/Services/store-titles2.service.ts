import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { GetTitleSecondDto, SecondTitleClient } from 'src/shared/api/service-proxies';

@Injectable({
  providedIn: 'root'
})
export class StoreTitles2Service {
  private dataCache: { [key: number]: any[] } = {};
  private subjectTitles2 = new BehaviorSubject<GetTitleSecondDto[]>([]);
  private subjectHasNext = new BehaviorSubject<boolean>(false);
  private subjectHasPrev = new BehaviorSubject<boolean>(false);
  private subjectTotalPages = new BehaviorSubject<number>(0);
  private subjectAllTitles2 = new BehaviorSubject<GetTitleSecondDto[]>([]);
  
  Titles2$ : Observable<GetTitleSecondDto[]> = this.subjectTitles2.asObservable();
  AllTitles2$ : Observable<GetTitleSecondDto[]> = this.subjectAllTitles2.asObservable(); 
  
  has_next$ : Observable<boolean> = this.subjectHasNext.asObservable();
  has_prev$ : Observable<boolean> = this.subjectHasPrev.asObservable();
  total_pages$ : Observable<number> = this.subjectTotalPages.asObservable();
  
  size =10;
  has_next= false;
    constructor(
      private secondTitleService : SecondTitleClient,
      private http: HttpClient) { }
      loadAllTitles2(){
    
        this.secondTitleService.getAllData().subscribe((res)=>{
              this.subjectAllTitles2.next(res.data);
            });
          
      }
    loadSecondTitle_pageN(page){
      if (this.dataCache[page]) {
        this.subjectTitles2.next(this.dataCache[page]);}
      else{
      this.secondTitleService.getAll(page,this.size).subscribe((res)=>{
           this.dataCache[page] = res.data.data;
            this.subjectTitles2.next(res.data.data);
            this.subjectHasNext.next(res.data.hasNextPage);
            this.subjectHasPrev.next(res.data.hasPreviousPage);
            this.subjectTotalPages.next(res.data.totalPages);
          });
        }
    }

    reloadTitleSecondN(page){
   
      this.secondTitleService.getAll(page,this.size).subscribe((res)=>{
           this.dataCache[page] = res.data.data;
            this.subjectTitles2.next(res.data.data);
            this.subjectHasNext.next(res.data.hasNextPage);
            this.subjectHasPrev.next(res.data.hasPreviousPage);
            this.subjectTotalPages.next(res.data.totalPages);
          });
    }
  }
  