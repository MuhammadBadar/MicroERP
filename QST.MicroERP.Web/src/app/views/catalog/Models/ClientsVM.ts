export class ClientsVM {
    id!: number
    clientName?: string
    category?: string
    address?: string
    city?: string
    contact?: string
    owner?: string
    relevantPerson?: string
    isActive?: boolean
    cityId: number = 0
    categoryId: number = 0
    moduleIds: string
    modules: string
    moduleIdList: number[] = []
    isChecked: boolean
    countryId: number = 0
    country?: string
    userId: string
    user: string
    email?: string
}