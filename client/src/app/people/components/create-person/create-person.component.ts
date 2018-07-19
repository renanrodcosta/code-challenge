import { Component, OnInit, AfterViewInit, ViewChildren, ElementRef, ViewContainerRef } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, FormGroup, FormControl, FormArray, Validators, FormControlName } from '@angular/forms';
import { Router } from "@angular/router";

import { Observable } from 'rxjs/Observable';
import { Subscription } from 'rxjs/Subscription';
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/observable/fromEvent';
import 'rxjs/add/observable/merge';

import { ToastsManager, Toast } from 'ng2-toastr/ng2-toastr';

import { Person } from "../../models/person";
import { PeopleService } from "../../services/people.service";

import { DateUtils } from "../../../common/data-type-utils/date-utils";
import { GenericValidator } from "../../../common/validation/generic-form-validator";

@Component({
  selector: 'app-create-person',
  templateUrl: './create-person.component.html'
})

export class CreatePersonComponent implements OnInit, AfterViewInit {
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];
  public myDatePickerOptions = DateUtils.getMyDatePickerOptions();

  public errors: any[] = [];
  public peopleForm: FormGroup;
  public person: Person;

  constructor(private fb: FormBuilder,
    private peopleService: PeopleService,
    private router: Router,
    private toastr: ToastsManager,
    vcr: ViewContainerRef) {

    this.toastr.setRootViewContainerRef(vcr);

    this.validationMessages = {
      name: {
        required: 'The Name is required.',
        minlength: 'Min 2 caracters of the Name.',
        maxlength: 'Max 150 caracters of the Name.'
      },
      age: {
        required: 'The Age is required.'
      }
    };

    this.genericValidator = new GenericValidator(this.validationMessages);
    this.person = new Person();
  }

  displayMessage: { [key: string]: string } = {};
  private validationMessages: { [key: string]: { [key: string]: string } };
  private genericValidator: GenericValidator;

  ngOnInit(): void {
    this.peopleForm = this.fb.group({
      name: ['', [Validators.required,
      Validators.minLength(2),
      Validators.maxLength(150)]],
      age: ['', Validators.required]
    });
  }

  ngAfterViewInit(): void {
    let controlBlurs: Observable<any>[] = this.formInputElements
      .map((formControl: ElementRef) => Observable.fromEvent(formControl.nativeElement, 'blur'));

    Observable.merge(...controlBlurs).subscribe(value => {
      this.displayMessage = this.genericValidator.processMessages(this.peopleForm);
    });
  }

  save() {
    if (this.peopleForm.dirty && this.peopleForm.valid) {

      let p = Object.assign({}, this.person, this.peopleForm.value);

      this.peopleService
        .create(this.person)
        .subscribe(this.onSaveComplete, this.onError);
    }
  }

  onError(fail) {
    this.toastr.error('Process fail!', 'Ops! :(');
    this.errors = fail.error.errors;
  }

  onSaveComplete(person) {

    this.peopleForm.reset();
    this.errors = [];

    this.toastr.success('Person has been created!', 'Woohoo :D', { dismiss: 'controlled' })
      .then((toast: Toast) => {
        setTimeout(() => {
          this.toastr.dismissToast(toast);
          this.router.navigate(['/edit/' + person.id]);
        }, 3500);
      });
  }
}
