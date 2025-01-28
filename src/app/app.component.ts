import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'CRUDApp';
  // showList = true;

  // constructor(private router: Router) { }

  // navigateToEmpList() {
  //   this.router.navigate(["/add-employee"]);
  //   this.showList=false;
  // }
}
