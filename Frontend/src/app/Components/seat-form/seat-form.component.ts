import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { StoreEventPlacesService } from 'src/app/Services/store-event-places.service';
import { StoreSeatsTypesService } from 'src/app/Services/store-seats-types.service';
import { AppPrefixesType, Prefixes } from 'src/shared/AppEnums';
import { CreateSeatsDto, EventPlaceDto, SeatsClient, SeatsTypeDto, UpdateASeatDto } from 'src/shared/api/service-proxies';

@Component({
  selector: 'app-seat-form',
  templateUrl: './seat-form.component.html',
  styleUrls: ['./seat-form.component.scss']
})
export class SeatFormComponent {
  basicForm: FormGroup;
  editForm: FormGroup;
  places : EventPlaceDto[];
  seatTypes : SeatsTypeDto[];
  @Input() id = null;
  @Output() updated: EventEmitter<any> = new EventEmitter<any>();
  _prefixesType = Prefixes;
  // _appPrefixesType = AppPrefixesType.getName;
  constructor(
    public formBuilder: FormBuilder,
    private storePlaces : StoreEventPlacesService,
     private storeSeatType : StoreSeatsTypesService,
     private seatsService : SeatsClient
    ){ }
  ngOnInit(){
    this.storePlaces.loadEventPlaces_pageN(1);
     this.basicForm = this.formBuilder.group({
       EventPlaceId : [null,Validators.required],
       SeatTypeId : [null,Validators.required],
       Prefix : ['',Validators.required],
       start : ['',Validators.required],
       count : ['',Validators.required]
     });
     this.editForm = this.formBuilder.group({
      EventPlaceId : [null,Validators.required],
      SeatTypeId : [null,Validators.required],
      Code : ['',Validators.required]
    });
     this.storePlaces.loadAllEventPlaces();
     this.storePlaces.AllEventPlaces$.subscribe((res)=>{
      this.places = res;
     });
     this.storeSeatType.loadAllSeatType();
     this.storeSeatType.SeatTypes$.subscribe((res)=>{
      this.seatTypes = res;
     });
   }

   submitForm(){
    if(this.basicForm.valid){
    console.log(this.basicForm.value['EventPlaceId']);
    console.log(this.basicForm.value['SeatTypeId']);
    console.log(this.basicForm.value['Prefix']);
    console.log(this.basicForm.value['start']);
    console.log(this.basicForm.value['count']);
    if(!this.id)
      {
        let model = new CreateSeatsDto();
         model.init({
          eventPlaceId : this.basicForm.value['EventPlaceId'], 
          seatTypeId : +this.basicForm.value['SeatTypeId'],
          prefix : +this.basicForm.value['Prefix'], 
          start : +this.basicForm.value['start'],
          count : +this.basicForm.value['count']
      });
        this.seatsService.createSeats(model).subscribe((res)=>{
          this.updated.emit();
         });
      }
      else{
      
      }
    }
   }
   submitEditForm(){
    if(this.editForm.valid){
      console.log(this.editForm.value['EventPlaceId']);
      console.log(this.editForm.value['SeatTypeId']);
      console.log(this.editForm.value['Code']);
      let model = new UpdateASeatDto();
      model.init({
        id : this.id,
       eventPlaceId : this.editForm.value['EventPlaceId'], 
       seatTypeId : +this.editForm.value['SeatTypeId'],
       code : this.editForm.value['Code']
   });
           this.seatsService.editOne(model).subscribe((res)=>{
            this.updated.emit();
     });
    }
   }
   enumKeys<O extends object, K extends keyof O = keyof O>(obj: O): K[] {
    return Object.keys(obj).filter(k => !Number.isNaN(k)) as K[]
}
_prefixesKeys():number[]{
  let arr =[];
  for (const tl of this.enumKeys(Prefixes)) {
    
    const value = Prefixes[tl]

    if (typeof value === "string") {
                   // console.log(`Value: ${InvitationStatus[tl]}`)
                    arr.push(tl);
    }
}
return arr;
}

}
