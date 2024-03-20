import { BaseDomainVM } from "../../catalog/Models/BaseDomainVM"

export class TaskVM extends BaseDomainVM {

    user?: string
    priority?: string
    status?: string
    module?: string
    taskTitle?: string
    storyPoints: string
    statusId: number
    sp: number
    claimPercent?: string
    remainingSPs: number
    reason: string
    date: Date
    dayEndStatus: number
    ischecked: boolean
    isDisabled: boolean = false
    claimId: number = 0

}
