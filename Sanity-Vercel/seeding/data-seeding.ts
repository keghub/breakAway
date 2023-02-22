// Data seeding script based on mock data from faker js
// https://fakerjs.dev

import { faker } from "@faker-js/faker";
import fs from "fs";

import { createCustomer, createCustomerDoc } from "./dataTypes/customer.js";
import { createOrder, createOrderDoc } from "./dataTypes/order.js";
import { createProduct, createProductDoc } from "./dataTypes/product.js";
import { createSite, createSiteDoc, ISite } from "./dataTypes/site.js";

console.info("--- Starting script ---")

const numberOfSites = faker.datatype.number({ min: 5, max: 10 });
const numberOfProducts = faker.datatype.number({ min: 200, max: 300 });
const numberOfOrders = faker.datatype.number({ min: 200, max: 300 });
const numberOfCustomers = faker.datatype.number({ min: 200, max: 300 });

console.info(`Will create:
${numberOfSites} sites
${numberOfProducts} products
${numberOfOrders} orders
${numberOfCustomers} customers`
);

const sites = new Array(numberOfSites)
    .fill({})
    .map(() => createSite());

const siteDocs = sites.map(s => createSiteDoc(s));

const products = new Array(numberOfProducts)
    .fill({})
    .map(() => {
        const productSiteIds = faker.helpers.arrayElements(sites).map((s: ISite) => s.id);
        const product = createProduct(productSiteIds);

        return product;
    });

const productDocs = products.map(p => createProductDoc(p));

const orders = new Array(numberOfOrders)
    .fill({})
    .map(() => {
        const productId = faker.helpers.arrayElement(products).id;
        const order = createOrder(productId);

        return order;
    });

const orderDocs = orders.map(o => createOrderDoc(o));

const customers = new Array(numberOfCustomers)
    .fill({})
    .map(() => createCustomer());

const customerDocs = customers.map(c => createCustomerDoc(c));

console.info("Writing data to file: data.ndjson");

fs.writeFileSync("data.ndjson", 
`${siteDocs.map(s => JSON.stringify(s)).join("\n")}
${productDocs.map(p => JSON.stringify(p)).join("\n")}
${orderDocs.map(o => JSON.stringify(o)).join("\n")}
${customerDocs.map(c => JSON.stringify(c)).join("\n")}`
);

console.info("--- Script complete ---");
