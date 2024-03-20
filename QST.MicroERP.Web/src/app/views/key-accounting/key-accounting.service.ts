import { AttributeValuesVm } from './Models/AttributesValuesVm';
import { AttributesVm } from './Models/AttributesVm';
import { ItemVariantsVM } from './Models/ItemVariants';
import { PurchaseVM } from './Models/PurchaseOrderVM';
import { StockTransferVM } from './Models/StockTransferVM';
import { VoucherTypeVM } from './Models/VoucherTypeVM';
import { DCVM } from './Models/DeliveryChallanVM';
import { Globals } from './../../globals';
import { VoucherDetailsVM, VoucherVM } from './Models/VoucherVM';
import { ItemVM, ProductAttribVm, ProductwithVariantsVM } from './Models/ItemVM';
import { SupplierVM } from './Models/SupplierVM';
import { CustomerVM } from './Models/CustomerVM';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { delay, Observable, of, Subject } from 'rxjs';
import { SalesLineVM, SalesVM } from './Models/SalesVM';
import { UOMConversionVm } from './Models/UOMConversionVm';
import { ItemUOMVm } from './Models/ItemUOMVm';
import { DocExtrasVM } from './Models/DocExtrasVM'
import { ProductTaxesVM } from './Models/ProductTaxesVM';
import { SaleGrossAccounts } from './Models/Enum/SaleGrossAccounts';
import { SalesAccounts } from './Models/Enum/SalesAccounts';
import { ItemTypes } from './Models/Enum/ItemTypes';
import { SettingsVM } from '../catalog/Models/SettingsVM';
import { Statses } from './Models/Enum/Status';
import { VchTypes } from './Models/Enum/VchTypes';
import { ItemsService } from '../items/items.service';
import { CatalogService } from '../catalog/catalog.service';
import { NotificationTemplateVM } from '../catalog/Models/NotificationTemplateVM';
import { NotificationVM } from '../catalog/Models/NotificationVM';

@Injectable({
  providedIn: 'root'
})
export class KeyAccountingService {
  voucher: VoucherVM
  selectedSales: SalesVM
  selectedSaleLines: SalesLineVM
  constructor(private http: HttpClient,
    private catSvc: CatalogService
  ) {
    this.voucher = new VoucherVM
  }


  UpdateVoucher(data: VoucherVM): Observable<VoucherVM> {
    return this.http.put<VoucherVM>(Globals.BASE_API_URL + 'Voucher', data).pipe();
  }
  GetVoucherById(id: number): Observable<VoucherVM[]> {

    return this.http.get<VoucherVM[]>(Globals.BASE_API_URL + 'Voucher/' + id).pipe()
  }
  SearchVoucher(data: VoucherVM): Observable<VoucherVM[]> {
    return this.http.post<VoucherVM[]>(Globals.BASE_API_URL + 'Voucher/Search', data).pipe();
  }
  GetVoucher(): Observable<VoucherVM[]> {
    return this.http.get<VoucherVM[]>(Globals.BASE_API_URL + 'Voucher').pipe();
  }
  SaveVoucher(data: VoucherVM): Observable<VoucherVM> {
    return this.http.post<VoucherVM>(Globals.BASE_API_URL + 'Voucher', data).pipe();
  }
  DeleteVoucher(id: number) {
    return this.http.delete(Globals.BASE_API_URL + 'Voucher/' + id);
  }
  ActivateVoucher(mod: VoucherVM) {
    return this.http.put(Globals.BASE_API_URL + 'Voucher/Update/Activate', mod);
  }
  DeActivateVoucher(mod: VoucherVM) {
    return this.http.put(Globals.BASE_API_URL + 'Voucher/DeActivate', mod);
  }


  UpdateDC(value: DCVM): Observable<DCVM> {
    return this.http.put<DCVM>(Globals.BASE_API_URL + 'DC', value).pipe();
  }
  GetDCById(id: number): Observable<DCVM[]> {
    return this.http.get<DCVM[]>(Globals.BASE_API_URL + 'DC/' + id).pipe()
  }
  SearchDC(value: DCVM): Observable<DCVM[]> {
    return this.http.post<DCVM[]>(Globals.BASE_API_URL + 'DC/Search', value).pipe();
  }
  GetDC(): Observable<DCVM[]> {
    return this.http.get<DCVM[]>(Globals.BASE_API_URL + 'DC').pipe();
  }
  SaveDC(value: DCVM): Observable<DCVM> {
    return this.http.post<DCVM>(Globals.BASE_API_URL + 'DC', value).pipe();
  }
  DeleteDC(id: number) {
    return this.http.delete(Globals.BASE_API_URL + 'DC/' + id);
  }


  GetItem(): Observable<ItemVM[]> {
    return this.http.get<ItemVM[]>(Globals.BASE_API_URL + 'Item').pipe();
  }
  UpdateItem(Item: ItemVM): Observable<ItemVM> {
    return this.http.put<ItemVM>(Globals.BASE_API_URL + 'Item', Item).pipe();
  }
  GetItemById(id: number): Observable<ItemVM> {
    return this.http.get<ItemVM>(Globals.BASE_API_URL + 'Item/' + id).pipe();
  }
  SearchItem(Item: ItemVM): Observable<ItemVM[]> {
    return this.http.post<ItemVM[]>(Globals.BASE_API_URL + 'Item/Search', Item).pipe();
  }
  SaveItem(tem: ItemVM): Observable<ItemVM> {
    return this.http.post<ItemVM>(Globals.BASE_API_URL + 'Item', tem).pipe();
  }
  DeleteItem(id: number) {
    return this.http.delete(Globals.BASE_API_URL + 'Item/' + id);
  }


  UpdateCustomer(Customer: CustomerVM) {
    return this.http.put(Globals.BASE_API_URL + 'Customer', Customer);
  }
  DealCustomerAsASupplier(Customer: CustomerVM) {
    return this.http.put(Globals.BASE_API_URL + 'Customer/Customer', Customer);
  }
  GetCustomerById(id: number): Observable<CustomerVM[]> {

    return this.http.get<CustomerVM[]>(Globals.BASE_API_URL + 'Customer/' + id).pipe()
  }
  SearchCustomer(Customer: CustomerVM): Observable<CustomerVM[]> {
    return this.http.post<CustomerVM[]>(Globals.BASE_API_URL + 'Customer/Search', Customer).pipe();
  }
  GetCustomer(): Observable<CustomerVM[]> {
    return this.http.get<CustomerVM[]>(Globals.BASE_API_URL + 'Customer').pipe();
  }
  SaveCustomer(Customer: CustomerVM): Observable<any> {
    return this.http.post(Globals.BASE_API_URL + 'Customer', Customer);
  }
  DeleteCustomer(id: number) {
    return this.http.delete(Globals.BASE_API_URL + 'Customer/' + id);
  }


  UpdateSupplier(Supplier: SupplierVM) {
    return this.http.put(Globals.BASE_API_URL + 'Supplier', Supplier);
  }
  DealSupplierAsACustomer(Supplier: SupplierVM) {
    return this.http.put(Globals.BASE_API_URL + 'Supplier/Supplier', Supplier);
  }
  GetSupplierById(id: number): Observable<SupplierVM[]> {

    return this.http.get<SupplierVM[]>(Globals.BASE_API_URL + 'Supplier/' + id).pipe()
  }
  SearchSupplier(Supplier: SupplierVM): Observable<SupplierVM[]> {
    return this.http.post<SupplierVM[]>(Globals.BASE_API_URL + 'Supplier/Search', Supplier).pipe();
  }
  GetSupplier(): Observable<SupplierVM[]> {
    return this.http.get<SupplierVM[]>(Globals.BASE_API_URL + 'Supplier').pipe();
  }
  SaveSupplier(Supplier: SupplierVM): Observable<any> {
    return this.http.post(Globals.BASE_API_URL + 'Supplier', Supplier);
  }
  DeleteSupplier(id: number) {
    return this.http.delete(Globals.BASE_API_URL + 'Supplier/' + id);
  }


  UpdateStockTransfer(value: StockTransferVM): Observable<StockTransferVM> {
    return this.http.put<StockTransferVM>(Globals.BASE_API_URL + 'StockTransfer', value);
  }
  GetStockTransferById(id: number): Observable<StockTransferVM[]> {
    return this.http.get<StockTransferVM[]>(Globals.BASE_API_URL + 'StockTransfer/' + id).pipe()
  }
  SearchStockTransfer(value: StockTransferVM): Observable<StockTransferVM[]> {
    return this.http.post<StockTransferVM[]>(Globals.BASE_API_URL + 'StockTransfer/Search', value).pipe();
  }
  GetStockTransfer(): Observable<StockTransferVM[]> {
    return this.http.get<StockTransferVM[]>(Globals.BASE_API_URL + 'StockTransfer').pipe();
  }
  SaveStockTransfer(value: StockTransferVM): Observable<StockTransferVM> {
    return this.http.post<StockTransferVM>(Globals.BASE_API_URL + 'StockTransfer', value);
  }
  DeleteStockTransfer(id: number) {
    return this.http.delete(Globals.BASE_API_URL + 'StockTransfer/' + id);
  }


  UpdateVoucherType(VoucherType: VoucherTypeVM) {
    return this.http.put(Globals.BASE_API_URL + 'VoucherType', VoucherType);
  }
  GetVoucherTypeById(id: number): Observable<VoucherTypeVM[]> {

    return this.http.get<VoucherTypeVM[]>(Globals.BASE_API_URL + 'VoucherType/' + id).pipe()
  }
  SearchVoucherType(VoucherType: VoucherTypeVM): Observable<VoucherTypeVM[]> {
    return this.http.post<VoucherTypeVM[]>(Globals.BASE_API_URL + 'VoucherType/Search', VoucherType).pipe();
  }
  GetVoucherType(): Observable<VoucherTypeVM[]> {
    return this.http.get<VoucherTypeVM[]>(Globals.BASE_API_URL + 'VoucherType').pipe();
  }
  SaveVoucherType(VoucherType: VoucherTypeVM): Observable<any> {
    return this.http.post(Globals.BASE_API_URL + 'VoucherType', VoucherType);
  }
  DeleteVoucherType(id: number) {
    return this.http.delete(Globals.BASE_API_URL + 'VoucherType/' + id);
  }


  UpdatePurchase(value: PurchaseVM): Observable<PurchaseVM> {
    return this.http.put<PurchaseVM>(Globals.BASE_API_URL + 'Purchase', value);
  }
  SearchPurchase(value: PurchaseVM): Observable<PurchaseVM[]> {
    return this.http.post<PurchaseVM[]>(Globals.BASE_API_URL + 'Purchase/Search', value).pipe();
  }
  GetPurchase(): Observable<PurchaseVM[]> {
    return this.http.get<PurchaseVM[]>(Globals.BASE_API_URL + 'Purchase').pipe();
  }
  SavePurchase(value: PurchaseVM): Observable<PurchaseVM> {
    return this.http.post<PurchaseVM>(Globals.BASE_API_URL + 'Purchase', value);
  }
  DeletePurchase(id: number) {
    return this.http.delete(Globals.BASE_API_URL + 'Purchase/' + id);
  }


  UpdateSale(value: SalesVM): Observable<SalesVM> {
    return this.http.put<SalesVM>(Globals.BASE_API_URL + 'Sale', value);
  }
  SearchSale(value: SalesVM): Observable<SalesVM[]> {
    return this.http.post<SalesVM[]>(Globals.BASE_API_URL + 'Sale/Search', value).pipe();
  }
  GetSale(): Observable<SalesVM[]> {
    return this.http.get<SalesVM[]>(Globals.BASE_API_URL + 'Sale').pipe();
  }
  SaveSale(value: SalesVM): Observable<SalesVM> {
    return this.http.post<SalesVM>(Globals.BASE_API_URL + 'Sale', value);
  }
  DeleteSale(id: number) {
    return this.http.delete(Globals.BASE_API_URL + 'Sale/' + id);
  }


  UpdateAttributes(Attributes: AttributesVm) {
    return this.http.put(Globals.BASE_API_URL + 'Attributes', Attributes);
  }
  GetAttributesById(id: number): Observable<AttributesVm> {

    return this.http.get<AttributesVm>(Globals.BASE_API_URL + 'Attributes/' + id).pipe()
  }
  SearchAttributes(Attributes: AttributesVm): Observable<AttributesVm[]> {
    return this.http.post<AttributesVm[]>(Globals.BASE_API_URL + 'Attributes/Search', Attributes).pipe();
  }
  GetAttributes(): Observable<AttributesVm[]> {
    return this.http.get<AttributesVm[]>(Globals.BASE_API_URL + 'Attributes').pipe();
  }
  SaveAttributes(Attributes: AttributesVm): Observable<any> {
    return this.http.post(Globals.BASE_API_URL + 'Attributes', Attributes);
  }
  DeleteAttributes(id: number) {
    return this.http.delete(Globals.BASE_API_URL + 'Attributes/' + id);
  }



  UpdateAttributeValues(AttributeValues: AttributeValuesVm) {
    return this.http.put(Globals.BASE_API_URL + 'AttributeValues', AttributeValues);
  }
  GetAttributeValuesById(id: number): Observable<AttributeValuesVm> {

    return this.http.get<AttributeValuesVm>(Globals.BASE_API_URL + 'AttributeValues/' + id).pipe()
  }
  SearchAttributeValues(AttributeValues: AttributeValuesVm): Observable<AttributeValuesVm[]> {
    return this.http.post<AttributeValuesVm[]>(Globals.BASE_API_URL + 'AttributeValues/Search', AttributeValues).pipe();
  }
  GetAttributeValues(): Observable<AttributeValuesVm[]> {
    return this.http.get<AttributeValuesVm[]>(Globals.BASE_API_URL + 'AttributeValues').pipe();
  }
  SaveAttributeValues(AttributeValues: AttributeValuesVm): Observable<any> {
    return this.http.post(Globals.BASE_API_URL + 'AttributeValues', AttributeValues);
  }
  DeleteAttributeValues(id: number) {
    return this.http.delete(Globals.BASE_API_URL + 'AttributeValues/' + id);
  }


  UpdateProductAttrib(value: ProductAttribVm): Observable<ProductAttribVm> {
    return this.http.put<ProductAttribVm>(Globals.BASE_API_URL + 'ProductAttrib', value);
  }
  SearchProductAttrib(value: ProductAttribVm): Observable<ProductAttribVm[]> {
    return this.http.post<ProductAttribVm[]>(Globals.BASE_API_URL + 'ProductAttrib/Search', value).pipe();
  }
  GetProductAttrib(): Observable<ProductAttribVm[]> {
    return this.http.get<ProductAttribVm[]>(Globals.BASE_API_URL + 'ProductAttrib').pipe();
  }
  SaveProductAttrib(value: ProductAttribVm): Observable<ProductAttribVm> {
    return this.http.post<ProductAttribVm>(Globals.BASE_API_URL + 'ProductAttrib', value);
  }
  DeleteProductAttrib(id: number) {
    return this.http.delete(Globals.BASE_API_URL + 'ProductAttrib/' + id);
  }


  UpdateItemVariants(value: ItemVariantsVM) {
    return this.http.put(Globals.BASE_API_URL + 'ItemVariants', value);
  }
  GetItemVariantsById(id: number): Observable<ItemVariantsVM> {
    return this.http.get<ItemVariantsVM>(Globals.BASE_API_URL + 'ItemVariants/' + id).pipe()
  }
  SearchItemVariants(value: ItemVariantsVM): Observable<ItemVariantsVM[]> {
    return this.http.post<ItemVariantsVM[]>(Globals.BASE_API_URL + 'ItemVariants/Search', value).pipe();
  }
  GetItemVariants(): Observable<ItemVariantsVM[]> {
    return this.http.get<ItemVariantsVM[]>(Globals.BASE_API_URL + 'ItemVariants').pipe();
  }
  SaveItemVariants(value: ItemVariantsVM): Observable<any> {
    return this.http.post(Globals.BASE_API_URL + 'ItemVariants', value);
  }
  DeleteItemVariants(id: number) {
    return this.http.delete(Globals.BASE_API_URL + 'ItemVariants/' + id);
  }


  UpdateItemUOM(value: ItemUOMVm) {
    return this.http.put('https://localhost:8100/api/ItemUOM', value);
  }
  GetItemUOMById(id: number): Observable<ItemUOMVm> {
    return this.http.get<ItemUOMVm>('https://localhost:8100/api/ItemUOM/' + id).pipe()
  }
  SearchItemUOM(value: ItemUOMVm): Observable<ItemUOMVm[]> {
    return this.http.post<ItemUOMVm[]>('https://localhost:8100/api/ItemUOM/Search', value).pipe();
  }
  GetItemUOM(): Observable<ItemUOMVm[]> {
    return this.http.get<ItemUOMVm[]>('https://localhost:8100/api/ItemUOM').pipe();
  }
  SaveItemUOM(value: ItemUOMVm): Observable<any> {
    return this.http.post('https://localhost:8100/api/ItemUOM', value);
  }
  DeleteItemUOM(id: number) {
    return this.http.delete('https://localhost:8100/api/ItemUOM/' + id);
  }


  UpdateUOMConversion(value: UOMConversionVm) {
    return this.http.put('https://localhost:8100/api/UOMConversion', value);
  }
  GetUOMConversionById(id: number): Observable<UOMConversionVm> {
    return this.http.get<UOMConversionVm>('https://localhost:8100/api/UOMConversion/' + id).pipe()
  }
  SearchUOMConversion(value: UOMConversionVm): Observable<UOMConversionVm[]> {
    return this.http.post<UOMConversionVm[]>('https://localhost:8100/api/UOMConversion/Search', value).pipe();
  }
  GetUOMConversion(): Observable<UOMConversionVm[]> {
    return this.http.get<UOMConversionVm[]>('https://localhost:8100/api/UOMConversion').pipe();
  }
  SaveUOMConversion(value: UOMConversionVm): Observable<any> {
    return this.http.post('https://localhost:8100/api/UOMConversion', value);
  }
  DeleteUOMConversion(id: number) {
    return this.http.delete('https://localhost:8100/api/UOMConversion/' + id);
  }


  UpdateDocExtras(value: DocExtrasVM) {
    return this.http.put(Globals.BASE_API_URL + 'DocExtras', value);
  }
  GetDocExtrasById(id: number): Observable<DocExtrasVM> {
    return this.http.get<DocExtrasVM>(Globals.BASE_API_URL + 'DocExtras/' + id).pipe()
  }
  SearchDocExtras(value: DocExtrasVM): Observable<DocExtrasVM[]> {
    return this.http.post<DocExtrasVM[]>(Globals.BASE_API_URL + 'DocExtras/Search', value).pipe();
  }
  GetDocExtras(): Observable<DocExtrasVM[]> {
    return this.http.get<DocExtrasVM[]>(Globals.BASE_API_URL + 'DocExtras').pipe();
  }
  SaveDocExtras(value: DocExtrasVM): Observable<any> {
    return this.http.post(Globals.BASE_API_URL + 'DocExtras', value);
  }
  DeleteDocExtras(id: number) {
    return this.http.delete(Globals.BASE_API_URL + 'DocExtras/' + id);
  }


  UpdateProductTaxes(value: ProductTaxesVM) {
    return this.http.put('https://localhost:8100/api/ProductTaxes', value);
  }
  GetProductTaxesById(id: number): Observable<ProductTaxesVM> {
    return this.http.get<ProductTaxesVM>('https://localhost:8100/api/ProductTaxes/' + id).pipe()
  }
  SearchProductTaxes(value: ProductTaxesVM): Observable<ProductTaxesVM[]> {
    return this.http.post<ProductTaxesVM[]>('https://localhost:8100/api/ProductTaxes/Search', value).pipe();
  }
  GetProductTaxes(): Observable<ProductTaxesVM[]> {
    return this.http.get<ProductTaxesVM[]>('https://localhost:8100/api/ProductTaxes').pipe();
  }
  SaveProductTaxes(value: ProductTaxesVM): Observable<any> {
    return this.http.post('https://localhost:8100/api/ProductTaxes', value);
  }
  DeleteProductTaxes(id: number) {
    return this.http.delete('https://localhost:8100/api/ProductTaxes/' + id);
  }
  SearchStngByCode(Id: number, KeyCode: string): Observable<ProductTaxesVM[]> {
    return this.http.get<ProductTaxesVM[]>('https://localhost:8100/api/ProductTaxes/' + Id + "/" + KeyCode).pipe();
  }
  SearchItemwithVariants(Item: ProductwithVariantsVM): Observable<ProductwithVariantsVM[]> {
    return this.http.post<ProductwithVariantsVM[]>(Globals.BASE_API_URL + 'ProductTaxes/Product', Item).pipe();
  }
  GetAccountDetail(id: number): Observable<boolean> {
    debugger
    var subject = new Subject<boolean>();
    var acc = new SettingsVM
    acc.id = id
    acc.isActive = true
    this.catSvc.SearchSettings(acc).subscribe({
      next: (res: SettingsVM[]) => {
        if (id == SaleGrossAccounts.smallPkg) {
          var smallPkgGrossSale = this.selectedSales.saleLines
            .filter(line => line.itemType === ItemTypes.smallPkg)
            .reduce((sum, line) => sum + line.amount, 0);

          if (smallPkgGrossSale > 0) {
            var line = new VoucherDetailsVM
            line.acId = parseInt(res[0].value)
            line.credit = smallPkgGrossSale
            this.voucher.voucherDetails.push(line)
          }
        }
        else if (id == SaleGrossAccounts.largePkg) {
          var largePkgGrossSale = this.selectedSales.saleLines
            .filter(line => line.itemType === ItemTypes.largePkg)
            .reduce((sum, line) => sum + line.amount, 0);
          if (largePkgGrossSale > 0) {
            var line = new VoucherDetailsVM
            line.acId = parseInt(res[0].value)
            line.credit = largePkgGrossSale
            this.voucher.voucherDetails.push(line)
          }
        }
        else if (id == SalesAccounts.InvoiceDiscount) {
          if (this.selectedSales.discount > 0) {
            var line = new VoucherDetailsVM
            line.acId = parseInt(res[0].value)
            line.debit = this.selectedSales.discount
            this.voucher.voucherDetails.push(line)
          }
        }
        else if (id == SalesAccounts.SaleDiscount) {
          //SaleDiscount
          var disc = this.selectedSales.saleLines
            .reduce((sum, line) => sum + line.disc, 0);
          if (disc > 0) {
            var line = new VoucherDetailsVM
            line.acId = parseInt(res[0].value)
            line.debit = disc
            this.voucher.voucherDetails.push(line)
          }
        }
        else if (id == SalesAccounts.BulkDiscount) {
          //BulkDiscount
          var bulkDisc = this.selectedSales.saleLines
            .reduce((sum, line) => sum + line.bulkDisc, 0);
          if (bulkDisc > 0) {
            var line = new VoucherDetailsVM
            line.acId = parseInt(res[0].value)
            line.debit = bulkDisc
            this.voucher.voucherDetails.push(line)
          }
        }
        else if (id == SalesAccounts.QtyDiscount) {
          //QtyDiscount
          var qtyDisc = this.selectedSales.saleLines
            .reduce((sum, line) => sum + line.qtyDisc, 0);
          if (qtyDisc > 0) {
            var line = new VoucherDetailsVM
            line.acId = parseInt(res[0].value)
            line.debit = qtyDisc
            this.voucher.voucherDetails.push(line)
          }
        }
        else if (id == SalesAccounts.SaleGST) {
          //SaleGst
          var gst = this.selectedSales.saleLines
            .reduce((sum, line) => sum + line.gst, 0);
          if (gst > 0) {
            var line = new VoucherDetailsVM
            line.acId = parseInt(res[0].value)
            line.credit = gst
            this.voucher.voucherDetails.push(line)
          }
        }
        else if (id == SalesAccounts.SaleGSTRet) {
          //GstRet
          var gstRet = this.selectedSales.saleLines
            .reduce((sum, line) => sum + line.gstRet, 0);
          if (gstRet > 0) {
            var line = new VoucherDetailsVM
            line.acId = parseInt(res[0].value)
            line.credit = gstRet
            this.voucher.voucherDetails.push(line)
          }
        }
        else if (id == SalesAccounts.Withholding) {
          //wth
          var wth = this.selectedSales.saleLines
            .reduce((sum, line) => sum + line.wht, 0);
          if (wth > 0) {
            var line = new VoucherDetailsVM
            line.acId = parseInt(res[0].value)
            line.credit = wth
            this.voucher.voucherDetails.push(line)
          }
        }
        else if (id == SalesAccounts.FurtherTAX) {
          //fTax
          var fTax = this.selectedSales.saleLines
            .reduce((sum, line) => sum + line.fTax, 0);
          if (fTax > 0) {
            var line = new VoucherDetailsVM
            line.acId = parseInt(res[0].value)
            line.credit = fTax
            this.voucher.voucherDetails.push(line)
          }
        }
        else if (id == SalesAccounts.ChargesAdd) {
          //ChargAdd
          var chrgAdd = this.selectedSales.saleLines
            .reduce((sum, line) => sum + line.chrgsAdd, 0);
          if (chrgAdd > 0) {
            var line = new VoucherDetailsVM
            line.acId = parseInt(res[0].value)
            line.credit = chrgAdd
            this.voucher.voucherDetails.push(line)
          }
        }
        else if (id == SalesAccounts.ChargesLess) {
          //chrgLess
          var chrgLess = this.selectedSales.saleLines
            .reduce((sum, line) => sum + line.chrgsLess, 0);
          if (chrgLess > 0) {
            var line = new VoucherDetailsVM
            line.acId = parseInt(res[0].value)
            line.debit = chrgLess
            this.voucher.voucherDetails.push(line)
          }
        }
        else if (id == SalesAccounts.PackingCharges) {
          //pkgChrgs
          if (this.selectedSales.packChrgs > 0) {
            var line = new VoucherDetailsVM
            line.acId = parseInt(res[0].value)
            line.credit = this.selectedSales.packChrgs
            this.voucher.voucherDetails.push(line)
          }
        }
        else if (id == SalesAccounts.FreightCharges) {
          //freightChrgs
          if (this.selectedSales.freightChrgs > 0) {
            var line = new VoucherDetailsVM
            line.acId = parseInt(res[0].value)
            line.debit = this.selectedSales.freightChrgs
            this.voucher.voucherDetails.push(line)
          }
        }
        subject.next(true);
      }, error: () => {

      }
    })
    return subject.asObservable();
  }
  GetDebitTotal() {
    this.voucher.voucherDetails.forEach(element => {
      if (element.debit == undefined)
        element.debit = 0
    });
    return this.voucher.voucherDetails?.map(t => t.debit).reduce((acc, value) => acc + value, 0);
  }
  GetCreditTotal() {
    this.voucher.voucherDetails.forEach(element => {
      if (element.credit == undefined)
        element.credit = 0
    });
    return this.voucher.voucherDetails?.map(t => t.credit).reduce((acc, value) => acc + value, 0);
  }
  GetItemType(): Promise<void> {
    console.warn(898989)
    return new Promise<void>(async (resolve, reject) => {
      this.selectedSales.saleLines.forEach(element => {
        var product = new ItemVM
        product.id = element.productId
        this.SearchItem(product).subscribe({
          next: (res: ItemVM[]) => {
            element.itemType = res[0].typeId
          }, error: () => {
            ;
          }
        })
      });
      setTimeout(() => {
        console.log("Service method completed");
        resolve();
      }, 2000); // 2 seconds delay
    });
    //  return of(null).pipe(delay(1000));
  }
  async PostSalesToGL(sale: SalesVM): Promise<void> {
    return new Promise<void>(async (resolve, reject) => {
      this.selectedSales = sale
      await this.GetItemType()
      this.voucher.voucherDetails = []
      this.GetAccountDetail(SalesAccounts.InvoiceDiscount).subscribe({
        next: (res) => {
          if (res)
            this.GetAccountDetail(SalesAccounts.FreightCharges).subscribe({
              next: (res) => {
                if (res)
                  this.GetAccountDetail(SalesAccounts.SaleDiscount).subscribe({
                    next: (res) => {
                      if (res)
                        this.GetAccountDetail(SalesAccounts.BulkDiscount).subscribe({
                          next: (res) => {
                            if (res)
                              this.GetAccountDetail(SalesAccounts.QtyDiscount).subscribe({
                                next: (res) => {
                                  if (res)
                                    this.GetAccountDetail(SalesAccounts.SaleGST).subscribe({
                                      next: (res) => {
                                        if (res)
                                          this.GetAccountDetail(SalesAccounts.SaleGSTRet).subscribe({
                                            next: (res) => {
                                              if (res)
                                                this.GetAccountDetail(SalesAccounts.FurtherTAX).subscribe({
                                                  next: (res) => {
                                                    if (res) this.GetAccountDetail(SalesAccounts.Withholding).subscribe({
                                                      next: (res) => {
                                                        if (res)
                                                          this.GetAccountDetail(SalesAccounts.ChargesAdd).subscribe({
                                                            next: (res) => {
                                                              if (res) this.GetAccountDetail(SalesAccounts.ChargesLess).subscribe({
                                                                next: (res) => {
                                                                  if (res)
                                                                    this.GetAccountDetail(SalesAccounts.PackingCharges).subscribe({
                                                                      next: (res) => {
                                                                        if (res)
                                                                          this.GetAccountDetail(SaleGrossAccounts.smallPkg).subscribe({
                                                                            next: (res) => {
                                                                              if (res)
                                                                                this.GetAccountDetail(SaleGrossAccounts.largePkg).subscribe({
                                                                                  next: (res) => {
                                                                                    if (res) {
                                                                                      var totalCredit = this.GetCreditTotal()
                                                                                      var totalDebit = this.GetDebitTotal()
                                                                                      var custDebit = Math.abs(totalCredit - totalDebit)
                                                                                      var line = new VoucherDetailsVM
                                                                                      line.debit = custDebit
                                                                                      line.acId = this.selectedSales.acId
                                                                                      line.isDefaultDrCr = true
                                                                                      this.voucher.voucherDetails.push(line);
                                                                                      this.voucher.statusId = Statses.Draft
                                                                                      this.voucher.vchTypeKeyCode = "SALE"
                                                                                      this.voucher.vchTypeId = VchTypes.Sale
                                                                                      this.voucher.vchDate = new Date
                                                                                      this.voucher.isActive = true
                                                                                      this.SaveVoucher(this.voucher).subscribe({
                                                                                        next: () => {
                                                                                          //this.selectedSales.isPosted = true
                                                                                          //this.selectedSales.statusId = Statses.Posted
                                                                                          this.selectedSales.credit = this.GetCreditTotal()
                                                                                          this.selectedSales.debit = this.GetDebitTotal()
                                                                                          this.UpdateSale(this.selectedSales).subscribe({
                                                                                            next: () => {
                                                                                              console.warn("from svc")
                                                                                              this.catSvc.SuccessMsgBar("Successfully Posted in to GL", 6000)
                                                                                            },
                                                                                            error: () => {
                                                                                              this.catSvc.ErrorMsgBar("Error Occurred", 2000)
                                                                                            }
                                                                                          })
                                                                                        },
                                                                                        error: () => {
                                                                                          this.catSvc.ErrorMsgBar("Error Occurred", 2000)
                                                                                        }
                                                                                      })
                                                                                    }
                                                                                  }
                                                                                })
                                                                            }
                                                                          })
                                                                      }
                                                                    })
                                                                }
                                                              })
                                                            }
                                                          })
                                                      }
                                                    })
                                                  }
                                                })
                                            }
                                          })
                                      }
                                    })
                                }
                              })
                          }
                        })
                    }
                  })
              }
            })
        }
      })
      setTimeout(() => {
        console.log("Service method completed");
        resolve();
      }, 2000); // 2 seconds delay
    });
  }
  IsSupplierBeingUsed(supId: number): Observable<boolean> {
    var pur = new PurchaseVM
    pur.supplierId = supId
    var subject = new Subject<boolean>();
    this.SearchPurchase(pur)
      .subscribe({
        next: (value: PurchaseVM[]) => {
          if (value.length > 0)
            subject.next(true);
          else
            subject.next(false);
          return true
        }, error: (err) => {
          return false
        }
      })
    return subject.asObservable();
  }
  IsCustomerBeingUsed(custId: number): Observable<boolean> {
    var dc = new DCVM
    dc.custId = custId
    var subject = new Subject<boolean>();
    this.SearchDC(dc)
      .subscribe({
        next: (value: DCVM[]) => {
          if (value.length > 0)
            subject.next(true);
          else
            subject.next(false);
          return true
        }, error: (err) => {
          return false
        }
      })
    return subject.asObservable();
  }
  SendMailtoCustomer(sale: SalesVM, cust: CustomerVM) {
    debugger
    var tem = new NotificationTemplateVM
    tem.keyCode = "SaleInvoice"
    this.catSvc.SearchNotificationTemplate(tem).subscribe({
      next: (res: NotificationTemplateVM[]) => {
        var notification = new NotificationVM
        ///notification.mailBody = html
        res[0].body = res[0].body.replace('#table', this.prepareTemplateTable(sale))
        res[0].body = res[0].body.replaceAll('#invNo', sale.invNo)
        res[0].body = res[0].body.replaceAll('#saleDate', sale.date.toDateString())
        if (sale.gross != undefined)
          res[0].body = res[0].body.replaceAll('#gross', sale.gross.toString())
        else
          res[0].body = res[0].body.replaceAll('#gross', "0")
        if (sale.discount != undefined)
          res[0].body = res[0].body.replaceAll('#discount', sale.discount.toString())
        else
          res[0].body = res[0].body.replaceAll('#discount', "0")
        if (sale.gst != undefined)
          res[0].body = res[0].body.replaceAll('#gst', sale.gst.toString())
        else
          res[0].body = res[0].body.replaceAll('#gst', "0")
        res[0].body = res[0].body.replaceAll('#custAddress', cust.address)
        res[0].body = res[0].body.replaceAll('#custNo', cust.phone)
        res[0].body = res[0].body.replaceAll('#custMail', cust.email)
        res[0].body = res[0].body.replaceAll('#customer', sale.customer)
        notification.mailBody = res[0].body
        notification.senderMail = "bintameer212@gmail.com"
        notification.receiverMail = cust.email
        notification.mailSubject = res[0].subject
        this.catSvc.html = res[0].body
        this.catSvc.SendMail(notification).subscribe({
          next: (res) => {

          }, error: (err) => {
            console.warn(err)
            this.catSvc.ErrorMsgBar("Error Occurred while sending mail", 5000)
          }
        })
      }, error: (err) => {
        console.warn(err)
        this.catSvc.ErrorMsgBar("Template not found", 5000)
      }
    })
  }
  public prepareTemplateTable(sale: SalesVM) {
    let template = `<table style="width: 100%;border:2px solid rgb(189, 186, 186);">
<thead style=" background-color: rgb(189, 186, 186);">
  <tr>
    <th style="width:25%">Product</th>
    <th>Sale Rate</th>
    <th style="width:25%">Qty</th>
    <th style="width:25%">Amount</th>
  </tr>
</thead>`;
    sale.saleLines.forEach((x) => {
      template += `
 <tbody>
  <tr>
     <td style="width:25%; text-align:center">${x.product}</td>
     <td style="width:25%;text-align:center">${x.saleRate}</td>
     <td style="width:25%;text-align:center">${x.saleQty}</td>
     <td style="width:25%;text-align:center">${x.amount}</td>
   </tr>
  </tbody>
        `;
    });
    return template + `</table>`;
  }

  generatePdf(): Observable<any> {
    const url = `${Globals.BASE_API_URL}/Voucher/GetPDF`;
    return this.http.post(url, {
      responseType: 'blob' as 'json',
      observe: 'response'
    });
  }
  GetPdf(vch: VoucherVM): Observable<any> {
    return this.http.post<any>(Globals.SpPRING_BOOT_API_URL + "Voucher/Report", vch, {
      responseType: 'blob' as 'json',
      observe: 'response'
    });
  }


}


