import { AttributesVm } from "./AttributesVm";
import { ProductTaxesVM } from "./ProductTaxesVM";
import { ItemUOMVm } from "./ItemUOMVm";
import { ItemVariantsVM } from "./ItemVariants";

export class ItemVM {
    clientId: number
    moduleId: number
    id!: number;
    typeId: number = 0
    itemType: string
    vendorId: number = 0
    name?: string;
    saleRate!: number
    purRate!: number
    conversion!: number
    gstSaleRate!: number
    gstPurRate!: any
    saleStRate!: number
    purStRate!: any
    purUnits?: string
    saleUnits?: string
    retailRate?: number
    extraRate!: number
    prMazdoori!: number
    unitPrice?: number;
    unitsInStock?: number
    isActive: boolean
    manufacturer: string
    manufacturersId?: number = 0
    formula?: string
    category: string
    categoryId?: number = 0
    remarks?: string
    attributes?: AttributesVm[] = new Array();
    productAttribs?: ProductAttribVm[] = new Array();
    variants?: ItemVariantsVM[] = new Array();
    saleUnitIds: number[] = []
    purUnitIds: number[] = []
    purPossibleUnits?: string
    salePossibleUnits?: string
    itemUOMList: ItemUOMVm[] = new Array()
    productTaxes?: ProductTaxesVM[] = new Array()
}
export class ProductAttribVm {
    id!: number
    productId?: number = 0;
    product?: string
    attributeValue?: string
    attribute?: string
    attribId: number = 0
    attribValId: number;
    price: number
    purRate: number
    saleRate: number
    attribValues?: number[];
    isActive: boolean = true;
    dBoperation?: number;
    addMode: boolean = false
    editMode: boolean = false
    description: string
    isChecked: boolean
}
export class ProductwithVariantsVM {
    id: number
    prodName: string
    productTaxes?: ProductTaxesVM[] = new Array()
    isVariant: boolean
}
