import { Component } from '@angular/core';
import { AppLanguageType } from 'src/shared/AppEnums';
import { FirstTitleClient, GetPersonTypeDto, GetTitleFirstDto, Language, PersonTypeClient } from 'src/shared/api/service-proxies';
import {MatDialog, MatDialogModule,  MAT_DIALOG_DATA,} from '@angular/material/dialog';
import { Title1FormComponent } from 'src/app/Components/title1-form/title1-form.component';
import { ConfirmAlertComponent } from 'src/app/Components/confirm-alert/confirm-alert.component';
import { StorePersonTypesService } from 'src/app/Services/store-person-types.service';
import { PersonTypeFormComponent } from 'src/app/Components/person-type-form/person-type-form.component';

@Component({
  selector: 'app-person-types',
  templateUrl: './person-types.component.html',
  styleUrls: ['./person-types.component.scss']
})
export class PersonTypesComponent {
  personTypes : GetPersonTypeDto[] = [];
  _languageType = Language;
  _appLanguageType = AppLanguageType.getName;
 total_pages;current_page=1;has_next=false;has_prev=false;
  
  constructor(private store : StorePersonTypesService,
    private personTypeService : PersonTypeClient,
    private dialog: MatDialog){

  }

  ngOnInit(){
    this.store.loadPersonTypes_pageN(1);
    this.store.PersonTypes$.subscribe((res:GetPersonTypeDto[])=>{
      this.personTypes = res;
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
    let title = 'إضافة فئة';
    if(id!)
    title = 'تعديل فئة ';
    const dialogRef = this.dialog.open(PersonTypeFormComponent,{
      disableClose: true,
      width:'50%',
      data :{
        title:title,
        id : id
      }
    });
    dialogRef.afterClosed().subscribe(result => {
      if(result != null && result.event == true){
        this.store.reloadPersonTypesN(this.current_page);
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
  this.store.loadPersonTypes_pageN(this.current_page);
}
nextPage(){
  this.current_page = ++this.current_page;
  this.store.loadPersonTypes_pageN(this.current_page);
  console.log(this.current_page); 
}
prevPage(){
  this.current_page = this.current_page -1;
  this.store.loadPersonTypes_pageN(this.current_page);
  console.log("page ", this.current_page)
   
}
getPage(page){

  this.store.loadPersonTypes_pageN(page);
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
       this.personTypeService.deleteOne(id).subscribe((res=>{
        this.store.reloadPersonTypesN(this.current_page);
}))
      }
    });
  }
}
