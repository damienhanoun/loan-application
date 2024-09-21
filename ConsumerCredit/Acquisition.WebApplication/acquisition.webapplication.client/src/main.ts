import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { provideRouter } from '@angular/router';
import { appRoutes } from './app/navigation/app-route';

bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(appRoutes)
  ]
})
  .then(() => {
    console.log('Application bootstrapped successfully!');
  })
  .catch(err => {
    console.error('Error bootstrapping the application:', err);
  });
