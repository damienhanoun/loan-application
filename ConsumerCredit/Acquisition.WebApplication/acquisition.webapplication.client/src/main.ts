import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { provideRouter } from '@angular/router';
import {
  provideHttpClient,
  withInterceptorsFromDi,
} from '@angular/common/http';
import {
  AcquisitionApiClient,
  API_BASE_URL,
} from './app/services/acquisition-http-service';
import {
  creditApplicationJourneyNavigationConfiguration,
  JOURNEY_STEPS,
} from './app/journey/journey.configuration';
import { appRoutes } from './app/journey/app-route';
import { provideStoreDevtools } from '@ngrx/store-devtools';
import { isDevMode } from '@angular/core';

bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(appRoutes),
    provideHttpClient(withInterceptorsFromDi()),
    AcquisitionApiClient,
    { provide: API_BASE_URL, useValue: 'https://localhost:7188' },
    {
      provide: JOURNEY_STEPS,
      useValue: creditApplicationJourneyNavigationConfiguration,
    },
    provideStoreDevtools({
      maxAge: 25, // Retains last 25 states
      logOnly: !isDevMode(), // Restrict extension to log-only mode
      autoPause: true, // Pauses recording actions and state changes when the extension window is not open
      trace: true, //  If set to true, will include stack trace for every dispatched action, so you can see it in trace tab jumping directly to that part of code
      traceLimit: 75, // maximum stack trace frames to be stored (in case trace option was provided as true)
      connectInZone: true, // If set to true, the connection is established within the Angular zone
    }),
  ],
})
  .then(() => {
    console.log('Application bootstrapped successfully!');
  })
  .catch((err) => {
    console.error('Error bootstrapping the application:', err);
  });
