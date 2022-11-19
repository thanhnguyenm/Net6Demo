import { Component, OnInit } from '@angular/core';
import * as appRoute from './../mixins/routes';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  title = 'learning-css-angular14';
  routes = appRoute.default;
  
  constructor() { }

  ngOnInit(): void {
    console.log(this.routes);
  }

}
