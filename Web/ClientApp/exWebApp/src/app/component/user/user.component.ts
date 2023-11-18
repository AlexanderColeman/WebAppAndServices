import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit { // Implement OnInit

  drivers: any[] = []; // Declare the drivers array

  constructor(private userService: UserService, private http: HttpClient) { }

  ngOnInit(): void {
    this.userService.getData().subscribe((data: any[]) => {
      this.drivers = data; // Assign the result to the drivers property
    });
  }
}
