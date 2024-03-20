export class DoctorVM {
    id!: number
    clientId: number
    userId: string
    user: string
    defApptDur: number
    doctorName?: string
    dateOfBirth?: Date
    gender?: string
    genderId: number
    contactNo?: string
    houseNo?: string
    address?: string
    specialization?: string
    isActive?: boolean
    cityId: number = 0
    areaId: number = 0
    countryId: number = 0
    city: string
    area: string
    startTime: string
}