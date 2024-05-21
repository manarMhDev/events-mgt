import { Component, EventEmitter, Output , Inject, ViewChild} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { StoreEventPlacesService } from 'src/app/Services/store-event-places.service';
import { BooleanResponse, EventPlaceClient, SeatsTypesClient } from 'src/shared/api/service-proxies';

@Component({
  selector: 'app-event-place-form',
  templateUrl: './event-place-form.component.html',
  styleUrls: ['./event-place-form.component.scss']
})
export class EventPlaceFormComponent {
    basicForm: FormGroup;
    objId ;
    formData = new FormData();
    @ViewChild('fileInput') fileInput;
   @Output() updated: EventEmitter<any> = new EventEmitter<any>();
     constructor(
       private dialogRef: MatDialogRef<EventPlaceFormComponent>,
       @Inject(MAT_DIALOG_DATA) public data :any,
       public formBuilder: FormBuilder,
       private eventPlaceClient : EventPlaceClient,
       private store : StoreEventPlacesService
       ){ }
     ngOnInit(){
        this.objId= this.data !==null ? this.data.id : null;
       this.basicForm = this.formBuilder.group({
        Name: ['', [Validators.required, Validators.minLength(3)]],
         SeatingChart : ["1",Validators.required],
         Columns : [0,Validators.required],
         Rows : [0,Validators.required]
       });
     }
   
     ngAfterViewInit(){
      console.log(this.data.id);
       if(this.data.id){
         this.eventPlaceClient.getOne(this.data.id).subscribe((res)=>{
           this.basicForm = this.formBuilder.group({
            Name: [res.data.name, [Validators.required, Validators.minLength(3)]],
             SeatingChart: [res.data.seatingChart.toString(),Validators.required],
             Columns: [res.data.columns,Validators.required],
             Rows: [res.data.rows,Validators.required],
           });
         });
       }
    
     }
     get errorControl() {
       return this.basicForm.controls;
     }
     uploadFile() {
      let file = this.fileInput.nativeElement.files[0];
      this.formData.append('seatingChartImagePath', file, file.name);
  }
     submitForm(){
       if(this.basicForm.valid){
         console.log(this.basicForm.value['Name']);
         console.log(this.basicForm.value['SeatingChart']);
         console.log(this.basicForm.value['Columns']);
         console.log(this.basicForm.value['Rows']);

         if(!this.data.id)
         {
          this.formData.append('Name',this.basicForm.value['Name']);
          this.formData.append('SeatingChart',this.basicForm.value['SeatingChart']);
          this.formData.append('Columns',this.basicForm.value['Columns']);
          this.formData.append('Rows',this.basicForm.value['Rows']);
  
           this.store.createEventPlace(this.formData).subscribe((res : BooleanResponse)=>{
             this.dialogRef.close({event:res.succeeded});
            });
         }
         else{
           this.formData.append('id',this.objId.toString());
           this.formData.append('Name',this.basicForm.value['Name']);
           this.formData.append('SeatingChart',this.basicForm.value['SeatingChart']);
           this.formData.append('Columns',this.basicForm.value['Columns']);
           this.formData.append('Rows',this.basicForm.value['Rows']);
         
            this.store.editEventPlace(this.formData).subscribe((res : BooleanResponse)=>{
              this.dialogRef.close({event:res.succeeded});
             });
         }
      
       }
   
     }
     close(){
       this.dialogRef.close();
     }
  }
  