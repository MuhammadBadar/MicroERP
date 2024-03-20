export class SalesLineVM {
    itemType: number
    id!: number
    prchId?: number;
    productId?: number = 0;
    product?: string
    description?: string
    saleRate!: number
    retQty: number;
    saleQty: number = 1;
    issueQty: number;
    discRate!: number
    gstRate!: any
    gstRetailRate!: any
    retailRate!: number
    amount!: number
    isActive: boolean = true;
    dBoperation?: number;
    addMode: boolean = false
    editMode: boolean = false
    productAttribIds: string
    itemVariantId: number
    saleUnitId: number = 0
    saleUnit?: string
    fTaxRate!: number
    fTax!: number
    whtRate!: number
    wht!: number
    gst!: number
    disc!: number
    qtyDisc!: number
    bulkDisc!: number
    chrgsLess!: number
    chrgsAdd!: number
    gstRet!: number
}
export class SalesVM {
    id!: number;
    custId?: number = 0
    customer: string
    date: Date = new Date;
    invNo?: string;
    supplierId?: number = 0;
    supplier?: string
    salesmanId?: number = 0;
    salesman?: string
    acId?: number = 0;
    accName?: string
    gross?: number
    discount?: number
    gst?: number
    debit?: number
    credit?: number
    description?: string
    isActive: boolean;
    statusId?: number;
    isPosted: boolean = false;
    status?: string
    packChrgs?: number
    freightChrgs?: number
    netPayable?: number
    saleLines?: SalesLineVM[] = new Array();
}
