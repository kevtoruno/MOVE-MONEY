export interface User {
    id: number,
    agencyId: number,
    userRoleId: number,
    userName: string,
    firstName: string,
    lastName: string,
    created: Date,
    phoneNumber: string,
    dateOfBirth: Date,
    isActive: boolean,
    money: Number
}
