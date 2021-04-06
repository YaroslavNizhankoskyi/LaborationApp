import { AuthService } from './data/services/auth.service';
import { Component, OnInit } from '@angular/core';
import { User } from './data/types/auth/User';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'Laboration App';


  constructor(private authService: AuthService) {
  }

  ngOnInit(){
    this.setCurrentUser();
  }

  setCurrentUser() {
    const user: User = JSON.parse(localStorage.getItem('user'));
    if (user) {
      this.authService.setCurrentUser(user);
    }
  }
}
