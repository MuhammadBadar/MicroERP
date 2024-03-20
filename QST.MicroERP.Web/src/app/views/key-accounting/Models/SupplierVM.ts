export class SupplierVM {
    id?: number
    accId: number = 0
    account?: string
    companyName!: string
    contactName!: string
    address!: string
    cityId: number = 0
    countryId: number = 0
    city!: string
    country!: string
    phone!: string
    discRate!: number
    customerId!: number
    isCustomer!: boolean
    isActive!: boolean
}