import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {map} from 'rxjs/operators';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl = 'http://localhost:5000/api/Auth/';
constructor(private http: HttpClient) { }

login(model: any): Observable <any> {
return this.http.post(this.baseUrl + 'Login' , model).pipe(
  map((Response: any) => {
   const user = Response;
   if (user) {
   localStorage.setItem('token' , user.token);
   }
  }
));

}
}
