import { Component, EventEmitter, Output , Inject} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CreateFirstTitleDto, CreatePersonTypeDto, FirstTitleClient, PersonTypeClient, UpdatePersonTypeDto, UpdateTitleDto } from 'src/shared/api/service-proxies';

@Component({
  selector: 'app-person-type-form',
  templateUrl: './person-type-form.component.html',
  styleUrls: ['./person-type-form.component.scss']
})
export class PersonTypeFormComponent {
  color="#000000";
  basicForm: FormGroup;
  obj : UpdatePersonTypeDto;
 @Output() updated: EventEmitter<any> = new EventEmitter<any>();
   constructor(
     private dialogRef: MatDialogRef<PersonTypeFormComponent>,
     @Inject(MAT_DIALOG_DATA) public data :any,
     public formBuilder: FormBuilder,
     private personTypeService : PersonTypeClient,
     ){ }
   ngOnInit(){
     // this.objId= this.data !==null ? this.data.id : null;
     this.basicForm = this.formBuilder.group({
       title: ['', [Validators.required, Validators.minLength(3)]],
       Color : [this.color,Validators.required],
     });
   }
   ngAfterViewInit(){
     if(this.data.id){
       this.personTypeService.getOne(this.data.id).subscribe((res)=>{
         this.obj = res.data;
         this.basicForm = this.formBuilder.group({
           title: [res.data.name, [Validators.required, Validators.minLength(3)]]
   
         });
         this.color = res.data.color;
       });
     }
  
   }
   get errorControl() {
     return this.basicForm.controls;
   }
   setColor(value){
    this.color = value;
   }
   submitForm(){
     if(this.basicForm.valid){
       console.log(this.basicForm.value['title']);
   
      
       if(!this.data.id)
       {
         let model = new CreatePersonTypeDto();
          model.init({
         name : this.basicForm.value['title'], 
         color: this.color,
       });
         this.personTypeService.createOne(model).subscribe((res)=>{
           this.dialogRef.close({event:res.succeeded});
          });
       }
       else{
         console.log(this.obj)
         this.obj.name = this.basicForm.value['title'];
         this.obj.color = this.color;
         this.personTypeService.editOne(this.obj).subscribe((res)=>{
           this.dialogRef.close({event:res.succeeded});
          });
       }
    
     }
 
   }
   close(){
     this.dialogRef.close();
   }
}
