export interface User {
    id: number,
    agencyId: number,
    agency: string,
    userRoleId: number,
    userRole: string,
    userName: string,
    firstName: string,
    lastName: string,
    created: Date,
    phoneNumber: string,
    dateOfBirth: Date,
    isActive: boolean,
    money: Number
}
