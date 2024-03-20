import { VchTypes } from './../../Models/Enum/VchTypes';
import { UserVM } from './../../../security/models/user-vm';
import { SecurityService } from './../../../security/security.service';
import { SupplierVM } from './../../Models/SupplierVM';
import { Statses } from './../../Models/Enum/Status';
import { KeyAccountingService } from './../../key-accounting.service';
import { ItemsService } from './../../../items/items.service';
import { EnumTypes } from 'src/app/views/catalog/Models/EnumTypes';
import { VoucherVM, VoucherDetailsVM } from './../../Models/VoucherVM';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CatalogService } from 'src/app/views/catalog/catalog.service';
import { SettingsVM } from 'src/app/views/catalog/Models/SettingsVM';
import { MatTableDataSource } from '@angular/material/table';
import { NgForm } from '@angular/forms';
import { Location } from '@angular/common';
import Swal from 'sweetalert2';
import * as moment from 'moment';
import { ItemVM } from '../../Models/ItemVM';
import { MatCheckboxChange } from '@angular/material/checkbox';
import { VoucherTypeVM } from '../../Models/VoucherTypeVM';
import { ChangeDetectorRef } from '@angular/core';
import { Modules } from 'src/app/views/catalog/Models/Enums/Modules';
import { ManageVoucherTypeComponent } from '../../manage-voucher-type/manage-voucher-type.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-manage-voucher',
  templateUrl: './manage-voucher.component.html',
  styleUrls: ['./manage-voucher.component.css']
})
export class ManageVoucherComponent implements OnInit {
  defVal = "N/A"
  color = "red"
  even = "even"
  vchDetailId = 0
  addButton = true
  isDebitRequired: boolean = true
  isCreditRequired: boolean = true
  isInvNoRequired: boolean = false
  proccessing: boolean = false;
  lineAddMode: boolean = false
  lineEditMode: boolean = true
  Edit: boolean = false;
  Add: boolean = true;
  Godowns?: SettingsVM[]
  Salesmen?: UserVM[]
  Products?: ItemVM[]
  Accounts?: SettingsVM[]
  Vendors?: SupplierVM[]
  VoucherTypes?: VoucherTypeVM[]
  vchId!: number
  getVchById!: VoucherVM[];
  selectedVoucher: VoucherVM
  selectedVoucherDetail = new VoucherDetailsVM
  selectedDetail = new VoucherDetailsVM
  voucherDetails: VoucherDetailsVM[] = []
  selectedVoucherType?: VoucherTypeVM
  @ViewChild('voucherForm', { static: true }) voucherForm!: NgForm;
  @ViewChild('voucherDetailForm', { static: true }) voucherDetailForm!: NgForm;
  displayedColumns: string[] = ['acName', 'acId', 'debit', 'credit', 'descr', 'actions'];
  dataSource: any
  outputArray = [];
  @ViewChild("debit") debitField?: ElementRef;
  @ViewChild("credit") creditField?: ElementRef;
  constructor(
    private cdref: ChangeDetectorRef,
    public acntSvc: KeyAccountingService,
    private route: ActivatedRoute,
    private _location: Location,
    private dialog: MatDialog,
    public securitySvc: SecurityService,
    public catSvc: CatalogService,) {
    this.selectedVoucher = new VoucherVM();
    this.selectedVoucherDetail = new VoucherDetailsVM()

  }
  ngOnInit(): void {
    this.vchDetailId = 0
    this.voucherDetails = []
    this.route.queryParams.subscribe(params => {
      this.vchId = params['id'];
    });
    if (this.vchId > 0) {
      this.Edit = true;
      this.Add = false;
      this.GetVoucherById();
    }
    else {
      this.Add = true;
      this.Edit = false;
      this.dataSource = new MatTableDataSource(this.voucherDetails);
      this.arrangeVchLine();
    }
    this.lineAddMode = false;
    this.lineEditMode = false;
    this.isInvNoRequired = false;
    //this.GetSettings(EnumTypes.Godown)
    //this.GetSalesMen()
    //this.GetProducts()
    //this.GetSupplier()
    this.GetAccounts();
    this.GetVoucherTypes()
    this.selectedVoucher = new VoucherVM();
    this.selectedVoucherDetail = new VoucherDetailsVM()
    this.selectedVoucher.isActive = true
  }
  ngAfterContentChecked() {
    this.cdref.detectChanges();
  }
  GetVoucherTypes() {
    var vchType = new VoucherTypeVM
    vchType.clientId = +localStorage.getItem("ClientId")
    vchType.isActive = true
    this.acntSvc.SearchVoucherType(vchType).subscribe({
      next: (res: VoucherTypeVM[]) => {
        this.VoucherTypes = res;
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred!", 4000)
        console.warn(e);
      }
    })
  }
  GetAccounts() {
    var stng = new SettingsVM
    stng.levelId = EnumTypes.SubSidiary
    stng.moduleId = Modules.GL
    stng.clientId = +localStorage.getItem("ClientId")
    stng.isActive = true
    this.catSvc.SearchSettings(stng).subscribe({
      next: (res: SettingsVM[]) => {
        this.Accounts = res;
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred!", 4000)
        console.warn(e);
      }
    })
  }
  GetSupplier() {
    var sup = new SupplierVM
    sup.isActive = true
    this.acntSvc.SearchSupplier(sup).subscribe({
      next: (res: SupplierVM[]) => {
        this.Vendors = res;
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred!", 4000)
        console.warn(e);
      }
    })
  }
  GetProducts() {
    var item = new ItemVM
    item.isActive = true
    this.acntSvc.SearchItem(item).subscribe({
      next: (res: ItemVM[]) => {
        this.Products = res;
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred!", 4000)
        console.warn(e);
      }
    })
  }
  GetSalesMen() {
    var user = new UserVM
    user.role = "SalesMan"
    user.isActive = true
    this.securitySvc.SearchUser(user).subscribe({
      next: (value: UserVM[]) => {
        this.Salesmen = value
      }, error: (err) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 4000)
      },
    })
  }
  GetSettings(etype: EnumTypes) {
    var setting = new SettingsVM()
    setting.enumTypeId = etype
    setting.isActive = true
    this.catSvc.SearchSettings(setting).subscribe({
      next: (res: SettingsVM[]) => {
        if (etype == EnumTypes.Godown)
          this.Godowns = res;
      }, error: (e: any) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  arrangeVchLine() {
    const defaultEntry = this.dataSource.data.find(entry => entry.isDefaultDrCr == true);
    if (defaultEntry) {
      this.dataSource.data = this.dataSource.data.filter(entry => entry.isDefaultDrCr !== true);
      this.dataSource.data.push(defaultEntry);
    }
  }
  GetVoucherById() {
    var vch = new VoucherVM
    vch.clientId = +localStorage.getItem("ClientId")
    vch.id = this.vchId
    this.acntSvc.SearchVoucher(vch).subscribe({
      next: (res: VoucherVM[]) => {
        debugger
        this.getVchById = res;
        this.selectedVoucher = this.getVchById[0]
        this.SerachVourcherType(this.selectedVoucher.vchTypeId)
        this.voucherDetails = []
        this.selectedVoucher.voucherDetails?.forEach(element => {
          this.voucherDetails.push(element)
        });
        this.dataSource = new MatTableDataSource(this.voucherDetails);
        this.arrangeVchLine();
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred !", 6000)
        console.warn(e);
      }
    })
  }
  SerachVourcherType(vchTypeId: number) {
    var type = new VoucherTypeVM
    type.id = vchTypeId
    type.clientId = +localStorage.getItem("ClientId")
    this.acntSvc.SearchVoucherType(type).subscribe({
      next: (value: VoucherTypeVM[]) => {
        this.selectedVoucherType = value[0]
      }, error: (err) => {
        this.catSvc.ErrorMsgBar("Error Occurred !", 6000)
      },
    })
  }
  async Submit() {
    this.SetDates()
    this.OnSalesmanSelect();
    this.selectedVoucher.voucherDetails = this.voucherDetails
    this.proccessing = true
    this.VoucherValidation();
    if (this.selectedVoucher.voucherDetails.length == 0)
      this.catSvc.ErrorMsgBar("Please Add some Voucher Detail!", 5000)
    else {
      debugger
      let sumOfDebit: any = 0
      let sumOfCredit: any = 0
      this.selectedVoucher.voucherDetails.forEach(element => {
        if (element.credit != undefined)
          sumOfCredit += element.credit
      });
      this.selectedVoucher.voucherDetails.forEach(element => {
        if (element.debit != undefined)
          sumOfDebit += element.debit
      });
      if (sumOfCredit != sumOfDebit)
        this.catSvc.ErrorMsgBar("Unable to continue because debit and credit are not equal!", 5000)
      else {
        if (!this.voucherForm.invalid) {
          this.selectedVoucher.statusId = Statses.UnPosted
          this.selectedVoucher.clientId = +localStorage.getItem("ClientId")
          //  if (this.Edit)
          await this.UpdateVoucher();
          // else
          //   this.SaveVoucher()
        } else {
          this.catSvc.ErrorMsgBar("Please Fill all required fields!", 5000)
          this.proccessing = false
        }
      }
    }
    this.proccessing = false
  }
  SaveVoucher() {
    this.selectedVoucher.clientId = +localStorage.getItem("ClientId")
    this.selectedVoucher.voucherDetails[0].isDefaultDrCr = true
    this.acntSvc.SaveVoucher(this.selectedVoucher).subscribe({
      next: (res: VoucherVM) => {
        this.catSvc.SuccessMsgBar(" Successfully Added!", 5000)
        this.selectedVoucher = res
        this.voucherDetails = []
        this.selectedVoucher.voucherDetails?.forEach(element => {
          this.voucherDetails.push(element)
        });

        this.dataSource = new MatTableDataSource(res.voucherDetails);
        this.arrangeVchLine();
        this.RefreshDetail()
        // this.Add = true;
        // this.Edit = false;
        // this.proccessing = false
        // this.ngOnInit();
      }, error: (e: any) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
        this.voucherDetails = []
        this.proccessing = false
      }
    })
  }
  UpdateVoucher() {
    this.acntSvc.UpdateVoucher(this.selectedVoucher).subscribe({
      next: (res: VoucherVM) => {
        if (this.Edit) {
          if (this.lineEditMode)
            this.catSvc.SuccessMsgBar(" Successfully Updated!", 5000)
          else if (this.lineAddMode)
            this.catSvc.SuccessMsgBar(" Successfully Added!", 5000)
          else
            this.catSvc.SuccessMsgBar(" Successfully Updated!", 5000)
        } else {
          if (this.lineEditMode)
            this.catSvc.SuccessMsgBar(" Successfully Updated!", 5000)
          else
            this.catSvc.SuccessMsgBar(" Successfully Added!", 5000)
        }
        this.selectedVoucher = res
        this.voucherDetails = []
        this.selectedVoucher.voucherDetails?.forEach(element => {
          this.voucherDetails.push(element)
        });
        this.dataSource = new MatTableDataSource(res.voucherDetails);
        this.arrangeVchLine();
        this.RefreshDetail()
        this.proccessing = false
        if (!this.Edit && res.statusId == Statses.UnPosted)
          this.ngOnInit()
      }, error: (e: any) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
        this.voucherDetails = []
        this.proccessing = false
      }
    })
  }
  edit(vchDet: VoucherDetailsVM) {
    //document.getElementById("content").scrollIntoView();
    this.lineEditMode = true
    this.lineAddMode = false
    this.addButton = false
    this.selectedVoucherDetail = vchDet
    this.selectedDetail = vchDet
    this.selectedVoucherDetail.editMode = true
    var event: any;
    this.TextChangeEvent(event);
  }
  delete(vchDet: VoucherDetailsVM) {
    if (this.voucherDetails.length == 1) {
      this.catSvc.ErrorMsgBar("You Can't delete it,as this Voucher has only one line ,and the Voucher Detail Can't be Empty", 9000)
    } else {
      if (this.selectedVoucher.statusId == Statses.Posted) {
        this.catSvc.ErrorMsgBar("Can't Delete a posted Voucher Line", 6000)
      } else {
        Swal.fire({
          title: 'Are you sure?',
          text: "You won't be able to revert this!",
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
          if (result.value) {
            if (vchDet.id == undefined || vchDet.id == 0) {
              Swal.fire(
                'Deleted!',
                'Successfully Deleted.',
                'success'
              )
            } else {
              var line = this.voucherDetails.find(x => x.isDefaultDrCr == true)
              debugger
              if (this.selectedVoucherType?.defaultDrCrFirst == true) {
                if (vchDet.debit != 0 && vchDet.credit == 0)
                  line.credit = Math.max(0, line.credit - vchDet.debit)
                else if (vchDet.credit != 0 && vchDet.debit == 0)
                  line.credit = line.credit + vchDet.credit
              }
              else if (this.selectedVoucherType?.defaultDrCrFirst == false) {
                if (vchDet.credit != 0 && vchDet.debit == 0)
                  line.debit = Math.max(0, line.debit - vchDet.credit)
                else if (vchDet.credit == 0 && vchDet.debit != 0)
                  line.debit = line.debit + vchDet.debit
              }
              line.dBoperation = 2
              this.voucherDetails.forEach(element => {
                if (element.isDefaultDrCr == true)
                  element = line
              });
              var voucher = new VoucherVM
              voucher = this.selectedVoucher
              voucher.voucherDetails = []
              voucher.voucherDetails.push(line)
              voucher.voucherDetails.push(vchDet)
              vchDet.dBoperation = 3
              this.acntSvc.UpdateVoucher(voucher).subscribe({
                next: (data: VoucherVM) => {
                  Swal.fire(
                    'Deleted!',
                    'Successfully Deleted.',
                    'success'
                  )
                  this.voucherDetails = []
                  data.voucherDetails?.forEach(element => {
                    this.voucherDetails.push(element)
                  });
                  this.dataSource = new MatTableDataSource(data.voucherDetails);
                  this.arrangeVchLine();
                }, error: (e) => {
                  this.catSvc.ErrorMsgBar("Error Occurred!", 4000)
                  console.warn(e);
                }
              })
            }
            // this.voucherDetails = this.voucherDetails.filter(x => x != vchDet)
            // this.dataSource = new MatTableDataSource(this.voucherDetails);
          }
        })
      }
    }
  }
  onSelectProduct(produuct: ItemVM) {
    this.selectedVoucherDetail.product = produuct.name
  }
  onAccountSelect(account: SettingsVM) {
    debugger
    this.selectedVoucherDetail.acName = account.name

    if (this.selectedVoucherType?.defaultDrCrFirst == true) {
      document.getElementById("debit")?.focus();
    } else if (this.selectedVoucherType?.defaultDrCrFirst == false) {
      document.getElementById("credit")?.focus();
    }
  }
  async AddVoucherDetailtoList() {
    this.onBlurDrCr()
    this.VoucherValidation();
    if (!this.voucherForm.invalid) {
      if (this.selectedVoucherDetail.acId == 0 || this.selectedVoucherDetail.acId == undefined)
        this.voucherDetailForm.form.controls['acId'].setErrors({ 'incorrect': true });
      if (!this.voucherDetailForm.invalid) {
        if (this.selectedVoucherDetail.credit == 0 && this.selectedVoucherDetail.debit == 0)
          this.catSvc.ErrorMsgBar("Debit and Credit both can't be 0 at a time", 5000)
        else {
          if (this.selectedVoucher.statusId == Statses.Posted) {
            this.catSvc.ErrorMsgBar("Can't Add and Update posted Voucher Line", 6000)
            this.RefreshDetail()
          }
          else {
            if (this.lineEditMode)
              this.selectedVoucherDetail.dBoperation = 2
            else
              this.selectedVoucherDetail.dBoperation = 1
            if (this.selectedVoucherDetail.dBoperation == 1) {
              this.selectedVoucher.statusId = Statses.Draft
              this.lineAddMode = true
            }

            this.voucherDetails.push(this.selectedVoucherDetail)

            this.selectedVoucher.voucherDetails = []
            var line = this.voucherDetails.find(x => x.isDefaultDrCr == true)
            line.dBoperation = 2
            this.selectedVoucher.voucherDetails?.push(line)
            this.selectedVoucher.voucherDetails?.push(this.selectedVoucherDetail)
            this.selectedVoucher.clientId = +localStorage.getItem("ClientId")
            if (this.selectedVoucher?.id > 0)
              await this.UpdateVoucher();
            else {
              this.selectedVoucher.voucherDetails[0].isDefaultDrCr = true
              await this.SaveVoucher();
            }
          }
        }
      }
      else {
        this.catSvc.ErrorMsgBar("Please fill all required fields of Voucher Line", 5000)
      }
    } else {
      this.catSvc.ErrorMsgBar("Please fill all required fields of Voucher", 5000)
    }
  }
  RefreshDetail() {
    debugger
    this.lineAddMode = false;
    this.addButton = true
    this.lineEditMode = false;
    this.isCreditRequired = true
    this.isDebitRequired = true
    this.voucherDetailForm.controls['debit'].enable();
    this.voucherDetailForm.controls['credit'].enable();
    this.selectedVoucherDetail = new VoucherDetailsVM
    if (this.vchId > 0)
      this.GetVoucherById()
  }
  TextChangeEvent(event: any) {
    if (this.selectedVoucherDetail.credit != 0 && this.selectedVoucherDetail.credit != undefined) {
      this.voucherDetailForm.controls['debit'].disable();
      this.isDebitRequired = false
      this.selectedVoucherDetail.debit = undefined
    } else {
      this.voucherDetailForm.controls['debit'].enable();
      this.isDebitRequired = true
    }
    if (this.selectedVoucherDetail.debit != 0 && this.selectedVoucherDetail.debit != undefined) {
      this.voucherDetailForm.controls['credit'].disable();
      this.isCreditRequired = false
      this.selectedVoucherDetail.credit = undefined
    } else {
      this.voucherDetailForm.controls['credit'].enable();
      this.isCreditRequired = true
    }
  }
  onBlurDrCr() {
    debugger
    var line = this.voucherDetails.find(x => x.isDefaultDrCr == true)
    if (this.selectedVoucherType?.defaultDrCrFirst == true) {
      if (this.selectedVoucherDetail.credit != undefined && this.selectedVoucherDetail.credit != 0) {
        //line.credit = Math.max(0, line.credit - this.selectedVoucherDetail.credit)
        if (this.lineEditMode)
          line.credit = Math.max(0, this.GetDebitTotal() - this.GetCreditTotal() + line.credit)
        else
          line.credit = Math.max(0, line.credit - this.selectedVoucherDetail.credit)
      } else {
        var sumOfDebit = this.GetDebitTotal()
        if (this.selectedVoucherDetail.debit != undefined) {
          if (this.lineEditMode)
            sumOfDebit -= this.selectedDetail.debit

          sumOfDebit += this.selectedVoucherDetail.debit
          line.credit = Math.max(0, (sumOfDebit - this.GetCreditTotal()) + line.credit)
        }
      }


    } else if (this.selectedVoucherType?.defaultDrCrFirst == false) {
      if (this.selectedVoucherDetail.debit != undefined && this.selectedVoucherDetail.debit != 0) {
        if (this.lineEditMode)
          line.debit = Math.max(0, this.GetCreditTotal() - this.GetDebitTotal() + line.debit)
        else
          line.debit = Math.max(0, line.debit - this.selectedVoucherDetail.debit)
      } else {
        var sumOfCredit = this.GetCreditTotal()
        if (this.selectedVoucherDetail.credit != undefined) {
          if (this.lineEditMode)
            sumOfCredit -= this.selectedDetail.credit

          sumOfCredit += this.selectedVoucherDetail.credit
          line.debit = Math.max(0, (sumOfCredit - this.GetDebitTotal()) + line.debit)
        }
      }
    }
    this.voucherDetails.forEach(element => {
      if (element.isDefaultDrCr == true)
        element = line
    });
  }
  OnSalesmanSelect() {
    debugger
    if (this.selectedVoucher.salesmanId != "N/A" && this.selectedVoucher.salesmanId != undefined && this.selectedVoucher.salesmanId != null) {
      this.isInvNoRequired = true
      if (this.selectedVoucher.invNo == undefined && this.selectedVoucher.invNo == null)
        this.voucherForm.form.controls['invNo'].setErrors({ 'incorrect': true });
    }
    else
      this.isInvNoRequired = false
  }
  Back() {
    this._location.back();
  }
  VoucherValidation() {
    if (this.selectedVoucher.vchTypeId == 0 || this.selectedVoucher.vchTypeId == undefined)
      this.voucherForm.form.controls['vchTypeId'].setErrors({ 'incorrect': true });
  }
  onBlur() {
    if (this.selectedVoucher.id > 0) {
      this.proccessing = true
      this.VoucherValidation()
      if (!this.voucherForm.invalid) {
        this.SetDates()
        this.acntSvc.UpdateVoucher(this.selectedVoucher).subscribe({
          next: (res: VoucherVM) => {
            this.catSvc.SuccessMsgBar(" Successfully Updated!", 5000)
            this.proccessing = false
          }, error: (e: any) => {
            this.catSvc.ErrorMsgBar("Error Occurred", 5000)
            console.warn(e);
            this.proccessing = false
          }
        })
      } else {
        this.catSvc.ErrorMsgBar("Please fill all required fields", 5000)
      }
    }
  }
  SetDates() {
    if (this.selectedVoucher.docDate != null && this.selectedVoucher.docDate != undefined) {
      this.selectedVoucher.docDate = moment(this.selectedVoucher.docDate).toDate()
      this.selectedVoucher.docDate = new Date(Date.UTC(this.selectedVoucher.docDate.getFullYear(), this.selectedVoucher.docDate.getMonth(), this.selectedVoucher.docDate.getDate()))
    }
    this.selectedVoucher.vchDate = moment(this.selectedVoucher.vchDate).toDate()
    this.selectedVoucher.vchDate = new Date(Date.UTC(this.selectedVoucher.vchDate.getFullYear(), this.selectedVoucher.vchDate.getMonth(), this.selectedVoucher.vchDate.getDate()))

  }
  GetDebitTotal() {
    this.voucherDetails.forEach(element => {
      if (element.debit == undefined)
        element.debit = 0
    });
    return this.voucherDetails?.map(t => t.debit).reduce((acc, value) => acc + value, 0);
  }
  GetCreditTotal() {
    this.voucherDetails.forEach(element => {
      if (element.credit == undefined)
        element.credit = 0
    });
    return this.voucherDetails?.map(t => t.credit).reduce((acc, value) => acc + value, 0);
  }
  GetQtyTotal() {
    return this.voucherDetails?.map(t => t.qty).reduce((acc, value) => acc + value, 0);
  }
  OnSelectVchType(val: VoucherTypeVM) {
    this.selectedVoucherType = val;
    this.selectedVoucher.vchTypeKeyCode = val.keyCode
    let vchDetail = new VoucherDetailsVM
    vchDetail.acId = val.defaultDrCrSecondId
    vchDetail.dBoperation = 1

    this.selectedVoucher.statusId = Statses.Draft
    this.selectedVoucher.voucherDetails = []
    this.selectedVoucher.voucherDetails?.push(vchDetail)
    this.SaveVoucher();

  }
  IsPostedCheck(event: MatCheckboxChange): void {
    debugger
    Swal.fire({
      title: 'Are you sure?',
      text: "You want to post  this voucher",
      icon: 'info',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, post it!'
    }).then((result) => {
      if (result.value) {
        this.selectedVoucher.isPosted = true
        this.selectedVoucher.statusId = Statses.Posted
        Swal.fire(
          'Successfully Posted.',
          'success'
        )
        this.onBlur()
      } else {
        this.selectedVoucher.isPosted = false
      }
    })
  }
  VchTypeDialog() {
    var dialogRef = this.dialog.open(ManageVoucherTypeComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '1000px', height: '650'
      , data: { isDialog: true }
    });
    dialogRef.afterClosed()
      .subscribe((res) => {
        this.GetVoucherTypes()
        this.GetAccounts()
      });
  }
}
