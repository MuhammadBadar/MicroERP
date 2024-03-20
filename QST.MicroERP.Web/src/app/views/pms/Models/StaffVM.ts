export class StaffVM {
    id!: number
    clientId: number
    userId: string
    user: string
    name?: string
    dateOfBirth?: Date
    gender?: string
    genderId: number
    contactNo?: string
    houseNo?: string
    address?: string
    isActive?: boolean
    cityId: number = 0
    areaId: number = 0
    countryId: number = 0
    city: string
    area: string
}