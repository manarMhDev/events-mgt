import { Component, EventEmitter, Output , Inject, ViewChild} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { StoreSeatsTypesService } from 'src/app/Services/store-seats-types.service';
import { BooleanResponse, SeatsTypesClient } from 'src/shared/api/service-proxies';
@Component({
  selector: 'app-seat-type',
  templateUrl: './seat-type.component.html',
  styleUrls: ['./seat-type.component.scss']
})
export class SeatTypeComponent {
color="#000000";
colorText = "#000000";
  basicForm: FormGroup;
  objId ;
  formData = new FormData();
  @ViewChild('fileInput') fileInput;
 @Output() updated: EventEmitter<any> = new EventEmitter<any>();
   constructor(
     private dialogRef: MatDialogRef<SeatTypeComponent>,
     @Inject(MAT_DIALOG_DATA) public data :any,
     public formBuilder: FormBuilder,
     private seatsTypesClient : SeatsTypesClient,
     private store : StoreSeatsTypesService
     ){ }
   ngOnInit(){
      this.objId= this.data !==null ? this.data.id : null;
     this.basicForm = this.formBuilder.group({
      Name: ['', [Validators.required, Validators.minLength(3)]],
       Color : [this.color,Validators.required],
       ColorText : [this.colorText,Validators.required]
     });
   }
   setColor(value){
    this.color = value;
   }
   setColorText(value){
    this.colorText = value;
   }
   ngAfterViewInit(){
    console.log(this.data.id);
     if(this.data.id){
       this.seatsTypesClient.getOne(this.data.id).subscribe((res)=>{
         this.basicForm = this.formBuilder.group({
          Name: [res.data.name, [Validators.required, Validators.minLength(3)]]
         });
         this.color = res.data.color;
         this.colorText = res.data.colorText;
       });
     }
  
   }
   get errorControl() {
     return this.basicForm.controls;
   }
   uploadFile() {
    let file = this.fileInput.nativeElement.files[0];
    this.formData.append('SeatImage', file, file.name);
}
   submitForm(){
     if(this.basicForm.valid){
       console.log(this.basicForm.value['Name']);
   
      
       if(!this.data.id)
       {
        this.formData.append('Name',this.basicForm.value['Name']);
        this.formData.append('Color',this.color);
        this.formData.append('ColorText',this.colorText);

         this.store.createSeatType(this.formData).subscribe((res : BooleanResponse)=>{
           this.dialogRef.close({event:res.succeeded});
          });
       }
       else{
         this.formData.append('id',this.objId.toString());
         this.formData.append('Name',this.basicForm.value['Name']);
         this.formData.append('Color',this.color);
         this.formData.append('ColorText',this.colorText);
       
          this.store.editSeatType(this.formData).subscribe((res : BooleanResponse)=>{
            this.dialogRef.close({event:res.succeeded});
           });
       }
    
     }
 
   }
   close(){
     this.dialogRef.close();
   }
}
