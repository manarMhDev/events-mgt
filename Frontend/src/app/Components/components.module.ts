import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { Title1FormComponent } from './title1-form/title1-form.component';
import { Title2FormComponent } from './title2-form/title2-form.component';
import { ReactiveFormsModule , FormsModule } from '@angular/forms';
import { ConfirmAlertComponent } from './confirm-alert/confirm-alert.component';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { SeatTypeComponent } from './seat-type/seat-type.component';
import { ColorPickerModule } from 'ngx-color-picker';
import { PaginationComponent } from './pagination/pagination.component';
import { PersonTypeFormComponent } from './person-type-form/person-type-form.component';
import { EventPlaceFormComponent } from './event-place-form/event-place-form.component';
import { EventFormComponent } from './event-form/event-form.component';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import {ErrorStateMatcher} from '@angular/material/core';
import {MatSelectModule} from '@angular/material/select';
import { InvitationFormComponent } from './invitation-form/invitation-form.component';
import {MatGridListModule} from '@angular/material/grid-list';
import {MatRadioModule} from '@angular/material/radio';
import { SeatFormComponent } from './seat-form/seat-form.component';
import { BookSeatFormComponent } from './book-seat-form/book-seat-form.component';

@NgModule({
    imports: [ReactiveFormsModule , FormsModule,BrowserModule,CommonModule,RouterModule,
      MatDatepickerModule,
    MatNativeDateModule,
    MatFormFieldModule,
    MatInputModule,
    MatCardModule,
    MatSelectModule,
      ColorPickerModule,
      MatGridListModule,
      MatRadioModule],
    declarations: [ 
      HeaderComponent,
       FooterComponent,
        Title1FormComponent, 
        ConfirmAlertComponent,
        Title2FormComponent, 
        SeatTypeComponent, 
        PaginationComponent, 
        PersonTypeFormComponent, EventPlaceFormComponent, EventFormComponent, InvitationFormComponent, SeatFormComponent, BookSeatFormComponent],
    exports: [
      HeaderComponent,FooterComponent,PaginationComponent,SeatFormComponent],
  })
  export class ComponentsModule {}
