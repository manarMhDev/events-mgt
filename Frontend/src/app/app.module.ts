import { NgModule ,APP_INITIALIZER} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppHttpInterceptor } from 'src/shared/auth/app-http.interceptor';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './pages/home/home.component';
import { ComponentsModule } from './Components/components.module';
import { LoginComponent } from './pages/login/login.component';
import { AppAuthService } from 'src/shared/auth/app-auth.service';
import { AppSessionService } from 'src/shared/session/app-session.service';
import { AppInitializer } from './app-initializer';
import { API_BASE_URL } from 'src/shared/api/service-proxies';
import { AppConsts } from 'src/shared/AppConsts';
import { ServiceProxyModule } from '../shared/api/service-proxies.module';
import { FormsModule , ReactiveFormsModule  } from '@angular/forms';
import { Titles1Component } from './pages/titles1/titles1.component';
import { Titles2Component } from './pages/titles2/titles2.component';
import { RouterModule } from '@angular/router';
import {MatDialog, MatDialogModule,  MAT_DIALOG_DATA,} from '@angular/material/dialog';
import {MatGridListModule} from '@angular/material/grid-list';
import { SeatsTypesComponent } from './pages/seats-types/seats-types.component';
import { PersonTypesComponent } from './pages/person-types/person-types.component';
import { EventPlacesComponent } from './pages/event-places/event-places.component';
import { EventsComponent } from './pages/events/events.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import {MatSelectModule} from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';

import { MatTableModule } from '@angular/material/table';
import { MatListModule } from '@angular/material/list';
import { MatButtonModule } from '@angular/material/button';
import {MatSortModule} from '@angular/material/sort';
import { InvitationsComponent } from './pages/invitations/invitations.component';
import { SeatsComponent } from './pages/seats/seats.component';
@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    Titles1Component,
    Titles2Component,
    SeatsTypesComponent,
    PersonTypesComponent,
    EventPlacesComponent,
    EventsComponent,
    InvitationsComponent,
    SeatsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterModule,
    ComponentsModule,
    HttpClientModule,
    ServiceProxyModule,
    FormsModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    BrowserAnimationsModule,
    MatTableModule,
    MatListModule,
    MatButtonModule,
    MatSortModule,
    MatGridListModule
  ],
  providers: [ 
    {
      provide: APP_INITIALIZER,
      useFactory: (appInitializer: AppInitializer) => appInitializer.init(),
      deps: [AppInitializer],
      multi: true,
    },
    { provide: API_BASE_URL, useFactory: () => AppConsts.remoteServiceBaseUrl },
    { provide: HTTP_INTERCEPTORS, useClass: AppHttpInterceptor, multi: true },
    AppAuthService,
    AppSessionService,
    MatDialogModule,
    ComponentsModule
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
