

export class DCDetailsVM {
    id!: number
    description?: string
    product?: string
    qty: number = 1;
    productId?: number = 0;
    isActive: boolean = true;
    dBoperation?: number;
    dcId?: number;
    addMode: boolean = false
    editMode: boolean = false
}
export class DCVM {
    id!: number;
    date: Date = new Date;
    invNo?: string;
    custId?: number = 0;
    acId?: number = 0;
    acName?: string
    customer?: string
    isActive: boolean;
    dcDetails?: DCDetailsVM[] = new Array();
}
