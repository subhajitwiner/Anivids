import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TestService {

  constructor(private http: HttpClient) { }
  getWeather(){
    return this.http.get('https://localhost:7107/WeatherForecast')
  }
}
