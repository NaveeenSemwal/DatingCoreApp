import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { LoaderService } from '../_services/loader.service';
import { delay, finalize } from 'rxjs';

export const loadingInterceptor: HttpInterceptorFn = (req, next) => {

  const loadingService = inject(LoaderService);

  loadingService.loadSpinner();
  
  return next(req).pipe(
    delay(1000),finalize(()=>{

      loadingService.unLoadSpinner();

    }));
};
