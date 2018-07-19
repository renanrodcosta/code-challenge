import { Component, OnInit, ViewContainerRef } from '@angular/core';

import { ToastsManager } from 'ng2-toastr';

import { Person } from "../../models/person";
import { PeopleService } from "../../services/people.service";

@Component({
  selector: 'app-list-peple',
  templateUrl: './list-peple.component.html'
})
export class ListPeopleComponent implements OnInit {
  public people: Person[];
  errorMessage: string;

  constructor(public service: PeopleService, public toastr: ToastsManager, vcr: ViewContainerRef) {
    this.toastr.setRootViewContainerRef(vcr);
  }

  ngOnInit() : void {
    this.service
      .getAll()
      .subscribe(people => this.people = people,
      error => this.errorMessage = error);
  }
}
