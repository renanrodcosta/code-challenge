import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html'
})
export class HeaderComponent {
  private token: string;
  constructor() { }

  public isCollapsed: boolean = true;
}
