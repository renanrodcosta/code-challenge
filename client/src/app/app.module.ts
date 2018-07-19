import { BrowserModule, Title } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { rootRouterConfig } from './app.routes';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { registerLocaleData } from '@angular/common';
import localePt from '@angular/common/locales/pt';
registerLocaleData(localePt);

// 3s components
import { ToastModule, ToastOptions } from 'ng2-toastr/ng2-toastr';
import { CustomFormsModule } from 'ng2-validation'

// bootstrap
import { AlertModule } from 'ngx-bootstrap';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { CarouselModule } from 'ngx-bootstrap/carousel';

// shared
import { NotFoundComponent } from './shared/not-found/not-found.component';

// components
import { HomeComponent } from './home/home.component';
import { AppComponent } from './app.component';

// services
import { ErrorInterceptor } from './services/error.handler.service';

// modules
import { SharedModule } from "./shared/shared.module";

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NotFoundComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule,
    SharedModule,
    ReactiveFormsModule,
    ToastModule.forRoot(),
    AlertModule.forRoot(),
    CollapseModule.forRoot(),
    CarouselModule.forRoot(),
    RouterModule.forRoot(rootRouterConfig, { useHash: false })
  ],
  providers: [
    Title,
    {
        provide: HTTP_INTERCEPTORS,
        useClass: ErrorInterceptor,
        multi: true
      }    
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

