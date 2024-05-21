import { Component, EventEmitter, Output } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-confirm-alert',
  templateUrl: './confirm-alert.component.html',
  styleUrls: ['./confirm-alert.component.scss']
})

export class ConfirmAlertComponent {
  @Output() updated: EventEmitter<any> = new EventEmitter<any>();
constructor(private dialogRef: MatDialogRef<ConfirmAlertComponent>){}
Ok(){
  this.dialogRef.close({event:true});
  }
  close(){
    this.dialogRef.close();
  }
}
