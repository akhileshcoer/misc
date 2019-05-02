import { Component } from '@angular/core';
import { Logger } from 'projects/example-logger/src/lib/contracts';
import { Config } from 'projects/example-logger/src/public-api';



@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  constructor(log: Logger, config: Config) {    
    log.log('test');
  }

  title = 'example-tester';
}
