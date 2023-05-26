## Loading the content server side (`getServerSideProps`)

In the last excersise, we loaded the content of the page client side, now we're going to change it so that we load the page server side instead.

Look into the [documentation](https://nextjs.org/docs/basic-features/data-fetching/get-server-side-props) for `getServerSideProps`.

**Excersise**
* Move the rendering of the sites from client side to the server side.
* [Deploy the app to vercel!](deploy.md)

_Note:_ Now that we are using our environment variables server side instead, we can update them so that they are not accessed client side anymore (this is in general preferable). See the documentation: https://nextjs.org/docs/basic-features/environment-variables

_Note 2:_ For these simpler examples we can use the `InferGetServerSidePropsType<typeof getServerSideProps>`, but for more complex scenarios we should aim to use a dedicated interface for the page components.



????
https://nextjs.org/docs/basic-features/pages#pre-rendering
