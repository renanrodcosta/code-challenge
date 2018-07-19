import { Injectable } from "@angular/core";
import { HttpInterceptor, HttpHandler, HttpEvent, HttpRequest, HttpErrorResponse } from "@angular/common/http";
import { Router } from "@angular/router";

import { Observable } from "rxjs/Observable";
import 'rxjs/add/operator/catch';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

    constructor(private router: Router){}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        
        return next.handle(req).catch(err => {
            
            if(err instanceof HttpErrorResponse) {
                if (err.status === 404) {
                    this.router.navigate(['/not-found']);
                }
            }

            return Observable.throw(err);
        });
    }    
}