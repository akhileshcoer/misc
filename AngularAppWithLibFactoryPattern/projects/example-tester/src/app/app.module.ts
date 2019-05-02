import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { AppComponent } from './app.component';
import { LOGGER_PROVIDER, Config, ExampleLoggerModule } from 'projects/example-logger/src/public-api';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    ExampleLoggerModule
  ],
  providers: [Config,
    {
      provide: APP_INITIALIZER,
      useFactory: (config: Config) => {        
        return () => {
          config.special = true;      
        }
      },
      multi: true,
      deps: [Config]
    },
    LOGGER_PROVIDER],
  bootstrap: [AppComponent]
})

export class AppModule { }