export class UOMConversionVm {
    id!: number
    uomId: number = 0
    convertedUOMId: number = 0
    qty: number
    isBaseUnit: boolean
    multiplier: number
    displayUOM?: string
    uom?: string
    convertedUOM?: string
    isActive: boolean;
    salePrice: number
    purPrice: number;
    isChecked: boolean = false
}