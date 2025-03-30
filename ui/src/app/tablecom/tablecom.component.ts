import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { StudentService } from '../student.service';

@Component({
  selector: 'app-tablecom',
  templateUrl: './tablecom.component.html',
  styleUrl: './tablecom.component.scss'
})
export class TablecomComponent implements OnInit {
  constructor(private studentService: StudentService, private cdr: ChangeDetectorRef){}
  ngOnInit(): void {
      this.studentService.messessData.subscribe({
        next: res =>{
          
          this.datalist.push(res)
          this.cdr.detectChanges()
        }
      })

  }
  datalist: any[]=[];
  getFormData(event:any){
    this.datalist.push(event)
  }
}
