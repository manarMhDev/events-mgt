import { Component, EventEmitter, Output , Inject, ViewChild} from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { StoreEventPlacesService } from 'src/app/Services/store-event-places.service';
import { StoreEventsService } from 'src/app/Services/store-events.service';
import { BooleanResponse, CreateEventDto, EventPlaceDto, EventsClient } from 'src/shared/api/service-proxies';

export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}
@Component({
  selector: 'app-event-form',
  templateUrl: './event-form.component.html',
  styleUrls: ['./event-form.component.scss']
})

export class EventFormComponent {
  basicForm: FormGroup;
  objId ;
  formData = new FormData();
  matcher = new MyErrorStateMatcher();
 places : EventPlaceDto[];
 @Output() updated: EventEmitter<any> = new EventEmitter<any>();
   constructor(
     private dialogRef: MatDialogRef<EventFormComponent>,
     @Inject(MAT_DIALOG_DATA) public data :any,
     public formBuilder: FormBuilder,
     private eventsClient : EventsClient,
     private store : StoreEventsService,
     private storePlaces : StoreEventPlacesService
     ){ }
   ngOnInit(){
    this.storePlaces.loadEventPlaces_pageN(1);
      this.objId= this.data !==null ? this.data.id : null;
     this.basicForm = this.formBuilder.group({
      NameArabic: ['', [Validators.required, Validators.minLength(3)]],
      NameEnglish: ['', [Validators.required, Validators.minLength(3)]],
      Description : [""],
       EventDate : [{disabled: true},Validators.required],
       EventPlaceId : [null,Validators.required]
     });
     this.storePlaces.loadAllEventPlaces();
     this.storePlaces.AllEventPlaces$.subscribe((res)=>{
      this.places = res;
     });

   }
 
   ngAfterViewInit(){
    console.log(this.data.id);
     if(this.data.id){
       this.eventsClient.getOne(this.data.id).subscribe((res)=>{
         this.basicForm = this.formBuilder.group({
          NameArabic: [res.data.nameArabic, [Validators.required, Validators.minLength(3)]],
          NameEnglish: [res.data.nameEnglish, [Validators.required, Validators.minLength(3)]],
           Description: [res.data.description,Validators.required],
           EventDate: [res.data.eventDate,Validators.required],
           EventPlaceId: [res.data.eventPlaceId,Validators.required],
         });
       });
     }
  
   }
   get errorControl() {
     return this.basicForm.controls;
   }

   submitForm(){
     if(this.basicForm.valid){
       console.log(this.basicForm.value['NameArabic']);
       console.log(this.basicForm.value['NameEnglish']);
       console.log(this.basicForm.value['Description']);
       console.log(this.basicForm.value['EventPlaceId']);
       console.log(this.basicForm.value['EventDate']);

       if(!this.data.id)
       {
        let model = new CreateEventDto();
        model.nameArabic = this.basicForm.value['NameArabic'];
        model.nameEnglish = this.basicForm.value['NameEnglish'];
        model.eventPlaceId = this.basicForm.value['EventPlaceId'];
        model.eventDate = new Date(this.basicForm.value['EventDate'])
        model.description = this.basicForm.value['Description'];
         this.eventsClient.createOne(model).subscribe((res : BooleanResponse)=>{
           this.dialogRef.close({event:res.succeeded});
          });
       }
       else{
         this.formData.append('id',this.objId.toString());
         this.formData.append('NameArabic',this.basicForm.value['NameArabic']);
         this.formData.append('NameEnglish',this.basicForm.value['NameEnglish']);
         this.formData.append('EventDate',(new Date(this.basicForm.value['EventDate'])).toUTCString());
         this.formData.append('Description',this.basicForm.value['Description']);
         this.formData.append('EventPlaceId',this.basicForm.value['EventPlaceId']);
       
          this.store.editEvent(this.formData).subscribe((res : BooleanResponse)=>{
            this.dialogRef.close({event:res.succeeded});
           });
       }
    
     }
 
   }
   close(){
     this.dialogRef.close();
   }
}
