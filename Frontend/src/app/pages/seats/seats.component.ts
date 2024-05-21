import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmAlertComponent } from 'src/app/Components/confirm-alert/confirm-alert.component';
import { StoreEventsService } from 'src/app/Services/store-events.service';
import { StoreSeatsService } from 'src/app/Services/store-seats.service';
import { GetEventsDto, GetSeatsDto, SeatsClient } from 'src/shared/api/service-proxies';

@Component({
  selector: 'app-seats',
  templateUrl: './seats.component.html',
  styleUrls: ['./seats.component.scss']
})
export class SeatsComponent {
  seats : GetSeatsDto[] = [];
  total_pages;current_page=1;has_next=false;has_prev=false;
  currentItem = null;
  eventId = 0;
  events : GetEventsDto[];
  constructor(private store : StoreSeatsService,
    private seatsService : SeatsClient,
    private dialog: MatDialog,
    private storeEvents : StoreEventsService,
  ){

  }
  ngOnInit(){
   // this.store.loadSeats_pageN(this.eventId,1);
    this.store.Seats$.subscribe((res:GetSeatsDto[])=>{
      this.seats = res;
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
     this.storeEvents.loadAllEvents();
     this.storeEvents.AllEvents$.subscribe((res)=>{
      this.events = res;
     });
  }
  changeEvent(event)
  {
    if(event.isUserInput) {
      this.eventId =event.source.value;
      this.reload();
    }
  }
  openEditTitleDialog(id){
  this.currentItem = id;
  }
  alertDelete(id){
    const dialogRef = this.dialog.open(ConfirmAlertComponent,{
      width:'30%'
    });
    dialogRef.afterClosed().subscribe(result => {
      if(result != null && result.event == true){
       this.seatsService.deleteOne(id).subscribe((res=>{
        this.store.reloadSeatsN(this.eventId,this.current_page);
}))
      }
    });
  }
  reload(){
    this.store.loadSeats_pageN(this.eventId,this.current_page);
  }
  trackByFn(index: number, item: any): any {
    return item.id; // Replace "id" with the unique identifier of your data item
  }
  onPageChange(page: number): void {
    this.current_page = page;
    this.fetchData();
  }
  fetchData(): void {
    this.store.loadSeats_pageN(this.eventId,this.current_page);
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
}
