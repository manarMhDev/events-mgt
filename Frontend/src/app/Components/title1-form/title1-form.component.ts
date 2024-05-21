import { Component, EventEmitter, Output , Inject} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CreateFirstTitleDto, FirstTitleClient, UpdateTitleDto } from 'src/shared/api/service-proxies';

@Component({
  selector: 'app-title1-form',
  templateUrl: './title1-form.component.html',
  styleUrls: ['./title1-form.component.scss']
})
export class Title1FormComponent {
  basicForm: FormGroup;
   obj : UpdateTitleDto;
  @Output() updated: EventEmitter<any> = new EventEmitter<any>();
    constructor(
      private dialogRef: MatDialogRef<Title1FormComponent>,
      @Inject(MAT_DIALOG_DATA) public data :any,
      public formBuilder: FormBuilder,
      private firstTitleService : FirstTitleClient,
      ){ }
    ngOnInit(){
      // this.objId= this.data !==null ? this.data.id : null;
      this.basicForm = this.formBuilder.group({
        title: ['', [Validators.required, Validators.minLength(3)]],
        Language: ["1",Validators.required],

      });
    }
    ngAfterViewInit(){
      if(this.data.id){
        this.firstTitleService.getOne(this.data.id).subscribe((res)=>{
          this.obj = res.data;
          this.basicForm = this.formBuilder.group({
            title: [res.data.name, [Validators.required, Validators.minLength(3)]]
    
          });
        });
      }
   
    }
    get errorControl() {
      return this.basicForm.controls;
    }
    submitForm(){
      if(this.basicForm.valid){
        console.log(this.basicForm.value['title']);
        console.log(this.basicForm.value['Language']);
    
       
        if(!this.data.id)
        {
          let model = new CreateFirstTitleDto();
           model.init({
          name : this.basicForm.value['title'], 
          language : +this.basicForm.value['Language']
        });
          this.firstTitleService.createOne(model).subscribe((res)=>{
            this.dialogRef.close({event:res.succeeded});
           });
        }
        else{
          console.log(this.obj)
          this.obj.name = this.basicForm.value['title'];
          this.firstTitleService.editOne(this.obj).subscribe((res)=>{
            this.dialogRef.close({event:res.succeeded});
           });
        }
     
      }
  
    }
    close(){
      this.dialogRef.close();
    }
}
