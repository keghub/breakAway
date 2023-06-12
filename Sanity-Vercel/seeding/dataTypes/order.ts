import { faker } from "@faker-js/faker";
import { IdentifiedSanityDocumentStub } from "@sanity/client";
import { nanoid } from "nanoid";
import { createRef, IRef } from "./ref";

interface IOrder {
  id: string;
  amount: number;
  product: IRef;
  customer: IRef;
}

export const createOrder = (productId: string, customerId: string): IOrder => {
  return {
    id: nanoid(),
    amount: +faker.random.numeric(),
    product: createRef("product", productId),
    customer: createRef("customer", customerId),
  };
};

export const createOrderDoc = (order: IOrder): IdentifiedSanityDocumentStub => {
  const { id, amount, product, customer } = order;

  return {
    _id: `order-${id}`,
    _type: "order",
    amount,
    product,
    customer,
  };
};
