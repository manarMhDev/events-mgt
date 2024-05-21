import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { AppLanguageType } from 'src/shared/AppEnums';
import {  EventsClient, GetEventsDto, Language } from 'src/shared/api/service-proxies';
import {MatDialog, MatDialogModule,  MAT_DIALOG_DATA,} from '@angular/material/dialog';
import { ConfirmAlertComponent } from 'src/app/Components/confirm-alert/confirm-alert.component';
import { AppConsts } from 'src/shared/AppConsts';
import { StoreEventsService } from 'src/app/Services/store-events.service';
import { EventFormComponent } from 'src/app/Components/event-form/event-form.component';
import { MatTableDataSource } from '@angular/material/table';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { MatSort, Sort } from '@angular/material/sort';
import { LiveAnnouncer } from '@angular/cdk/a11y';


@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class EventsComponent implements AfterViewInit  {
  expandedElement: GetEventsDto | null;
  isTableExpanded = false;
  events : GetEventsDto[] = [];
  AppConsts = AppConsts;
  _languageType = Language;
  _appLanguageType = AppLanguageType.getName;
  dataEventsList = new MatTableDataSource();
  displayedEventsColumnsList: string[] = ['id', 'nameArabic', 'eventDate', 'Time', 'Actions'];
  
  @ViewChild(MatSort) sort: MatSort;
  total_pages;current_page=1;has_next=false;has_prev=false;
  constructor(private store : StoreEventsService,
    private service: EventsClient,
    private dialog: MatDialog,
    private _liveAnnouncer: LiveAnnouncer){
  }
  applyFilter(value) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataEventsList.filter = filterValue.trim().toLowerCase();
  }
  toggleTableRows() {
    this.isTableExpanded = !this.isTableExpanded;

    this.dataEventsList.data.forEach((row: any) => {
      row.isExpanded = this.isTableExpanded;
    })
  }
  ngAfterViewInit() {
    this.store.Events$.subscribe((res:GetEventsDto[])=>{
      this.events = res;
      this.dataEventsList.data = res;
      this.dataEventsList.sort = this.sort;
     });

  }
  //  /** Announce the change in sort state for assistive technology. */
  //  announceSortChange(sortState) {
  //   // This example uses English messages. If your application supports
  //   // multiple language, you would internationalize these strings.
  //   // Furthermore, you can customize the message to add additional
  //   // details about the values being sorted.
  //   if (sortState.direction) {
  //     this._liveAnnouncer.announce(`Sorted ${sortState.direction}ending`);
  //   } else {
  //     this._liveAnnouncer.announce('Sorting cleared');
  //   }
  // }
  ngOnInit(){


    this.store.loadEvents_pageN(1);
   
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
  this.store.loadEvents_pageN(this.current_page);
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
        this.store.reloadEventsN(this.current_page);
}))
      }
    });
  }
  openAddTitleDialog(id?:number) {
    let title = 'إضافة مناسبة';
    if(id!)
    title = 'تعديل مناسبة';
    const dialogRef = this.dialog.open(EventFormComponent,{
      disableClose: true,
      width:'50%',
      data :{
        title:title,
        id : id
      }
    });
    dialogRef.afterClosed().subscribe(result => {
      if(result != null && result.event == true){
        this.store.reloadEventsN(this.current_page);
      }
    });
  }

}
