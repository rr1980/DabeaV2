
import { ErrorHandler, Injectable } from '@angular/core';

@Injectable()
export class GlobalErrorHandler implements ErrorHandler {
  
  handleError(error) {
    const err = {
      message: error.message ? error.message : error.toString(),
      stack: error.stack ? error.stack : ''
    };

    // Log  the error
    console.log("++",err);

        // Optionally send it to your back-end API
        // Notify the user
  }
}
