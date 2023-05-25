## We are connecting our vercel app to our sanity database

### Part 1:

**Preparation**
* Install the [sanity client](https://www.npmjs.com/package/@sanity/client) by running `npm install @sanity/client` in the vercel/nextjs app.
* We can now read data from sanity, see the client documentation for a basic sample.
* To be able to access your sanity database, you will need to find the project id, which can be found in the Sanity Studio management ui.
* We also need the name of the dataset, which also can be found in the management ui (if you've followed the previous steps, the name of the dataset will be `production`)

**Excercise**
* The goal is to display the names for the list of sites in sanity in one of the `Column` components.
* Note that to use an async call to load data in react, you can use the `useEffect` hook. See: https://react.dev/reference/react/useEffect
  * Read up about the hook, and make sure you understand how it works

_Hint:_ If you get a CORS error, you might need to add the url to your nextjs app in the list of allowed CORS-origins in the Sanity Studio management ui (see: https://www.sanity.io/manage/personal/project/{propertyId}/api#cors-origins)

### Part 2:

Use an .env file for all the sensitive information, and use environment variables.
https://nextjs.org/docs/basic-features/environment-variables
