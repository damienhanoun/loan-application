import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { provideRouter } from '@angular/router';
import { appRoutes } from './app/navigation/app-route';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { AcquisitionApiClient, API_BASE_URL } from './app/services/acquisition-http-service';

bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(appRoutes),
    provideHttpClient(withInterceptorsFromDi()),
    AcquisitionApiClient,
    {provide: API_BASE_URL, useValue: 'https://localhost:7188'}
  ]
})
  .then(() => {
    console.log('Application bootstrapped successfully!');
  })
  .catch(err => {
    console.error('Error bootstrapping the application:', err);
  });
