import { BaseDomainVM } from "../../catalog/Models/BaseDomainVM";

export class ScheduleDayEventVM extends BaseDomainVM {
    schId?: number;
    SchDayId: number
    day?: string
    scheduleTypeId: number = 0;
    scheduleType?: string
    startTime?: string
    endTime?: string
    locationId: number = 0
    location?: string
    eventTypeId: number = 0
    eventType?: string
    sp?: number

}
