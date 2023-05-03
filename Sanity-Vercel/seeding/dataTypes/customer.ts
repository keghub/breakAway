import { faker } from "@faker-js/faker";
import { IdentifiedSanityDocumentStub } from "@sanity/client";
import { nanoid } from "nanoid";

export interface ICustomer {
    id: string;
    email: string;
    firstname: string;
    lastname: string;
    address: string;
}

export const createCustomer = (): ICustomer => {
    const firstname = faker.name.firstName();
    const lastname = faker.name.lastName();
    const email = faker.internet.email(firstname, lastname);

    return {
        id: nanoid(),
        firstname,
        lastname,
        address: faker.address.streetAddress(),
        email
    }
}

export const createCustomerDoc = (customer: ICustomer): IdentifiedSanityDocumentStub => {
    const { id, firstname, lastname, address, email } = customer;

    return {
        _id: `customer-${id}`,
        _type: "customer",
        firstname,
        lastname,
        address,
        email,
    };
}