import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';

export class AppHttpInterceptor implements HttpInterceptor {
    intercept(req: HttpRequest<any>,
              next: HttpHandler): Observable<HttpEvent<any>> {

            const token = localStorage.getItem("event-token");

            if (token) {
                const cloned = req.clone({
                    headers: req.headers.set("Authorization",
                        "Bearer " + token)
                        // .set('Content-Type', 'application/json')
                });
    
                return next.handle(cloned);
            }
            else {
                return next.handle(req);
            }
        
      
    }
}