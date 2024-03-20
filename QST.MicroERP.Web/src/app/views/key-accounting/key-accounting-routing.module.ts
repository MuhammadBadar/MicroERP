import { ManageItemComponent } from './manage-item/manage-item.component';
import { ManageSupplierComponent } from './manage-supplier/manage-supplier.component';
import { ManageCustomerComponent } from './manage-customer/manage-customer.component';
import { ManageVoucherTypeComponent } from './manage-voucher-type/manage-voucher-type.component';
import { DeliveryChallanListComponent } from './DeliveryChallan/delivery-challan-list/delivery-challan-list.component';
import { ManageDeliveryChallanComponent } from './DeliveryChallan/manage-delivery-challan/manage-delivery-challan.component';
import { VoucherListComponent } from './Voucher/voucher-list/voucher-list.component';
import { ManageVoucherComponent } from './Voucher/manage-voucher/manage-voucher.component';
import { ChartOfAccountComponent } from './chart-of-account/chart-of-account.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StockTransferListComponent } from './StockTransfer/stock-transfer-list/stock-transfer-list.component';
import { ManageStockTransferComponent } from './StockTransfer/manage-stock-transfer/manage-stock-transfer.component';
import { ManagePurchaseOrderComponent } from './PurchaseOrder/manage-purchase-order/manage-purchase-order.component';
import { PurchaseOrderListComponent } from './PurchaseOrder/purchase-order-list/purchase-order-list.component';
import { SalesListComponent } from './Sales/sales-list/sales-list.component';
import { ManageSaleComponent } from './Sales/manage-sale/manage-sale.component';
import { ManageAttributesComponent } from './manage-attributes/manage-attributes.component';
import { ManageAttributesValuesComponent } from './manage-attributes-values/manage-attributes-values.component';
import { ProductListComponent } from './Product/product-list/product-list.component';
import { ManageUOMComponent } from './Product/manage-uom/manage-uom.component';
import { UOMConversionComponent } from './Product/uomconversion/uomconversion.component';
import { ManageDocExtrasComponent } from './Product/manage-doc-extras/manage-doc-extras.component';
import { ChartOfAccountListComponent } from './chart-of-account-list/chart-of-account-list.component';
import { ManageTaxesComponent } from './Product/manage-taxes/manage-taxes.component';
import { ProductTaxesComponent } from './Product/product-taxes/product-taxes.component';
import { AuthorizationCheck } from '../security/AuthorizationCheck';
import { RouteIds } from '../catalog/Models/Enums/RouteIds';

const routes: Routes = [
  {
    path: "tAccount",
    component: ChartOfAccountComponent,
    canActivate: [AuthorizationCheck],
    data: { RouteId: [RouteIds.ChartOfAccount, ''] },
    pathMatch: "full"
  },
  {
    path: "voucher",
    component: ManageVoucherComponent,
    canActivate: [AuthorizationCheck],
    pathMatch: "full"
  },
  {
    path: "sTList",
    component: StockTransferListComponent,
    canActivate: [AuthorizationCheck],
    pathMatch: "full"
  },
  {
    path: "stockTransfer",
    component: ManageStockTransferComponent,
    canActivate: [AuthorizationCheck],
    pathMatch: "full"
  },
  {
    path: "vchList",
    component: VoucherListComponent,
    canActivate: [AuthorizationCheck],
    data: { RouteId: [RouteIds.Vouchers, ''] },
    pathMatch: "full"
  },
  {
    path: "vchType",
    component: ManageVoucherTypeComponent,
    canActivate: [AuthorizationCheck],
    data: { RouteId: [RouteIds.VoucherTypes, ''] },
    pathMatch: "full"
  },
  {
    path: "dc",
    component: ManageDeliveryChallanComponent,
    canActivate: [AuthorizationCheck],
    pathMatch: "full"
  },
  {
    path: "dcList",
    component: DeliveryChallanListComponent,
    canActivate: [AuthorizationCheck],
    pathMatch: "full"
  },
  {
    path: "customer",
    component: ManageCustomerComponent,
    canActivate: [AuthorizationCheck],
    pathMatch: "full"
  },
  {
    path: "supplier",
    component: ManageSupplierComponent,
    canActivate: [AuthorizationCheck],
    pathMatch: "full"
  },
  {
    path: "item",
    component: ManageItemComponent,
    canActivate: [AuthorizationCheck],
    pathMatch: "full"
  },
  {
    path: "mngPurchase",
    component: ManagePurchaseOrderComponent,
    canActivate: [AuthorizationCheck],
    pathMatch: "full"
  },
  {
    path: "purchaseList",
    component: PurchaseOrderListComponent,
    canActivate: [AuthorizationCheck],
    pathMatch: "full"
  },
  {
    path: "mngSale",
    component: ManageSaleComponent,
    canActivate: [AuthorizationCheck],
    pathMatch: "full"
  },
  {
    path: "salesList",
    component: SalesListComponent,
    canActivate: [AuthorizationCheck],
    pathMatch: "full"
  },
  {
    path: "attributes",
    component: ManageAttributesComponent,
    canActivate: [AuthorizationCheck],
    pathMatch: "full"
  },
  {
    path: "attValues",
    component: ManageAttributesValuesComponent,
    canActivate: [AuthorizationCheck],
    pathMatch: "full"
  },
  {
    path: "proList",
    component: ProductListComponent,
    canActivate: [AuthorizationCheck],
    pathMatch: "full"
  },
  {
    path: "uom",
    component: ManageUOMComponent,
    canActivate: [AuthorizationCheck],
    pathMatch: "full"
  },
  {
    path: "uomConvrn",
    component: UOMConversionComponent,
    canActivate: [AuthorizationCheck],
    pathMatch: "full"
  },
  {
    path: "docExtras",
    component: ManageDocExtrasComponent,
    canActivate: [AuthorizationCheck],
    pathMatch: "full"
  },
  {
    path: "accountList",
    component: ChartOfAccountListComponent,
    canActivate: [AuthorizationCheck],
    pathMatch: "full"
  },
  {
    path: "taxes",
    component: ManageTaxesComponent,
    canActivate: [AuthorizationCheck],
    pathMatch: "full"
  },
  {
    path: "proTaxes",
    component: ProductTaxesComponent,
    canActivate: [AuthorizationCheck],
    pathMatch: "full"
  },];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class KeyAccountingRoutingModule { }
