## Creating an order for a product

In the pages folder, there should be a folder called `api` (if not, create the folder), with a sample file. The sample file exports a handler that returns a 200 with a payload.

Since the api folder is created within the pages folder, the api folder automatically becomes a reachable route in our application.

### Create a new api-endpoint.
* Create file call it `order.ts`
* Export another handler from that file.

**The `api/order` handler**

The handler should accept the payload needed for creating/updating a customer and creating an order.
* Is there an existing customer with the email in the payload
  * Update the customer based on the payload data
  * Create an order and link it to the customer
* If there's not an existing customer
  * Create a new customer
  * Create an order and link it to the new customer

### Update the UI
**The `products/{id}` page**
* Update the product page, add the necessary input fields for creating/updating a customer and creating an order.
  * You will have to check the data model in sanity to discover which type of data is needed.
* Add an "order button", that creates an order using the handler described above.
* Make sure the UI updates after the order has been placed to notify the user.

**Nice to have**: Add validation in the UI and in the API endpoint.

_Note_: We can specify in the sanity document which properties should be required, and that's applied in the sanity UI. But when we use the client (or http endpoint) to post, that required field validation is not applied, i.e. we can post incomplete data if we are not careful.

### Last step
[Deploy to vercel!](deploy.md)
