import { Component } from '@angular/core';
import { StoreTitles1Service } from 'src/app/Services/store-titles1.service';
import { AppLanguageType } from 'src/shared/AppEnums';
import { FirstTitleClient, GetTitleFirstDto, Language } from 'src/shared/api/service-proxies';
import {MatDialog, MatDialogModule,  MAT_DIALOG_DATA,} from '@angular/material/dialog';
import { Title1FormComponent } from 'src/app/Components/title1-form/title1-form.component';
import { ConfirmAlertComponent } from 'src/app/Components/confirm-alert/confirm-alert.component';
@Component({
  selector: 'app-titles1',
  templateUrl: './titles1.component.html',
  styleUrls: ['./titles1.component.scss']
})
export class Titles1Component {
  firstTitles : GetTitleFirstDto[] = [];
  _languageType = Language;
  _appLanguageType = AppLanguageType.getName;
 total_pages;current_page=1;has_next=false;has_prev=false;
  
  constructor(private store : StoreTitles1Service,
    private firstTitleService: FirstTitleClient,
    private dialog: MatDialog){

  }

  ngOnInit(){
    this.store.loadFirstTitle_pageN(1);
    this.store.Titles1$.subscribe((res:GetTitleFirstDto[])=>{
      this.firstTitles = res;
     });
     this.store.total_pages$.subscribe((res)=>{
      this.total_pages = res;
     });
     this.store.has_next$.subscribe((res)=>{
      this.has_next = res;
     });
     this.store.has_prev$.subscribe((res)=>{
      this.has_prev = res;
     });
  }
  openAddTitleDialog(id?:number) {
    let title = 'إضافة لقب 1';
    if(id!)
    title = 'تعديل لقب 1';
    const dialogRef = this.dialog.open(Title1FormComponent,{
      disableClose: true,
      width:'50%',
      data :{
        title:title,
        id : id
      }
    });
    dialogRef.afterClosed().subscribe(result => {
      if(result != null && result.event == true){
        this.store.reloadTitleFirstN(this.current_page);
      }
    });
  }
  counter(i: number) {
    return new Array(i);
}
onPageChange(page: number): void {
  this.current_page = page;
  this.fetchData();
}
fetchData(): void {
  this.store.loadFirstTitle_pageN(this.current_page);
}
nextPage(){
  this.current_page = ++this.current_page;
  this.store.loadFirstTitle_pageN(this.current_page);
  console.log(this.current_page); 
}
prevPage(){
  this.current_page = this.current_page -1;
  this.store.loadFirstTitle_pageN(this.current_page);
  console.log("page ", this.current_page)
   
}
getPage(page){

  this.store.loadFirstTitle_pageN(page);
    this.current_page = page;
   this.store.total_pages$.subscribe((res)=>{
    this.total_pages = res;
   });
   this.store.has_next$.subscribe((res)=>{
    this.has_next = res;
   });
   this.store.has_prev$.subscribe((res)=>{
    this.has_prev = res;
   });
}
get pageNumbers(): number[] {
  return Array.from({ length: this.total_pages }, (_, index) => index + 1);
}
goToPreviousPage(): void {
  if (this.current_page > 1) {
    this.current_page--;
    this.fetchData();
  }
}

goToNextPage(): void {
  if (this.current_page < this.total_pages) {
    this.current_page++;
    this.fetchData();
  }
}

goToPage(event): void {
  console.log(event.target.value);
  const pageNumber = parseInt(event.target.value, 10);
  if (pageNumber && pageNumber >= 1 && pageNumber <= this.total_pages && pageNumber !== this.current_page) {
    this.current_page = pageNumber;
    this.fetchData();
  }
}
trackByFn(index: number, item: any): any {
  return item.id; // Replace "id" with the unique identifier of your data item
}
  alertDelete(id:number){
    const dialogRef = this.dialog.open(ConfirmAlertComponent,{
      width:'30%'
    });
    dialogRef.afterClosed().subscribe(result => {
      if(result != null && result.event == true){
       this.firstTitleService.deleteOne(id).subscribe((res=>{
        this.store.reloadTitleFirstN(this.current_page);
}))
      }
    });
  }
}
