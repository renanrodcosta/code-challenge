import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

import { Observable } from "rxjs/Observable";

import { BaseService } from "../../services/base.service";
import { Person } from "../models/person";

@Injectable()
export class PeopleService extends BaseService {

  constructor(private http: HttpClient) { super(); }

  getAll(): Observable<Person[]> {
    return this.http
      .get<Person[]>(this.UrlServiceV1 + "people")
      .catch(super.serviceError);
  };

  get(id: string): Observable<Person> {
    return this.http
      .get<Person>(this.UrlServiceV1 + "people/" + id)
      .catch(super.serviceError);
  };

  create(person: Person): Observable<Person> {
    let response = this.http
      .post(this.UrlServiceV1 + "people", person)
      .map(super.extractData)
      .catch(super.serviceError);
    return response;
  };

  remove(id: string): Observable<Person> {
    let response = this.http
      .delete(this.UrlServiceV1 + "people/" + id)
      .map(super.extractData)
      .catch((super.serviceError));
    return response;
  };
}


