export class UserVM {
    email: string;
    userName: string;
    id?: string;
    name?: string;
    isActive: boolean = true;
    passwordHash?: string;
    userPassword?: string;
    firstName?: string;
    lastName?: string;
    // employeeId?: number = 0;
    phoneNumber?: string;
    userPhone?: string;
    directSupervisorId?: string;
    role?: string;
    clientId: number;
    roleId: number;
    fatherName?: string;
    cnic?: string
    designation?: string
    msCardNo?: string;
    address?: string;
    moduleId: number
    confirmPassword: string
    supervisorId: string
    supervisor: string
    includeSubordinatesData: boolean
    cltId: number
}
export class User {
    private _email: string;
    private _userName: string;

    get email(): string {
        return this._email;
    }

    set email(value: string) {
        this._email = value;
        // Automatically set the userName to the same value as email
        this._userName = value;
    }

    get userName(): string {
        return this._userName;
    }
}

