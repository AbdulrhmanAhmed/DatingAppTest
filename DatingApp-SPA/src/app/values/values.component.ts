import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-values',
  templateUrl: './values.component.html',
  styleUrls: ['./values.component.css']
})
export class ValuesComponent implements OnInit {
  constructor(private http: HttpClient) {}
  values: any;

   profileform = new FormGroup({
           
    username : new FormControl( ) ,
    password : new FormControl( ) ,
     Addressform : new FormGroup({
      city : new FormControl( ) ,
      street : new FormControl( ) ,
      state : new FormControl( ) ,
     })
    });

  ngOnInit() {
    this.getValues();
  }
  getValues() {
    this.http.get('http://localhost:5000/api/Values').subscribe(
      c => {
        this.values = c;
      },
      error => {
        console.log(error);
      }
    );
  }
  onSubmit() {

console.log(this.profileform);
  }
}
