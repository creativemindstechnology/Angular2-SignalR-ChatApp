import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { SimpleNotificationsModule } from 'angular2-notifications';

import { AppComponent } from './app.component';
import { RegistrationComponent } from './registration/registration.component';
import { ChatComponent } from './chat/chat-component';

import { ChatService } from './chat/chat-service'

import "rxjs/add/operator/map";

@NgModule({
  declarations: [
    AppComponent,
    RegistrationComponent,
        
    ChatComponent,
    
  ],
  imports: [
    BrowserModule,
    SimpleNotificationsModule.forRoot(),
    FormsModule,
    ReactiveFormsModule,
    HttpModule,

    
  ],
  providers: [ChatService],
  bootstrap: [AppComponent]
})
export class AppModule { }
