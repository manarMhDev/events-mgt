import { Component, EventEmitter, Output , Inject, ViewChild} from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { StoreEventPlacesService } from 'src/app/Services/store-event-places.service';
import { StoreEventsService } from 'src/app/Services/store-events.service';
import { StoreInvitationsService } from 'src/app/Services/store-invitations.service';
import { StorePersonTypesService } from 'src/app/Services/store-person-types.service';
import { StoreTitles1Service } from 'src/app/Services/store-titles1.service';
import { StoreTitles2Service } from 'src/app/Services/store-titles2.service';
import { AppInvitationStatus, AppLanguageType } from 'src/shared/AppEnums';
import { BooleanResponse, EventPlaceDto, EventsClient, FormType, GetEventsDto, GetPersonTypeDto, GetTitleFirstDto, GetTitleSecondDto, InvitationClient, InvitationCreateDto, InvitationStatus, UpdateInvitationDto } from 'src/shared/api/service-proxies';
export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}

@Component({
  selector: 'app-invitation-form',
  templateUrl: './invitation-form.component.html',
  styleUrls: ['./invitation-form.component.scss']
})

export class InvitationFormComponent {
  basicForm: FormGroup;
  objId ;
  formData = new FormData();
  matcher = new MyErrorStateMatcher();
  places : EventPlaceDto[];
  titles1 : GetTitleFirstDto[];
  titles2 : GetTitleSecondDto[];
  PersonTypes : GetPersonTypeDto[];
  events : GetEventsDto[];
  _invitationStatusType = InvitationStatus;
  _appInvitationStatusType = AppInvitationStatus.getName;
 @Output() updated: EventEmitter<any> = new EventEmitter<any>();
   constructor(
     private dialogRef: MatDialogRef<InvitationFormComponent>,
     @Inject(MAT_DIALOG_DATA) public data :any,
     public formBuilder: FormBuilder,
     private invitationsClient : InvitationClient,
     private store : StoreInvitationsService,
     private storeTitles1 : StoreTitles1Service,
     private storeTitles2 : StoreTitles2Service,
     private storeEvents : StoreEventsService,
     private storePersonTypes : StorePersonTypesService,
     ){ }
   ngOnInit(){
    this.storeTitles1.loadFirstTitle_pageN(1);
      this.objId= this.data !==null ? this.data.id : null;
     this.basicForm = this.formBuilder.group({
      fullName: ['', [Validators.required, Validators.minLength(3)]],
      title1: [null, [Validators.required]],
      title2: [null],
      personType: [null, [Validators.required]],
      phone : [""],
      email : [""],
      party : [""],
      position : [""],
      sendWhatsapp :  ["0",Validators.required],
      sendEmail:  ["0",Validators.required],
      confirmAttendance:  ["0",Validators.required],
      invitationStatus : ["1",Validators.required],
       Language: ["1",Validators.required],
       EventId : [null,Validators.required]
     });
     this.storeTitles1.loadAllTitles1();
     this.storeTitles1.AllTitles1$.subscribe((res)=>{
      this.titles1 = res;
     });
     this.storeTitles2.loadAllTitles2();
     this.storeTitles2.AllTitles2$.subscribe((res)=>{
      this.titles2 = res;
     });
     this.storePersonTypes.loadAllPersonTypes();
     this.storePersonTypes.AllPersonTypes$.subscribe((res)=>{
      this.PersonTypes = res;
     });
     this.storeEvents.loadAllEvents();
     this.storeEvents.AllEvents$.subscribe((res)=>{
      this.events = res;
     });
   }
  
   enumKeys<O extends object, K extends keyof O = keyof O>(obj: O): K[] {
    return Object.keys(obj).filter(k => !Number.isNaN(k)) as K[]
}
_invitationStatusTypeKeys():number[]{
  let arr =[];
  for (const tl of this.enumKeys(InvitationStatus)) {
    
    const value = InvitationStatus[tl]

    if (typeof value === "string") {
                   // console.log(`Value: ${InvitationStatus[tl]}`)
                    arr.push(tl);
    }
}
return arr;
}
   ngAfterViewInit(){
    console.log(this.data.id);
     if(this.data.id){
       this.invitationsClient.getOne(this.data.id).subscribe((res)=>{
         this.basicForm = this.formBuilder.group({
          fullName: [res.data.fullName, [Validators.required, Validators.minLength(3)]],
          title1: [res.data.titleFirstId, [Validators.required]],
          title2: [res.data.titleSecondId],
          personType: [res.data.personTypeId, [Validators.required]],
          phone : [res.data.phone],
          email : [res.data.email],
          party : [res.data.party],
          position : [res.data.position],
          sendWhatsapp :  [res.data.sendWhatsapp === true ? "1": "0",Validators.required],
          sendEmail:  [res.data.sendEmail === true ? "1": "0",Validators.required],
          confirmAttendance:  [res.data.confirmAttendance  === true ? "1": "0",Validators.required],
          invitationStatus : [res.data.invitationStatus.toString(),Validators.required],
           Language: [res.data.language.toString() ,Validators.required],
           EventId : [res.data.eventId,Validators.required]
         });
       });
     }
  
   }
   get errorControl() {
     return this.basicForm.controls;
   }

   submitForm(){
     if(this.basicForm.valid){
       console.log(this.basicForm.value['fullName']);
       console.log(this.basicForm.value['title1']);
       console.log(this.basicForm.value['title2']);
       console.log(this.basicForm.value['phone']);
       console.log(this.basicForm.value['email']);
       console.log(this.basicForm.value['party']);
       console.log(this.basicForm.value['position']);
       console.log(this.basicForm.value['sendWhatsapp']);
       console.log(this.basicForm.value['sendEmail']);
       console.log(this.basicForm.value['cameToEvent']);
       console.log(this.basicForm.value['invitationStatus']);
       console.log(this.basicForm.value['Language']);
       console.log(this.basicForm.value['EventId']);
    
       if(!this.data.id)
       {
        let model = new InvitationCreateDto();
        model.init({
          fullName : this.basicForm.value['fullName'], 
          titleFirstId : +this.basicForm.value['title1'],
          titleSecondId: +this.basicForm.value['title2'],
          personTypeId : +this.basicForm.value['personType'],
          phone :this.basicForm.value['phone'],
          email:this.basicForm.value['email'],
          party:this.basicForm.value['party'],
          position :this.basicForm.value['position'],
          sendWhatsapp :this.basicForm.value['sendWhatsapp'] === "0"? false : true,
          sendEmail:this.basicForm.value['sendEmail']  === "0"? false : true,
          hasCameToEvent: this.basicForm.value['cameToEvent']  === "0"? false : true,
          invitationStatus:+this.basicForm.value['invitationStatus'],
          language : +this.basicForm.value['Language'],
          eventId : this.basicForm.value['EventId'],
          formType :  FormType.Internal
     });
         this.store.createInvitation(model).subscribe((res : BooleanResponse)=>{
           this.dialogRef.close({event:res.succeeded});
          });
       }
       else{
        let model = new UpdateInvitationDto();
        model.init({
          id : this.data.id,
          fullName : this.basicForm.value['fullName'], 
          titleFirstId : +this.basicForm.value['title1'],
          titleSecondId: +this.basicForm.value['title2'],
          personTypeId : +this.basicForm.value['personType'],
          phone :this.basicForm.value['phone'],
          email:this.basicForm.value['email'],
          party:this.basicForm.value['party'],
          position :this.basicForm.value['position'],
          sendWhatsapp :this.basicForm.value['sendWhatsapp'] === "0"? false : true,
          sendEmail:this.basicForm.value['sendEmail']  === "0"? false : true,
          hasCameToEvent: this.basicForm.value['cameToEvent']  === "0"? false : true,
          invitationStatus:+this.basicForm.value['invitationStatus'],
          language : +this.basicForm.value['Language'],
          eventId : this.basicForm.value['EventId'],
          formType :  FormType.Internal
     });
          this.store.editInvitation(model).subscribe((res : BooleanResponse)=>{
            this.dialogRef.close({event:res.succeeded});
           });
       }
    
     }
 
   }
   close(){
     this.dialogRef.close();
   }
}
