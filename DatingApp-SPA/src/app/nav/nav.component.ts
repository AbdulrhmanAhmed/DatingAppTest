import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/Auth.service';


@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
 model: any = {};
  constructor(private authService: AuthService) { }

  ngOnInit() {
  }
login() {
 this.authService.login(this.model).subscribe(next => {
console.log('Loged Success');
}
, error => {
  console.log('Logedd failed');
});
}
}
