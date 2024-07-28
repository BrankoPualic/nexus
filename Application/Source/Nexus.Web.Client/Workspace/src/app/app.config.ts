import { ApplicationConfig, Provider, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import { AccountController } from './_generated/services';
import { SettingsService } from './services/settings.service';
import { provideHttpClient, withFetch, withInterceptors } from '@angular/common/http';
import { provideAnimations } from '@angular/platform-browser/animations';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideClientHydration(),
    provideAnimations(),
    provideHttpClient(
      withFetch(),
      withInterceptors([]),
    ),
    servicesProvider(),
    controllersProvider()]
};

function controllersProvider(): Provider[] {
  return [AccountController];
}

function servicesProvider(): Provider[] {
  return [SettingsService];
}
