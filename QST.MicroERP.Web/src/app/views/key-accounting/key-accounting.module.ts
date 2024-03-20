import { ChartOfAccountComponent } from './chart-of-account/chart-of-account.component';
import { KeyAccountingService } from './key-accounting.service';
import { CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';

import { KeyAccountingRoutingModule } from './key-accounting-routing.module';

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
import { ManageVoucherComponent } from './Voucher/manage-voucher/manage-voucher.component';
import { VoucherListComponent } from './Voucher/voucher-list/voucher-list.component';
import { ManageDeliveryChallanComponent } from './DeliveryChallan/manage-delivery-challan/manage-delivery-challan.component';
import { DeliveryChallanListComponent } from './DeliveryChallan/delivery-challan-list/delivery-challan-list.component';
import { ManageCustomerComponent } from './manage-customer/manage-customer.component';
import { ManageSupplierComponent } from './manage-supplier/manage-supplier.component';
import { ManageItemComponent } from './manage-item/manage-item.component';
import { ManageStockTransferComponent } from './StockTransfer/manage-stock-transfer/manage-stock-transfer.component';
import { StockTransferListComponent } from './StockTransfer/stock-transfer-list/stock-transfer-list.component';
import { ManagePurchaseOrderComponent } from './PurchaseOrder/manage-purchase-order/manage-purchase-order.component';
import { PurchaseOrderListComponent } from './PurchaseOrder/purchase-order-list/purchase-order-list.component';
import { ManageSaleComponent } from './Sales/manage-sale/manage-sale.component';
import { SalesListComponent } from './Sales/sales-list/sales-list.component';
import { ManageAttributesComponent } from './manage-attributes/manage-attributes.component';
import { ManageAttributesValuesComponent } from './manage-attributes-values/manage-attributes-values.component';
import { MatTabsModule } from '@angular/material/tabs';
import { ProductListComponent } from './Product/product-list/product-list.component';
import { ConfigureProductComponent } from './Product/configure-product/configure-product.component';
import { ConfigureProductAttribComponent } from './Product/configure-product-attrib/configure-product-attrib.component';
import { ConfigureProductByVariantComponent } from './Product/configure-product-by-variant/configure-product-by-variant.component';
import { ConfigurePurchaseRateComponent } from './Product/configure-purchase-rate/configure-purchase-rate.component';
import { ConfigureSaleRateComponent } from './Product/configure-sale-rate/configure-sale-rate.component';
import { ConfigurePurchaseRateByVariantComponent } from './Product/configure-purchase-rate-by-variant/configure-purchase-rate-by-variant.component';
import { ConfigureSaleRateByVariantComponent } from './Product/configure-sale-rate-by-variant/configure-sale-rate-by-variant.component';
import { ConfigureBarCodeComponent } from './Product/configure-bar-code/configure-bar-code.component';
import { ManageUOMComponent } from './Product/manage-uom/manage-uom.component';
import { UOMConversionComponent } from './Product/uomconversion/uomconversion.component';
import { SalesUOMComponent } from './Product/sales-uom/sales-uom.component';
import { PurchaseUOMComponent } from './Product/purchase-uom/purchase-uom.component';
import { ManageDocExtrasComponent } from './Product/manage-doc-extras/manage-doc-extras.component';
import { ChartOfAccountListComponent } from './chart-of-account-list/chart-of-account-list.component';
import { ProductTaxesComponent } from './Product/product-taxes/product-taxes.component';
import { ManageTaxesComponent } from './Product/manage-taxes/manage-taxes.component';
import { NgxMatDatetimePickerModule, NgxMatNativeDateModule, NgxMatTimepickerModule } from '@angular-material-components/datetime-picker';
import { MatMomentDateModule } from '@angular/material-moment-adapter';
import { ManageVoucherTypeComponent } from './manage-voucher-type/manage-voucher-type.component';
import { AuthorizationCheck } from '../security/AuthorizationCheck';


@NgModule({
  declarations: [
    ChartOfAccountComponent,
    ManageVoucherComponent,
    VoucherListComponent,
    ManageDeliveryChallanComponent,
    DeliveryChallanListComponent,
    ManageCustomerComponent,
    ManageSupplierComponent,
    ManageItemComponent,
    ManageStockTransferComponent,
    StockTransferListComponent,
    ManagePurchaseOrderComponent,
    PurchaseOrderListComponent,
    ManageSaleComponent,
    SalesListComponent,
    ManageAttributesComponent,
    ManageAttributesValuesComponent,
    ProductListComponent,
    ConfigureProductComponent,
    ConfigureProductAttribComponent,
    ConfigureProductByVariantComponent,
    ConfigurePurchaseRateComponent,
    ConfigureSaleRateComponent,
    ConfigurePurchaseRateByVariantComponent,
    ConfigureSaleRateByVariantComponent,
    ConfigureBarCodeComponent,
    ManageUOMComponent,
    UOMConversionComponent,
    SalesUOMComponent,
    PurchaseUOMComponent,
    ManageDocExtrasComponent,
    ChartOfAccountListComponent,
    ProductTaxesComponent,
    ManageTaxesComponent,
    ManageVoucherTypeComponent
  ],
  imports: [
    NgxMatDatetimePickerModule,
    MatMomentDateModule,
    NgxMatTimepickerModule,
    NgxMatNativeDateModule,
    CommonModule,
    MatTabsModule,
    KeyAccountingRoutingModule,
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
  providers: [DatePipe, KeyAccountingService, AuthorizationCheck],
  schemas: [CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA]
})
export class KeyAccountingModule { }
