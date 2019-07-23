
import { ErrorHandler, Injectable } from '@angular/core';

@Injectable()
export class GlobalErrorHandler implements ErrorHandler {
  
  handleError(error) {
    const err = {
      message: error.message ? error.message : error.Message ? error.Message : error.toString(),
      stack: error.stack ? error.stack : error.Stack ? error.Stack : ''
    };

    // Log  the error
    console.error(error);

        // Optionally send it to your back-end API
        // Notify the user
  }
}
