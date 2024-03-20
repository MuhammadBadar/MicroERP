import { PatientExtraFields } from './PatientExtraFields'
export class PatientVM {
    clientId: number
    id!: number
    patientId: number
    email:string
    patientName?: string
    dateOfBirth: Date
    gender?: string
    contactNo?: string
    houseNo?: string
    address?: string
    remarks?: string
    isActive?: boolean
    genderId: number
    cityId: number = 0
    areaId: number = 0
    city: string
    area: string
    age: string
    countryId: number = 0
    ptFData: PatientExtraFields[] = new Array()
}