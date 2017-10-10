import { ModuleWithProviders } from '@angular/core';
import { Route, RouterModule } from '@angular/router';

import { AbsenceRequestsComponent } from './absence-requests.component';
import { ListComponent } from './list/list.component';
import { FormComponent } from './form/form.component';

const routes: Route[] = [
    { path: '', component: AbsenceRequestsComponent },
    { path: ':id', component: AbsenceRequestsComponent }
];

export const AbsenceRequestsRoutingModule: ModuleWithProviders = RouterModule.forChild(routes);
