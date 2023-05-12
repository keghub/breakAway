export default {
    name: "product",
    title: "Products",
    type: "document",
    fields: [
        {
            name: "name",
            title: "Name",
            type: "string",
        },
        {
            name: "price",
            title: "Price",
            type: "number",
        },
        {
            name: "description",
            title: "Description",
            type: "text",
        },
        {
            name: "sites",
            title: "Sites",
            type: "array",
            of: [
                { 
                    type: "reference",
                    to: [{ type: "site" }] 
                }
            ]
        }
    ]
}