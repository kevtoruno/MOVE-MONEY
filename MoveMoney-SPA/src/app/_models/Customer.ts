import { TypeIdentification } from './TypeIdentification';

export interface Customer {

    id: number;
    firstName: string;
    lastName: string;
    typeIdentificationId: number;
    identification: string;
    phoneNumber: string;
    dateOfBirth?: Date;
    country: string;
    city: string;
    address?: string;
}

export declare type Customers = Customer[]

export interface CustomerParams {
    customers: Customers;
}

