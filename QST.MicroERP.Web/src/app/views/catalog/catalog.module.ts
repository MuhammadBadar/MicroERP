import { CatalogService } from './catalog.service';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { CatalogRoutingModule } from './catalog-routing.module';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatCardModule } from '@angular/material/card';
import { MatStepperModule } from '@angular/material/stepper';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatRadioModule } from '@angular/material/radio';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatTreeModule } from '@angular/material/tree';
import { MatTableModule } from '@angular/material/table';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatSortModule } from '@angular/material/sort';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatPaginatorModule } from '@angular/material/paginator';
import {
  AccordionModule,
  BadgeModule,
  BreadcrumbModule,
  ButtonModule,
  CardModule,
  CarouselModule,
  CollapseModule,
  DropdownModule,
  FormModule,
  GridModule,
  ListGroupModule,
  ModalModule,
  NavModule,
  PaginationModule,
  PlaceholderModule,
  PopoverModule,
  ProgressModule,
  SharedModule,
  SpinnerModule,
  TableModule,
  TabsModule,
  TooltipModule,
  UtilitiesModule,


} from '@coreui/angular';
import { IconModule } from '@coreui/icons-angular';
import { MatTooltip, MatTooltipModule } from '@angular/material/tooltip';
import { FlexLayoutModule } from '@angular/flex-layout';
import { HttpClientModule } from '@angular/common/http';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { NotificationTemplateComponent } from './notification-template/notification-template.component';
import { ManageSettingsTypeComponent } from './manage-settings-type/manage-settings-type.component';
import { ManageSettingsComponent } from './manage-settings/manage-settings.component';
import { NgxMatDatetimePickerModule, NgxMatNativeDateModule, NgxMatTimepickerModule } from '@angular-material-components/datetime-picker';
import { MatMomentDateModule } from '@angular/material-moment-adapter';
import { ManageEnumLineComponent } from './manage-enum-line/manage-enum-line.component';
import { ManageEnumLineWithParentComponent } from './manage-enum-line-with-parent/manage-enum-line-with-parent.component';
import { ClientListComponent } from './Client/client-list/client-list.component';
import { ManageClientsComponent } from './Client//manage-clients/manage-clients.component';
import { ModulesComponent } from './modules/modules.component';
import { HomeComponent } from './home/home.component';
import { AuthorizationCheck } from '../security/AuthorizationCheck';
import { SMTPCredsListComponent } from './SMTPCredentials/smtpcreds-list/smtpcreds-list.component';
import { ManageSMTPCredsComponent } from './SMTPCredentials/manage-smtpcreds/manage-smtpcreds.component';

@NgModule({
  declarations: [
    NotificationTemplateComponent,
    ManageSettingsTypeComponent,
    ManageSettingsComponent,
    ManageEnumLineComponent,
    ClientListComponent,
    ManageClientsComponent,
    ModulesComponent,
    HomeComponent,
    ManageEnumLineWithParentComponent,
    SMTPCredsListComponent,
    ManageSMTPCredsComponent
  ],
  imports: [
    NgxMatDatetimePickerModule,
    MatMomentDateModule,
    NgxMatTimepickerModule,
    NgxMatNativeDateModule,
    CommonModule,
    CatalogRoutingModule,
    MatCardModule,
    ReactiveFormsModule,
    FlexLayoutModule,
    FormsModule,
    FlexLayoutModule,
    FormsModule,
    MatTableModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatInputModule,
    MatSnackBarModule,
    HttpClientModule,
    MatTooltipModule,
    MatInputModule,
    MatListModule,
    MatCardModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatProgressBarModule,
    MatRadioModule,
    MatSelectModule,
    MatCheckboxModule,
    MatButtonModule,
    MatIconModule,
    MatStepperModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    AccordionModule,
    BadgeModule,
    BreadcrumbModule,
    ButtonModule,
    CardModule,
    CollapseModule,
    GridModule,
    UtilitiesModule,
    SharedModule,
    ListGroupModule,
    IconModule,
    ListGroupModule,
    PlaceholderModule,
    ProgressModule,
    SpinnerModule,
    TabsModule,
    NavModule,
    TooltipModule,
    CarouselModule,
    FormModule,
    DropdownModule,
    PaginationModule,
    PopoverModule,
    ModalModule,
    TableModule,
    MatTooltipModule,
    IconModule,
    MatTooltipModule,
    HttpClientModule,
    FlexLayoutModule,
  ],
  providers: [DatePipe, CatalogService, AuthorizationCheck],
  schemas: [CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA]
})
export class CatalogModule { }
