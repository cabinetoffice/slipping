import { ModuleWithProviders } from '@angular/core';
import { Route, RouterModule } from '@angular/router';

import { ListComponent } from './list/list.component';

const routes: Route[] = [
    { path: '', component: ListComponent }
];

export const UsersRoutes: ModuleWithProviders = RouterModule.forChild(routes);
