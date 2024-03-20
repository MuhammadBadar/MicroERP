import { BaseDomainVM } from "../../catalog/Models/BaseDomainVM";
import { Priorities } from './Enums/Priorities';
export class TaskVM extends BaseDomainVM {
    directSupervisorEmail: string;
    directSupervisorId: string;
    directSupervisorName: string;
    directSupervisorPhoneNumber: string;
    userPhoneNumber: string;
    userMail: string;
    user: string;
    module: string;
    status: string;
    priority: string;
    priorityId: number;
    title: string;
    sp: number;
    spStr: string
    description: string;
    override isActive: boolean = true;
    modifiedOn: Date = new Date()
    userId: string;
    moduleId: number = 0
    statusId: number
    freeSearch: string
    attachments?: AttachmentsVM[] = new Array()
    storyPoints: string
    claimPercent?: string
    remainingSPs: number
    reason: string
    date: Date
    dayEndStatus: number
    ischecked: boolean
    isDisabled: boolean = false
    claimId: number = 0
    extraTime: number
    isEarlyFinshed: boolean
    isOverDue: boolean
    totalWorkedTime: number
    fromDate: Date = new Date
    toDate: Date = new Date
}
export class AttachmentsVM {
    // time:string;
    id: number;
    dBoperation: number;
    taskId: number;
    docPath: string;
    name: string;
    base64File: Text;
    size: string;
    isActive: boolean = true;
    createdById: number = 0
    createdOn: Date = new Date()
    modifiedById: number
    modifiedOn: Date = new Date()
    // createdOn: Date=new Date()


}
