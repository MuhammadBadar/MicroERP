import { BaseDomainVM } from '../../catalog/Models/BaseDomainVM';
export class ScheduleVM extends BaseDomainVM {
  userId: string
  user?: string
  roleId: string
  role?: string
  entityId: number = 0;
  entity?: string
  scheduleDays?: ScheduleDayVM[] = new Array();
  scheduleTypeId: number = 0;
  scheduleType?: string
  workingTypeId?: number = 0;
  workingType?: string
  workingHours?: string
  startDate?: Date
  endDate?: Date
  effectiveDate?: Date
  dayIds: number[] = []


}
export class ScheduleDayVM extends BaseDomainVM {
  dayId: number = 0
  day?: string
  workTime: string
  scheduleTypeId: number = 0;
  scheduleType?: string
  eventTypeId: number = 0
  eventType?: string
  locationId: number = 0
  location?: string
  override isActive: boolean = true;
  dBoperation?: number;
  scheduleId?: number;
  schId?: number;
  editMode: boolean = false
  userId: string
  schDayEvents: string
  schDayType: number
}
