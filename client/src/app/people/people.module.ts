import { NgModule } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { MyDatePickerModule } from 'mydatepicker';

import { PeopleComponent } from "./people.component";
import { ListPeopleComponent } from './components/list-people/list-people.component';
import { RemovePersonComponent } from './components/remove-person/remove-person.component';

import { PeopleService } from "./services/people.service";
import { ErrorInterceptor } from '../services/error.handler.service';

import { peopleRouterConfig } from "./people.routes";

import { SharedModule } from "../shared/shared.module";

@NgModule({
  imports: [
    SharedModule,
    CommonModule,
    FormsModule,
    RouterModule.forChild(peopleRouterConfig),
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    MyDatePickerModule,
  ],
  declarations: [
    PeopleComponent,
    ListPeopleComponent,
    RemovePersonComponent
  ],
  providers: [
    Title,
    PeopleService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true
    }
  ],
  exports: [RouterModule]
})

export class PeopleModule { }