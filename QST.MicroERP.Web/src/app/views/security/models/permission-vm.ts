import { BaseDomainVM } from "../../catalog/Models/BaseDomainVM";

export class PermissionVM extends BaseDomainVM {
    route?: string;
    user?: string;
    role?: string;
    resource?: string;
    override isActive: boolean = true;
    userId?: string = '';
    roleId: number = 0;
    // override clientId: number = 0;
    routeId: number;
    permissionId: number;
    permission?: string;
    isReadOnly?: boolean
    cltId: number
}