import { Routes } from '@angular/router';

import { PeopleComponent } from "./people.component";

import { ListPeopleComponent } from './components/list-people/list-people.component';
import { CreatePersonComponent } from "./components/create-person/create-person.component";
import { RemovePersonComponent } from "./components/remove-person/remove-person.component";

export const peopleRouterConfig: Routes = [
  {
    path: '', component: PeopleComponent,
    children: [
      { path: '', component: ListPeopleComponent },
      { path: 'create', component: CreatePersonComponent },
      { path: 'remove/:id', component: RemovePersonComponent }
    ]
  }
];