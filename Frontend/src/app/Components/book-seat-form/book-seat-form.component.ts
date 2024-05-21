import { Component, EventEmitter, Output , Inject} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { AllSeatsRequestDto, BookASeatDto, CreateFirstTitleDto, CreatePersonTypeDto, FirstTitleClient, GetSeatsDto, InvitationClient, PersonTypeClient, SeatsClient, UpdatePersonTypeDto, UpdateTitleDto } from 'src/shared/api/service-proxies';

@Component({
  selector: 'app-book-seat-form',
  templateUrl: './book-seat-form.component.html',
  styleUrls: ['./book-seat-form.component.scss']
})
export class BookSeatFormComponent {
  color="#000000";
  basicForm: FormGroup;
  obj : UpdatePersonTypeDto;
  seats : GetSeatsDto[] = [];
 @Output() updated: EventEmitter<any> = new EventEmitter<any>();
   constructor(
     private dialogRef: MatDialogRef<BookSeatFormComponent>,
     @Inject(MAT_DIALOG_DATA) public data :any,
     public formBuilder: FormBuilder,
     private seatsService : SeatsClient,
     private invitationService : InvitationClient
     ){ }
   ngOnInit(){
     // this.objId= this.data !==null ? this.data.id : null;
     this.basicForm = this.formBuilder.group({
       seatId: ['0',Validators.required]
     });
   }
   ngAfterViewInit(){
     if(this.data.id){
      this.invitationService.getOne(this.data.id).subscribe((invitation)=>{
        let model = new AllSeatsRequestDto();
        model.eventId = invitation.data.eventId;
        this.seatsService.nonTakenSeats(model).subscribe((res)=>{
          this.seats = res.data;
        });
      })
     
     }
  
   }
   get errorControl() {
     return this.basicForm.controls;
   }
   submitForm(){
     if(this.basicForm.valid){
       console.log(this.basicForm.value['seatId']);
         let model = new BookASeatDto();
          model.init({
            invitationId : this.data.id,
            seatPlaceId : this.basicForm.value['seatId'], 
       });
         this.seatsService.bookSeat(model).subscribe((res)=>{
           this.dialogRef.close({event:res.succeeded});
          });
     
    
     }
 
   }
   close(){
     this.dialogRef.close();
   }
}
