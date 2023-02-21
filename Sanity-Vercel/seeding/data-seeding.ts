// Data seeding script based on mock data from faker js
// https://fakerjs.dev

import { faker } from "@faker-js/faker";
import { createClient } from "@sanity/client";

import { config } from "./config.js";

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
    ${numberOfCustomers} customers`)

const sites = new Array(numberOfSites)
    .fill({})
    .map(() => createSite());

const products = new Array(numberOfProducts)
    .fill({})
    .map(() => {
        const productSiteIds = faker.helpers.arrayElements(sites).map((s: ISite) => s.id);
        const product = createProduct(productSiteIds);

        return product;
    });

const orders = new Array(numberOfOrders)
    .fill({})
    .map(() => {
        const productId = faker.helpers.arrayElement(products).id;
        const order = createOrder(productId);

        return order;
    });

const customers = new Array(numberOfCustomers)
    .fill({})
    .map(() => createCustomer());

const sanityClient = createClient(config);

// Add sites to sanity
const setupSites = async () => {
    console.info("Setting up sites...");

    const sitePromises = sites.map(site => {
        const siteDoc = createSiteDoc(site);
        return sanityClient.createOrReplace(siteDoc);
    });
    
    await Promise.all(sitePromises)
        .catch(err => {
            console.error("Unable to create sites", err);
            throw err;
        });

    console.info("Done");
}

// Add products to sanity
const setupProducts = async () => {
    console.info("Setting up products...");

    const productPromises = products.map(product => {
        const productDoc = createProductDoc(product);
        return sanityClient.createOrReplace(productDoc);
    });
    
    await Promise.all(productPromises)
        .catch(err => {
            console.error("Unable to create products", err);
            throw err;
        });
        
    console.info("Done");
} 

// Add orders to sanity
const setupOrders = async () => {
    console.info("Setting up orders...");

    const orderPromises = orders.map(order => {
        const orderDoc = createOrderDoc(order);
        return sanityClient.createOrReplace(orderDoc);
    });
    
    await Promise.all(orderPromises)
        .catch(err => {
            console.error("Unable to create orders", err);
            throw err;
        });
        
    console.info("Done");
}

// Add customers to sanity
const setupCustomers = async () => {
    console.info("Setting up customers...");

    const customerPromises = customers.map(customer => {
        const customerDoc = createCustomerDoc(customer);
        return sanityClient.createOrReplace(customerDoc);
    });
    
    await Promise.all(customerPromises)
        .catch(err => {
            console.error("Unable to create customers", err);
            throw err;
        });
        
    console.info("Done");
}

const setup = async () => {
    await setupSites();
    await setupProducts();
    await setupOrders();
    await setupCustomers();
}

setup().then(() => console.info("--- Script complete ---"));