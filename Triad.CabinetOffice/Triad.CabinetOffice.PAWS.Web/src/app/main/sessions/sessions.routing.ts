import { ModuleWithProviders } from '@angular/core';
import { Route, RouterModule } from '@angular/router';

import { SessionsComponent } from './sessions.component';

const routes: Route[] = [
    { path: '', component: SessionsComponent }
];

export const SessionsRoutes: ModuleWithProviders = RouterModule.forChild(routes);
