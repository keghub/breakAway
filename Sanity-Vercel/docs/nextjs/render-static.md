### Convert the function from `getServerSideProps` to `getStaticProps`

For a nextjs application we can choose between static rendering, server side rendering or client side rendering.

The difference between static rendering and server side rendering is that static rendered content will be rendererd at build time. The rendered content will then be cached for a specified time.

Using static rendering improves the performance the since everything is ready to be served to the user, but it does increase the build time.

**To implement static rendering:**
* Start with renaming the getServerSideProps to getStaticProps.
* In your dynamic route (for the sites), add a method called `getStaticPaths` (https://nextjs.org/docs/pages/building-your-application/data-fetching/get-static-paths)
  * For this simple example, it just needs to return 
```
return {
  paths: [],
  fallback: 'blocking',
};
```
* Update the default `getStaticPaths` to make sure it renders the pages for all the sites (update the paths parameter)

_Note:_ 
* The paths parameter tells nextjs and vercel which paths to render at build time. If we leave it empty as above, no paths will be rendererd at build.
* Fallback blocking means that if there's no page cached, the page will be server side rendered, and then cached.
* In a real application, we would add the getStaticPaths per [dynamic route](https://nextjs.org/docs/pages/building-your-application/routing/dynamic-routes).

Take some time to read about static rendering at https://nextjs.org/docs/basic-features/data-fetching/get-static-props

[Deploy!](deploy.md)
