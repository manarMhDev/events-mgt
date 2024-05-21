import { Component } from '@angular/core';
import { StoreTitles1Service } from 'src/app/Services/store-titles1.service';
import { AppLanguageType } from 'src/shared/AppEnums';
import { CreateFirstTitleDto, FirstTitleClient, GetSeatsTypeDto, GetTitleFirstDto, Language, SeatsTypesClient } from 'src/shared/api/service-proxies';
import {MatDialog, MatDialogModule,  MAT_DIALOG_DATA,} from '@angular/material/dialog';
import { Title1FormComponent } from 'src/app/Components/title1-form/title1-form.component';
import { ConfirmAlertComponent } from 'src/app/Components/confirm-alert/confirm-alert.component';
import { SeatTypeComponent } from 'src/app/Components/seat-type/seat-type.component';
import { StoreSeatsTypesService } from 'src/app/Services/store-seats-types.service';
import { AppConsts } from 'src/shared/AppConsts';

@Component({
  selector: 'app-seats-types',
  templateUrl: './seats-types.component.html',
  styleUrls: ['./seats-types.component.scss']
})
export class SeatsTypesComponent {
  seatTypes : GetSeatsTypeDto[] = [];
  _languageType = Language;AppConsts = AppConsts;
  _appLanguageType = AppLanguageType.getName;
  total_pages;current_page=1;has_next=false;has_prev=false;
  constructor(private store : StoreSeatsTypesService,
    private service: SeatsTypesClient,
    private dialog: MatDialog){

  }
  ngOnInit(){
    this.store.loadSeatTypes_pageN(1);
    this.store.SeatTypes$.subscribe((res:GetSeatsTypeDto[])=>{
      this.seatTypes = res;
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

  counter(i: number) {
    return new Array(i);
}
onPageChange(page: number): void {
  this.current_page = page;
  this.fetchData();
}
fetchData(): void {
  this.store.loadSeatTypes_pageN(this.current_page);
}
get pageNumbers(): number[] {
  return Array.from({ length: this.total_pages }, (_, index) => index + 1);
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
       this.service.deleteOne(id).subscribe((res=>{
        this.store.reloadSeatTypesN(this.current_page);
}))
      }
    });
  }
  openAddTitleDialog(id?:number) {
    let title = 'إضافة نوع كرسي';
    if(id!)
    title = 'تعديل نوع كرسي';
    const dialogRef = this.dialog.open(SeatTypeComponent,{
      disableClose: true,
      width:'50%',
      data :{
        title:title,
        id : id
      }
    });
    dialogRef.afterClosed().subscribe(result => {
      if(result != null && result.event == true){
        this.store.reloadSeatTypesN(this.current_page);
      }
    });
  }

}
