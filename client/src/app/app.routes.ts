import { Routes } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { NotFoundComponent } from './shared/not-found/not-found.component';

export const rootRouterConfig: Routes = [
    { path: '', component: HomeComponent },
    { path: 'home', component: HomeComponent },
    { path: 'not-found', component: NotFoundComponent },
    { path: 'people', loadChildren: 'app/people/people.module#PeopleModule' }
];