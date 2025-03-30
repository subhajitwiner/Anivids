import { Component, Inject, PLATFORM_ID } from '@angular/core';
import { TestService } from './test.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  messages: string[] = [];
  message = '';
  constructor(@Inject(PLATFORM_ID) private readonly platformId: Object,private readonly chatService: TestService) {}
  ngOnInit(): void {
    this.chatService.receiveMessages()?.subscribe(res =>{
      this.message= res
    })
    
  }

  sendMessage() {
    this.chatService.sendMessage(this.message);
    this.message = '';
  }
}

