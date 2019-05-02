import { NgModule, Provider, Optional, SkipSelf } from '@angular/core';
import { Logger } from './contracts';
import { Config, LOGGER_PROVIDER_FACTORY } from '../public-api';


export const LOGGER_PROVIDER: Provider = {
  provide: Logger,
  deps: [[new Optional(), new SkipSelf(), Logger], Config],
  useFactory: LOGGER_PROVIDER_FACTORY,
};

@NgModule({
  declarations: [],
  imports: [
  ],
  exports: [],
  providers: [Config]
})

export class ExampleLoggerModule { }
