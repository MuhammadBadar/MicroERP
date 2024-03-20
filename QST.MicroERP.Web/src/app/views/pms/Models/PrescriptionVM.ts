import { RxMedExtraFields } from './RxMedExtraFields'
export class PrescriptionVM {
    patientDOB: Date
    patientContact: string
    patientEmail: string
    patientCity: string
    doctorContact: string
    patientAge: string
    id!: number;
    clientId: number
    client: string
    doctorId: number = 0
    doctor: string
    patientId: number = 0
    patient: string
    date: Date = new Date
    time: string
    amount: number
    tokenNo: number
    tokenId: number
    tokenDate: number
    remarks: string
    medDetRemarks: string
    bp: string
    bpStatusId: number = 0
    isSugarPatient: boolean
    nextVisitNo: number
    weight: string
    temperature: string
    isActive: boolean;
    rxMedicines?: RxMedicineVM[] = []
    rxmefData: RxMedExtraFields[] = []
    reports: ReportsVM[] = []
    from?: Date
    to?: Date
    nextVisitDate: Date
    comments: string
    precautionIds: string
    precautions: string
    precauts: number[] = []
    doctorSpecialization: string
}
export class RxMedicineVM {
    id: number
    clientId: number
    client: string
    rxId: number
    medId: number = 0
    mrId: number = 0
    remarks: string
    remarksId: number = 0
    mr: string
    medicine: string
    noonQty: number
    eveQty: number
    amQty: number
    days: number
    isActive: boolean = true;
    dBoperation?: number;
    addMode: boolean
    editMode: boolean
}
export class ReportsVM {
    id: number
    rxId: number
    clientId: number
    client: string
    category: string
    categoryId: number = 0
    reportBase64Path: string
    date: Date = new Date
    name: string
    dBoperation: number
    isActive: boolean = true;
}