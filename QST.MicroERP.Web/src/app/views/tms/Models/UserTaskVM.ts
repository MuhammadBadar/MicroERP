import { BaseDomainVM } from "../../catalog/Models/BaseDomainVM"

export class UserTaskVM extends BaseDomainVM {
    userId?: string
    user?: string
    taskId: number = 0
    title?: string
    date?: Date
    parent?: string
    claimId: number = 0
    claim?: string
    statusId: number
    sp: number
    comments?: string
    reviewedBy?: string
    reviewComments?: string
    selectedTask: boolean = false;
    isDayEnded: boolean
    isChecked?: boolean;
    dayStartTime: Date
    dayEndTime: Date
    approvedClaim: number
    claimPercent: number
    approvedClaimId: number
    claimError: string
    remainingSPs
    lastClaim: string
    lastClaimId: number
    taskScore: string
    taskComPer: string
    workTime: number
    workSP: number
    claimWorkTime: string
    isEarlyFinshed: boolean
    extraTime: number
    isLastExistence: boolean
    priority: string
    isOverdue: boolean
    stalledReason: string
    status: string
    reason: string
}