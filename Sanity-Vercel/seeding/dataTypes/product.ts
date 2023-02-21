import { faker } from "@faker-js/faker";
import { IdentifiedSanityDocumentStub } from "@sanity/client";
import { nanoid } from "nanoid";
import { createRef, IRef } from "./ref"

interface IProduct {
    id: string;
    description: string;
    name: string;
    price: number;
    sites: Array<IRef>;
}

export const createProduct = (siteIds: Array<string>): IProduct => {
    return {
        id: nanoid(),
        description: faker.commerce.productDescription(),
        name: faker.commerce.productName(),
        price: +faker.commerce.price(),
        sites: siteIds.map(siteId => createRef("site", siteId)),
    };
}

export const createProductDoc = (product: IProduct): IdentifiedSanityDocumentStub => {
    const { id, description, name, price, sites } = product;
    
    return {
        _id: `product-${id}`,
        _type: "product",
        description,
        name,
        price,
        sites,
    };
}