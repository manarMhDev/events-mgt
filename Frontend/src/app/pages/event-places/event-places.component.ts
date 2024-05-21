import { Component } from '@angular/core';
import { AppLanguageType, AppSeatingType } from 'src/shared/AppEnums';
import {  EventPlaceClient, EventPlaceDto, Language, SeatingChart } from 'src/shared/api/service-proxies';
import {MatDialog, MatDialogModule,  MAT_DIALOG_DATA,} from '@angular/material/dialog';
import { ConfirmAlertComponent } from 'src/app/Components/confirm-alert/confirm-alert.component';
import { AppConsts } from 'src/shared/AppConsts';
import { StoreEventPlacesService } from 'src/app/Services/store-event-places.service';
import { EventPlaceFormComponent } from 'src/app/Components/event-place-form/event-place-form.component';

@Component({
  selector: 'app-event-places',
  templateUrl: './event-places.component.html',
  styleUrls: ['./event-places.component.scss']
})
export class EventPlacesComponent {
  eventPlaces : EventPlaceDto[] = [];
  AppConsts = AppConsts;
  _languageType = Language;
  _appLanguageType = AppLanguageType.getName;
  _seatingType = SeatingChart;
  _appSeatingType = AppSeatingType.getName;
  total_pages;current_page=1;has_next=false;has_prev=false;
  constructor(private store : StoreEventPlacesService,
    private service: EventPlaceClient,
    private dialog: MatDialog){
  }
  ngOnInit(){
    this.store.loadEventPlaces_pageN(1);
    this.store.EventPlaces$.subscribe((res:EventPlaceDto[])=>{
      this.eventPlaces = res;
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
  this.store.loadEventPlaces_pageN(this.current_page);
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
        this.store.reloadEventPlacesN(this.current_page);
}))
      }
    });
  }
  openAddTitleDialog(id?:number) {
    let title = 'إضافة مكان فعالية';
    if(id!)
    title = 'تعديل مكان فعالية';
    const dialogRef = this.dialog.open(EventPlaceFormComponent,{
      disableClose: true,
      width:'50%',
      data :{
        title:title,
        id : id
      }
    });
    dialogRef.afterClosed().subscribe(result => {
      if(result != null && result.event == true){
        this.store.reloadEventPlacesN(this.current_page);
      }
    });
  }

}
