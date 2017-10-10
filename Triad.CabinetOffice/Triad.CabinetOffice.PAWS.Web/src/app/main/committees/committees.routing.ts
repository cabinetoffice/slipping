import { ModuleWithProviders } from '@angular/core';
import { Route, RouterModule } from '@angular/router';

import { CommitteesComponent } from './committees.component';

const routes: Route[] = [
    { path: '', component: CommitteesComponent }
];

export const CommitteesRoutes: ModuleWithProviders = RouterModule.forChild(routes);
