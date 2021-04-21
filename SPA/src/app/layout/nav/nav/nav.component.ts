import { Router, RouterModule } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { FeedbackService } from 'src/app/data/services/feedback.service';
import { TipService } from 'src/app/data/services/tip.service';
import { AuthService } from 'src/app/data/services/auth.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

 
  unreadFeedbacks: number;
  unreadTips: number;
  isUa: boolean;


  constructor(
              private feed: FeedbackService,
              private tip: TipService,
              public route: Router,
              public auth: AuthService,
              public translate: TranslateService
              ) {

  }

  ngOnInit(): void {
    this.getNumberOfUnreadFeedbacks()
    this.getNumberOfUnreadTips();
    this.auth.currentUser$.subscribe( res => {
    });

    console.log(localStorage.getItem('token'))

    var lang = this.translate.currentLang;

    if(lang === undefined)
    {
      lang = this.translate.defaultLang;
      this.translate.use(lang);
    }

    this.isUa = lang == 'en'? false: true;
  }


  getNumberOfUnreadTips()
  {
    this.tip.getNumberOfUnreadTips().subscribe( 
      res => 
      {
        this.unreadFeedbacks = +res;
        console.log(+res);
      }
    )
  }

  getNumberOfUnreadFeedbacks()
  {
    this.feed.getNumberOfUnreadFeedbacks().subscribe( 
      res => 
      {
        this.unreadFeedbacks = +res;
        console.log(+res);
      }
    )
  }

  logout()
  {
    this.auth.logout();
  }

  changedLanguage()
  {
    var lang = this.translate.currentLang;
    console.log(lang)
    
    this.translate.use(lang == 'en'? 'ua': 'en');
    
  }
}
