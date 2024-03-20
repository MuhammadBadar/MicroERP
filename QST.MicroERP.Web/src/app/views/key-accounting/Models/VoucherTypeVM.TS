export class VoucherTypeVM {
    id!: number
    name?: string
    defaultDrCrFirst?: boolean
    keyCode?: string
    defaultDrCrSecondId?: number = 0
    defaultDrCrSecond?: string
    clientId: number
    isActive: boolean
}