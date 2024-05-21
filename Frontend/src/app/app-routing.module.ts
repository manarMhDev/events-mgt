import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { authGuard } from 'src/shared/auth/auth.guard';
import { Titles1Component } from './pages/titles1/titles1.component';
import { Titles2Component } from './pages/titles2/titles2.component';
import { SeatsTypesComponent } from './pages/seats-types/seats-types.component';
import { PersonTypesComponent } from './pages/person-types/person-types.component';
import { EventPlacesComponent } from './pages/event-places/event-places.component';
import { EventsComponent } from './pages/events/events.component';
import { InvitationsComponent } from './pages/invitations/invitations.component';
import { SeatsComponent } from './pages/seats/seats.component';

const routes: Routes = [
  { path:'', component: HomeComponent ,  canActivate: [authGuard] },
  { path:'login', component: LoginComponent },
  { path:'home', component: HomeComponent ,  canActivate: [authGuard] },
  { path:'titles1', component: Titles1Component ,  canActivate: [authGuard] },
  { path:'titles2', component: Titles2Component ,  canActivate: [authGuard] },
  { path:'seats-types', component: SeatsTypesComponent ,  canActivate: [authGuard] },
  { path:'person-types', component: PersonTypesComponent ,  canActivate: [authGuard] },
  { path:'event-places', component: EventPlacesComponent ,  canActivate: [authGuard] },
  { path:'events', component: EventsComponent ,  canActivate: [authGuard] },
  { path:'invitations', component: InvitationsComponent ,  canActivate: [authGuard] },
  { path:'seats', component: SeatsComponent ,  canActivate: [authGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
