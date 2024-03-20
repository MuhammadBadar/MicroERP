import { AppStatus } from 'src/app/views/catalog/Models/Enums/AppStatus';
export class AppointmentVM {
    id!: number
    tokenId: number
    tokenDate: number
    tokenNo: number
    clientId: number
    patientId: number = 0
    patientName?: string
    doctorId: number = 0
    doctor: string
    date?: Date = new Date
    time?: string
    age!: string
    ageTooltip: string
    dob: Date
    genderId: number
    gender?: string
    shortAge: number
    statusId: number
    status: string
    isActive: boolean = true
    searchByDate: boolean
    from?: Date
    to?: Date
    apptDate?: Date
    adjacentType: string
}