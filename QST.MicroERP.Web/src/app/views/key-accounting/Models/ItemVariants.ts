export class ItemVariantsVM {
    id!: number
    isActive: boolean = true
    itemId!: number
    attributeValuesIds?: string
    possibleValues?: string
    saleExtraRate?: number
    purchaseExtraRate?: number
    barCode?: string
    stockValue?: number
    dboperation?: number
    item?: string

}