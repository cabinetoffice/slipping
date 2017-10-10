import { ModuleWithProviders } from '@angular/core';
import { Route, RouterModule } from '@angular/router';

import { DivisionsComponent } from './divisions.component';

const routes: Route[] = [
    { path: '', component: DivisionsComponent }
];

export const DivisionsRoutes: ModuleWithProviders = RouterModule.forChild(routes);
