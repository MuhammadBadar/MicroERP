import { BaseDomainVM } from "../../catalog/Models/BaseDomainVM"
import { UserTaskVM } from "../../tms/Models/UserTaskVM"

export class AttendanceVM extends BaseDomainVM {
    userId?: string = ''
    inTime?: Date
    outTime?: Date
    date?: Date = new Date
    // View Properties
    user?: string
    workedHours?: string
    schTime?: string
    userName?: string
    day?: Date
    workedTime?: string
    inandOutTime?: string
    late: string
    targets?: string
    claimedSPs?: number
    claimPer?: string
    overTime?: string
    missingTime?: string
    storyPoints?: string
    dayEndStatus?: string
    dueSPs?: number
    sPsGap?: number
    fromDate: Date = new Date
    toDate: Date = new Date
    dayStartandEnd: string
    claimErrorPer: string
    finalScore: string
    userTasks: UserTaskVM[] = new Array()
}
