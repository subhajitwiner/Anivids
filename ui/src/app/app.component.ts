import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TestService } from './services/external/test.service';
import { BrandComponent } from './brand/brand.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, BrandComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit{
  data: any;
  constructor(private testService: TestService){}
  ngOnInit(): void {
    this.getWeather();
  }
  title = 'ui';
  getWeather(){
    this.testService.getWeather().subscribe(
      res =>{
        this.data = res;
        console.log(res)
      }
    )
  };
}
