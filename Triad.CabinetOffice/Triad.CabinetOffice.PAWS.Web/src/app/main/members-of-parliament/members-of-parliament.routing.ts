import { ModuleWithProviders } from '@angular/core';
import { Route, RouterModule } from '@angular/router';

import { MembersOfParliamentComponent } from './members-of-parliament.component';

const routes: Route[] = [
    { path: '', component: MembersOfParliamentComponent }
];

export const MembersOfParliamentRoutes: ModuleWithProviders = RouterModule.forChild(routes);
