export default {
    name: "order",
    title: "Orders",
    type: "document",
    fields: [
        {
            name: "product",
            title: "Product",
            type: "reference",
            to: [{ type: "product" }]
        },
        {
            name: "amount",
            title: "Amount",
            type: "number",
        }
    ]
}