import { Config } from './config.service';
import { Logger } from './contracts';

class ConsoleLogger extends Logger {

  log = (message: string): void => {
    console.log(message);
  }
}

class SpecialLogger extends Logger {

  log = (message: string): void => {
    console.log('I\'m special ' + message);
  }
}

export function LOGGER_PROVIDER_FACTORY(parent: Logger, config: Config): Logger {
  if (parent) return parent;

  if (config.special) {
    return new SpecialLogger();
  }
  return new ConsoleLogger();
}