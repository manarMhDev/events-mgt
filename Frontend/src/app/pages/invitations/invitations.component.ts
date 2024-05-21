import { Component } from '@angular/core';
import { AppFormType, AppInvitationStatus, AppLanguageType } from 'src/shared/AppEnums';
import { GetInvitationsDto, InvitationClient, Language, SeatingChart } from 'src/shared/api/service-proxies';
import { MatDialog, MatDialogModule,  MAT_DIALOG_DATA,} from '@angular/material/dialog';
import { ConfirmAlertComponent } from 'src/app/Components/confirm-alert/confirm-alert.component';
import { AppConsts } from 'src/shared/AppConsts';
import { StoreInvitationsService } from 'src/app/Services/store-invitations.service';
import { InvitationFormComponent } from 'src/app/Components/invitation-form/invitation-form.component';
import { BookSeatFormComponent } from 'src/app/Components/book-seat-form/book-seat-form.component';

@Component({
  selector: 'app-invitations',
  templateUrl: './invitations.component.html',
  styleUrls: ['./invitations.component.scss']
})
export class InvitationsComponent {
  invitaions : GetInvitationsDto[] = [];
  AppConsts = AppConsts;
  _languageType = Language;
  _appLanguageType = AppLanguageType.getName;
  _seatingType = SeatingChart;
  _appInvitationStatusType = AppInvitationStatus.getName;
  _appFormType = AppFormType.getName;
  total_pages;current_page=1;has_next=false;has_prev=false;
  constructor(private store : StoreInvitationsService,
    private service: InvitationClient,
    private dialog: MatDialog){
  }
  ngOnInit(){
    this.store.loadInvitations_pageN(1);
    this.store.Invitations$.subscribe((res:GetInvitationsDto[])=>{
      this.invitaions = res;
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
  this.store.loadInvitations_pageN(this.current_page);
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
bookASeat(id){
  let title = 'حجز مقعد';
  const dialogRef = this.dialog.open(BookSeatFormComponent,{
    panelClass: 'custom-dialog-container',
    disableClose: true,
    width:'50%',
    maxHeight: '90vh',
    data :{
      title:title,
      id : id
    }
  });
  dialogRef.afterClosed().subscribe(result => {
    if(result != null && result.event == true){
      this.store.reloadInvitationsN(this.current_page);
    }
  });
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
trackByFn(index: number, item: any): any {
  return item.id; // Replace "id" with the unique identifier of your data item
}

  openAddTitleDialog(id?:number) {
    let title = 'إضافة دعوة';
    if(id!)
    title = 'تعديل دعوة';
    const dialogRef = this.dialog.open(InvitationFormComponent,{
      panelClass: 'custom-dialog-container',
      disableClose: true,
      width:'50%',
      maxHeight: '90vh',
      data :{
        title:title,
        id : id
      }
    });
    dialogRef.afterClosed().subscribe(result => {
      if(result != null && result.event == true){
        this.store.reloadInvitationsN(this.current_page);
      }
    });
  }

}
