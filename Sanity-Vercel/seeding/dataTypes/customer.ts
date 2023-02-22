import { faker } from "@faker-js/faker";
import { IdentifiedSanityDocumentStub } from "@sanity/client";
import { nanoid } from "nanoid";

export interface ICustomer {
    id: string;
    firstname: string;
    lastname: string;
    address: string;
}

export const createCustomer = (): ICustomer => {
    return {
        id: nanoid(),
        firstname: faker.name.firstName(),
        lastname: faker.name.lastName(),
        address: faker.address.streetAddress(),
    }
}

export const createCustomerDoc = (customer: ICustomer): IdentifiedSanityDocumentStub => {
    const { id, firstname, lastname, address } = customer;

    return {
        _id: `customer-${id}`,
        _type: "customer",
        firstname,
        lastname,
        address,
    };
}