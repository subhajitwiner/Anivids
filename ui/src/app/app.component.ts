import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { UserService } from './services/api/user.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit{
  title = 'ui';
  constructor(private userService:UserService ){

  }
  data: any;
  ngOnInit(): void {
    this.userService.getWeather().subscribe(
      res =>{
        this.data = res;
        console.log(res);
      }
    );
  }
}
