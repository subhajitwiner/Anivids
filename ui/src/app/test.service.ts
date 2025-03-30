import { isPlatformBrowser } from '@angular/common';
import { Inject, Injectable, PLATFORM_ID } from '@angular/core';
import { Observable } from 'rxjs';
import { WebSocketSubject } from 'rxjs/webSocket';

@Injectable({
  providedIn: 'root'
})
export class TestService {
  private socket$: WebSocketSubject<string> | null = null;
  constructor(@Inject(PLATFORM_ID) private readonly platformId: Object) {
    // Only create WebSocket if running in the browser
    if (isPlatformBrowser(this.platformId)) {
      this.connect();
    } else {
      console.log('Skipping WebSocket initialization on the server.');
    }
  }
  private connect(): void {
    this.socket$ = new WebSocketSubject('ws://localhost:3001/chat');
  }
  sendMessage(message: string): void {
    if (this.socket$) {
      this.socket$.next(message);
    } else {
      console.error('WebSocket connection is not established.');
    }
  }
  receiveMessages(): Observable<string> | null {
    return this.socket$?.asObservable() || null;
  }
}
