import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

// bootstrap
import { CollapseModule } from 'ngx-bootstrap/collapse';

// components
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';

@NgModule({
    imports: [
        CommonModule, 
        RouterModule,
        CollapseModule 
        ],
    declarations: [
        HeaderComponent,
        FooterComponent
        ],
    exports: [
        HeaderComponent,
        FooterComponent
        ]
})
export class SharedModule { }