import { Component, Output } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { EventEmitter } from 'stream';
import {StudentService} from '../student.service'

@Component({
  selector: 'app-form-com',
  templateUrl: './form-com.component.html',
  styleUrl: './form-com.component.scss'
})
export class FormComComponent {
  form!: FormGroup;
  constructor(private fb:FormBuilder, private studentService:StudentService){
    this.form = this.fb.group({
      name: [],
      age: []
    })
  }
  inseartForm(){
    this.studentService.insertDataToDataLIst(this.form.value)

  }

}
