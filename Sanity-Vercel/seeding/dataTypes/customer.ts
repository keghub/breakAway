import { faker } from "@faker-js/faker";
import { IdentifiedSanityDocumentStub } from "@sanity/client";
import { nanoid } from "nanoid";

export interface ICustomer {
    id: string;
    firstname: string;
    lastname: string;
    address: string;
    createdAt: Date;
}

export const createCustomer = (): ICustomer => {
    return {
        id: nanoid(),
        firstname: faker.name.firstName(),
        lastname: faker.name.lastName(),
        address: faker.address.streetAddress(),
        createdAt: faker.date.recent(100),
    }
}

export const createCustomerDoc = (customer: ICustomer): IdentifiedSanityDocumentStub => {
    const { id, firstname, lastname, address, createdAt } = customer;

    return {
        _id: `customer-${id}`,
        _type: "customer",
        firstname,
        lastname,
        address,
        createdAt,
    };
}