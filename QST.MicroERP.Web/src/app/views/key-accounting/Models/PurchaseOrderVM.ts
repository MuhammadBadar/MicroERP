

export class PurchaseLineVM {
    id!: number
    prchId?: number;
    productId?: number = 0;
    product?: string
    description?: string
    purchaseRate!: number
    qty: number = 1;
    discPer!: number
    gstRate!: any
    gstRetailRate!: any
    retailRate!: number
    amount!: number
    isActive: boolean = true;
    dBoperation?: number;
    addMode: boolean = false
    editMode: boolean = false
    itemVariantId: number
    productAttribIds: string
    purUnitId: number = 0
    purUnit?: string
}
export class PurchaseVM {
    id!: number;
    date: Date = new Date;
    invNo?: string;
    supplierId?: number = 0;
    supplier?: string
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
    purchaseLines?: PurchaseLineVM[] = new Array();

}
