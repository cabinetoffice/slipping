import { ModuleWithProviders } from '@angular/core';
import { Route, RouterModule } from '@angular/router';
 
import { AuthenticationGuard } from './common/guards/authentication-guard';
import { MainComponent, HomeComponent, LoginComponent, LogoutComponent } from './';
 
const routes: Route[] = [
  { path: 'error', loadChildren: 'app/error-pages/error-pages.module#ErrorPagesModule'},
  { path: 'login', component: LoginComponent },
  { path: 'logout', component: LogoutComponent },
  { path: '', component: MainComponent, canActivate: [AuthenticationGuard], canActivateChild: [AuthenticationGuard], children: [
    { path: '', component: HomeComponent },
    { path: 'divisions', loadChildren: 'app/main/divisions/divisions.module#DivisionsModule'},
    { path: 'absence-requests', loadChildren: 'app/main/absence-requests/absence-requests.module#AbsenceRequestsModule'},
    { path: 'committees', loadChildren: 'app/main/committees/committees.module#CommitteesModule'},
    { path: 'members-of-parliament', loadChildren: 'app/main/members-of-parliament/members-of-parliament.module#MembersOfParliamentModule'},
    { path: 'parties', loadChildren: 'app/main/parties/parties.module#PartiesModule'},
    { path: 'whips-flocks', loadChildren: 'app/main/whips-flocks/whips-flocks.module#WhipsFlocksModule'},
    { path: 'sessions', loadChildren: 'app/main/sessions/sessions.module#SessionsModule'},
    { path: 'users', loadChildren: 'app/main/users/users.module#UsersModule'},
  ]},
  { path: '**', pathMatch: 'full', redirectTo: 'error/404' }
];
 
export const AppRoutes: ModuleWithProviders = RouterModule.forRoot(routes);
