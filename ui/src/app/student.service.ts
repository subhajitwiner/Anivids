import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StudentService {
  data: Subject<any> = new Subject();
  messessData = this.data.asObservable();
  url: string ="url"

  constructor() { 
  
  }
  insertDataToDataLIst(inputData: any){
    this.data.next(inputData)
  }
}
