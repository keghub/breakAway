### Go to the vision component in sanity studio and add a groq query
Learn the basics about groq queries here https://www.sanity.io/docs/groq

Here are some suggested queries you can try out:
* Select everything from all tables in the database, limit the query to the first 5 items
* Select everything from the first 5 orders
* Select everything from the first 5 customers, sorted by `firstname`
* Select `_id` and `email` from the first 5 customers, sorted by `firstname`
* Select `_id`, `amount`, `product.name`, `product.price` from one order (add a filter on the `_id`). Here we will join in the product to the order
  * Try to write the join both using the "dereference operator", and using a sub query
* Select everything from the products with a price above a threshold
* Select a specific product and dereference the site property
* Update the query above to use the params instead for the filtering

_**Hint:**_ `Ctrl + /` can be used to comment out multiple lines
