import { ModuleWithProviders } from '@angular/core';
import { Route, RouterModule } from '@angular/router';

import { WhipsFlocksComponent } from './whips-flocks.component';

const routes: Route[] = [
    { path: '', component: WhipsFlocksComponent }
];

export const WhipsFlocksRoutes: ModuleWithProviders = RouterModule.forChild(routes);
