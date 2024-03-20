import { BaseDomainVM } from "../../catalog/Models/BaseDomainVM"

export class TaskCommentVM extends BaseDomainVM {
    taskId: number
    taskTitle: string
    user: string
    userId: string
    comment: string
    datetime: Date = new Date()
    time: Date
    override isActive: boolean = true;
    modifiedOn: Date = new Date()
}