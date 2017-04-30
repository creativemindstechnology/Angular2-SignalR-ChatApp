import { Component, OnInit } from '@angular/core';

import { User } from './chat/chat-service';
import { NotificationsService } from 'angular2-notifications';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {

  userData: User;

  options = {
    position: ["top", "right"],
    timeOut: 5000,
    lastOnBottom: false,
    showProgressBar: false,
    pauseOnHover: true,
    clickToClose: true,
    maxStack: 5
  }

  constructor() {
    this.userData = new User();
  }

  ngOnInit() {

    let user: User = JSON.parse(localStorage.getItem('userData'));

    if (user) {
      this.userData.Name = user.Name;
      this.userData.Email = user.Email;      
    }

  }

  private showChatComponent() {

    if (this.userData.Name && this.userData.Email)
      return true;

    return false;

  }

}
