import { faker } from "@faker-js/faker";
import { IdentifiedSanityDocumentStub } from "@sanity/client";
import { nanoid } from "nanoid";
import { createRef, IRef } from "./ref";

interface IOrder {
    id: string;
    amount: number;
    product: IRef;
}

export const createOrder = (productId: string): IOrder => {
    return {
        id: nanoid(),
        amount: +faker.random.numeric(),
        product: createRef("product", productId),
    }
}

export const createOrderDoc = (order: IOrder): IdentifiedSanityDocumentStub => {
    const { id, amount, product } = order;
    
    return {
        _id: `order-${id}`,
        _type: "order",
        amount,
        product,
    };
}