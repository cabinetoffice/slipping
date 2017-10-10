import { ModuleWithProviders } from '@angular/core';
import { Route, RouterModule } from '@angular/router';

import { PartiesComponent } from './parties.component';

const routes: Route[] = [
    { path: '', component: PartiesComponent }
];

export const PartiesRoutes: ModuleWithProviders = RouterModule.forChild(routes);
