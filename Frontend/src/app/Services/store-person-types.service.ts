
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { GetPersonTypeDto, PersonTypeClient } from 'src/shared/api/service-proxies';


@Injectable({
  providedIn: 'root'
})
export class StorePersonTypesService {
  private dataCache: { [key: number]: any[] } = {};
  private subjectPersonTypes = new BehaviorSubject<GetPersonTypeDto[]>([]);
  private subjectAllPersonTypes = new BehaviorSubject<GetPersonTypeDto[]>([]);

  private subjectHasNext = new BehaviorSubject<boolean>(false);
  private subjectHasPrev = new BehaviorSubject<boolean>(false);
  private subjectTotalPages = new BehaviorSubject<number>(0);
  
  PersonTypes$ : Observable<GetPersonTypeDto[]> = this.subjectPersonTypes.asObservable();
  AllPersonTypes$ : Observable<GetPersonTypeDto[]> = this.subjectAllPersonTypes.asObservable(); 
  
  has_next$ : Observable<boolean> = this.subjectHasNext.asObservable();
  has_prev$ : Observable<boolean> = this.subjectHasPrev.asObservable();
  total_pages$ : Observable<number> = this.subjectTotalPages.asObservable();
  size =10;
  has_next= false;
    constructor(private personTypeService : PersonTypeClient) {
     }
     loadAllPersonTypes(){
    
      this.personTypeService.getAllData().subscribe((res)=>{
            this.subjectAllPersonTypes.next(res.data);
          });
        
    }
    loadPersonTypes_pageN(page){
      if (this.dataCache[page]) {
        this.subjectPersonTypes.next(this.dataCache[page]);}
      else{
      this.personTypeService.getAll(page,this.size).subscribe((res)=>{
           this.dataCache[page] = res.data.data;
            this.subjectPersonTypes.next(res.data.data);
            this.subjectHasNext.next(res.data.hasNextPage);
            this.subjectHasPrev.next(res.data.hasPreviousPage);
            this.subjectTotalPages.next(res.data.totalPages);
          });
        }
    }
    
    reloadPersonTypesN(page){
     
      this.personTypeService.getAll(page,this.size).subscribe((res)=>{
           this.dataCache[page] = res.data.data;
            this.subjectPersonTypes.next(res.data.data);
            this.subjectHasNext.next(res.data.hasNextPage);
            this.subjectHasPrev.next(res.data.hasPreviousPage);
            this.subjectTotalPages.next(res.data.totalPages);
          });
    }
  
  }
  