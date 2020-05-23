export interface Agency {
    id: number,
    countryId: number,
    agencyName: string,
    agencyType: string,
    city: string,
    money: number
}
export declare type Agencies = Agency[]

export interface AgencyParams {
    agencies: Agencies;
}