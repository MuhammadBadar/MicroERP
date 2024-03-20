

export class VoucherDetailsVM {
    id!: number
    billId!: number
    description?: string
    product?: string
    debit?: any;
    credit?: any;
    qty: number = 1;
    productId?: number = 0;
    acId?: number = 0;
    acName?: string
    rate?: number;
    isActive: boolean = true;
    dBoperation?: number;
    vchId?: number;
    addMode: boolean = false
    editMode: boolean = false
    isDefaultDrCr: boolean
    clientId: number
}
export class VoucherVM {
    id!: number;
    vchTypeKeyCode?: string;
    vchDate: Date = new Date;
    vchTypeId: number = 0;
    vchNo?: string;
    invNo?: string;
    docNo?: string;
    salesman?: string;
    godown?: string;
    vendor?: string;
    vchType?: string
    salesmanId: string = "N/A";
    godownId?: number = 0;
    vendorId?: number = 0;
    statusId?: number;
    isActive: boolean;
    docDate!: Date;
    qty: number = 1;
    description?: string
    isPosted: boolean = false;
    approvedById?: number;
    status?: string
    voucherDetails?: VoucherDetailsVM[] = new Array();
    isEdit = false;
    isView = false;
    clientId: number
}
