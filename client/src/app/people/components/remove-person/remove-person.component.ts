import { Component, OnInit, ViewContainerRef, ViewChildren, ElementRef } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import { FormControlName } from "@angular/forms";

import { Subscription } from "rxjs/Subscription";
import { ToastsManager, Toast } from 'ng2-toastr/ng2-toastr';

import { Person } from "../../models/person";
import { PeopleService } from "../../services/people.service";

@Component({
  selector: 'app-remove-person',
  templateUrl: './remove-person.component.html'
})
export class RemovePersonComponent implements OnInit {
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  public sub: Subscription;
  public personId: string = "";
  public person: Person;

  constructor(private peopleService: PeopleService,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastsManager,
    vcr: ViewContainerRef) {

    this.toastr.setRootViewContainerRef(vcr);
    this.person = new Person();
  }

  ngOnInit() {
    this.sub = this.route.params.subscribe(
      params => {
        this.personId = params['id'];
      });

    this.peopleService
      .get(this.personId)
      .subscribe(person => { this.person = person; }, this.onError);
  }

  public remove() {
    this.peopleService
      .remove(this.personId)
      .subscribe(this.onDeleteComplete, this.onError);
  }

  public onDeleteComplete() {
    this.toastr.success('Person has been removed!', 'Good bye :D', { dismiss: 'controlled' })
      .then((toast: Toast) => {
        setTimeout(() => {
          this.toastr.dismissToast(toast);
          this.router.navigate(['/people']);
        }, 2500);
      });
  }

  public onError() {
    this.toastr.error('Process fail!', 'Ops! :(');
  }
}
