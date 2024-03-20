import { SecurityModule } from './views/security/security.module';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DefaultLayoutComponent } from './containers';
import { Page500Component } from './views/pages/page500/page500.component';
import { Page401Component } from './views/pages/page401/page401.component';
import { RegisterComponent } from './views/pages/register/register.component';
import { LoginComponent } from './views/security/login/login.component';
import { TokenCheck } from './views/security/TokenCheck';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },
  {
    path: 'secLogin',
    redirectTo: 'secLogin',
    pathMatch: 'full'
  },
  {
    path: '',
    component: DefaultLayoutComponent,
    data: {
      title: 'Home'
    },
    children: [
      {
        path: 'securityLogin',
        loadChildren: () =>
          import('./views/security/security.module').then((m) => m.SecurityModule)
      },
      {
        path: 'unAuth',
        loadChildren: () =>
          import('./views/pages/pages.module').then((m) => m.PagesModule)
      },

      {
        path: 'pms/patient',
        loadChildren: () =>
          import('./views/pms/pms.module').then((m) => m.PMSModule)
      },
      {
        path: 'pms/rx',
        loadChildren: () =>
          import('./views/pms/pms.module').then((m) => m.PMSModule)
      },
      {
        path: 'pms/rxx',
        loadChildren: () =>
          import('./views/pms/pms.module').then((m) => m.PMSModule)
      },
      {
        path: 'pms/pms',
        loadChildren: () =>
          import('./views/pms/pms.module').then((m) => m.PMSModule)
      },
      {
        path: 'att/att',
        loadChildren: () =>
          import('./views/attendance/attendance.module').then((m) => m.AttendanceModule)
      },
      {
        path: 'voc/voc',
        loadChildren: () =>
          import('./views/vocabulary/vocabulary.module').then((m) => m.VocabularyModule)
      },
      {
        path: 'sch/sch',
        loadChildren: () =>
          import('./views/schedule/schedule.module').then((m) => m.ScheduleModule)
      },
      {
        path: 'task/task',
        loadChildren: () =>
          import('./views/tms/task.module').then((m) => m.TaskModule)
      },
      {
        path: 'lms/lms',
        loadChildren: () =>
          import('./views/lms/lms.module').then((m) => m.LMSModule)
      },
      {
        path: 'pms/appt',
        loadChildren: () =>
          import('./views/pms/pms.module').then((m) => m.PMSModule)
      },
      {
        path: 'pms/doctor',
        loadChildren: () =>
          import('./views/pms/pms.module').then((m) => m.PMSModule)
      },
      {
        path: 'pms/staff',
        loadChildren: () =>
          import('./views/pms/pms.module').then((m) => m.PMSModule)
      },
      {
        path: 'client/List',
        loadChildren: () =>
          import('./views/catalog/catalog.module').then((m) => m.CatalogModule)
      },
      {
        path: 'ctl/ctl',
        loadChildren: () =>
          import('./views/catalog/catalog.module').then((m) => m.CatalogModule)
      },
      {
        path: 'ctl/security',
        loadChildren: () =>
          import('./views/security/security.module').then((m) => m.SecurityModule)
      },
      {
        path: 'sLogin',
        loadChildren: () =>
          import('./views/security/security.module').then((m) => m.SecurityModule)
      },
      {
        path: 'keyAccounting',
        loadChildren: () =>
          import('./views/key-accounting/key-accounting.module').then((m) => m.KeyAccountingModule)
      },
      {
        path: 'account/accounts',
        loadChildren: () =>
          import('./views/key-accounting/key-accounting.module').then((m) => m.KeyAccountingModule)
      },
      {
        path: 'account/stakeHolder',
        loadChildren: () =>
          import('./views/key-accounting/key-accounting.module').then((m) => m.KeyAccountingModule)
      },
      {
        path: 'account/product',
        loadChildren: () =>
          import('./views/key-accounting/key-accounting.module').then((m) => m.KeyAccountingModule)
      },
      {
        path: 'account/tax',
        loadChildren: () =>
          import('./views/key-accounting/key-accounting.module').then((m) => m.KeyAccountingModule)
      },
      {
        path: 'product',
        loadChildren: () =>
          import('./views/key-accounting/key-accounting.module').then((m) => m.KeyAccountingModule)
      },
      {
        path: 'stakeHolder',
        loadChildren: () =>
          import('./views/key-accounting/key-accounting.module').then((m) => m.KeyAccountingModule)
      },
      {
        path: 'product',
        loadChildren: () =>
          import('./views/key-accounting/key-accounting.module').then((m) => m.KeyAccountingModule)
      },
      {
        path: 'tax',
        loadChildren: () =>
          import('./views/key-accounting/key-accounting.module').then((m) => m.KeyAccountingModule)
      },
      {
        path: 'catalog',
        loadChildren: () =>
          import('./views/catalog/catalog.module').then((m) => m.CatalogModule)
      },
      {
        path: 'notification',
        loadChildren: () =>
          import('./views/catalog/catalog.module').then((m) => m.CatalogModule)
      },
      {
        path: 'account',
        loadChildren: () =>
          import('./views/account/account.module').then((m) => m.AccountModule)
      },
      {
        path: 'settings',
        loadChildren: () =>
          import('./views/catalog/catalog.module').then((m) => m.CatalogModule)
      },
      {
        path: 'account/settings',
        loadChildren: () =>
          import('./views/catalog/catalog.module').then((m) => m.CatalogModule)
      },
      {
        path: 'pms/settings',
        loadChildren: () =>
          import('./views/catalog/catalog.module').then((m) => m.CatalogModule)
      },
      {
        path: 'test/testing',
        loadChildren: () =>
          import('./views/items/items.module').then((m) => m.ItemsModule)
        // import('./views/key-accounting/key-accounting.module').then((m) => m.KeyAccountingModule)
      },
      {
        path: 'catalog',
        loadChildren: () =>
          import('./views/items/items.module').then((m) => m.ItemsModule)
      },
      {
        path: 'account/security/security',
        loadChildren: () =>
          import('./views/security/security.module').then((m) => m.SecurityModule)
      },
      {
        path: 'security/security',
        loadChildren: () =>
          import('./views/security/security.module').then((m) => m.SecurityModule)
      },
      {
        path: 'security/catalog',
        loadChildren: () =>
          import('./views/catalog/catalog.module').then((m) => m.CatalogModule)
      },
      {
        path: 'pms/security/catalog',
        loadChildren: () =>
          import('./views/catalog/catalog.module').then((m) => m.CatalogModule)
      },
      {
        path: 'pms/security/security',
        loadChildren: () =>
          import('./views/security/security.module').then((m) => m.SecurityModule)
      },
      {
        path: 'account/security/catalog',
        loadChildren: () =>
          import('./views/catalog/catalog.module').then((m) => m.CatalogModule)
      },

      {
        path: 'dashboard',
        loadChildren: () =>
          import('./views/dashboard/dashboard.module').then((m) => m.DashboardModule)
      },
      {
        path: 'theme',
        loadChildren: () =>
          import('./views/theme/theme.module').then((m) => m.ThemeModule)
      },
      {
        path: 'base',
        loadChildren: () =>
          import('./views/base/base.module').then((m) => m.BaseModule)
      },
      {
        path: 'buttons',
        loadChildren: () =>
          import('./views/buttons/buttons.module').then((m) => m.ButtonsModule)
      }
      ,
      {
        path: 'my',
        loadChildren: () =>
          import('./views/buttons/buttons.module').then((m) => m.ButtonsModule)
      },
      {
        path: 'forms',
        loadChildren: () =>
          import('./views/forms/forms.module').then((m) => m.CoreUIFormsModule)
      },
      {
        path: 'charts',
        loadChildren: () =>
          import('./views/charts/charts.module').then((m) => m.ChartsModule)
      },
      {
        path: 'icons',
        loadChildren: () =>
          import('./views/icons/icons.module').then((m) => m.IconsModule)
      },
      {
        path: 'notifications',
        loadChildren: () =>
          import('./views/notifications/notifications.module').then((m) => m.NotificationsModule)
      },
      {
        path: 'widgets',
        loadChildren: () =>
          import('./views/widgets/widgets.module').then((m) => m.WidgetsModule)
      },
      {
        path: 'pages',
        loadChildren: () =>
          import('./views/pages/pages.module').then((m) => m.PagesModule)
      },
    ]
  },
  {
    path: '401',
    component: Page401Component,
    //canActivate: [TokenCheck],
    data: {
      title: 'Page 401',
    }
  },
  {
    path: '500',
    component: Page500Component,
    data: {
      title: 'Page 500',
    }
  },
  {
    path: '500',
    component: Page500Component,
    data: {
      title: 'Page 500'
    }
  },
  {
    path: 'securityLogin',
    component: SecurityModule,

  },
  {
    path: 'login',
    component: LoginComponent,
    canActivate: [TokenCheck],
    data: {
      title: 'Login Page'
    }
  },
  {
    path: 'secLogin',
    component: LoginComponent,
    canActivate: [TokenCheck],
    data: {
      title: 'Login Page',
      Route: 'secLogin'
    }
  },

  {
    path: 'register',
    component: RegisterComponent,
    data: {
      title: 'Register Page'
    }
  },
  { path: '**', redirectTo: ' ' }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, {
      scrollPositionRestoration: 'top',
      anchorScrolling: 'enabled',
      initialNavigation: 'enabledBlocking'
      // relativeLinkResolution: 'legacy'
    })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
