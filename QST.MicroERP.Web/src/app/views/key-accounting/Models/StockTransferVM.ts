

export class STLineVM {
    id!: number
    godownId?: number = 0;
    godown?: string
    description?: string
    productId?: number = 0;
    product?: string
    qty: number = 1;
    productUnits!: number
    isActive: boolean = true;
    dBoperation?: number;
    sTId?: number;
    addMode: boolean = false
    editMode: boolean = false
}
export class StockTransferVM {
    id!: number;
    date: Date = new Date;
    invNo?: string;
    transferTo?: number = 0;
    transferFrom?: number = 0;
    from?: string
    to?: string
    isActive: boolean;
    stockTransferLines?: STLineVM[] = new Array();
}
