import { Component } from '@angular/core';
import { StudentService } from '../student.service';

@Component({
  selector: 'app-student',
  templateUrl: './student.component.html',
  styleUrl: './student.component.scss'
})
export class StudentComponent {
  constructor( private studentService: StudentService){}
  studentDetels: any[] =[];

  getallStudentData (){
  
  }
}
