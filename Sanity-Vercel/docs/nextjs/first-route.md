## Create your first route

We want to move the listing of the sites to it's own page. We're also gonna create a new page for displaying a specific site.

### Setup
* Create a new folder in the pages folder, and name it `sites`
* In the new folder, create a new typescript file, name it `index.tsx`
  * This corresponds to the `/sites` route, and will be used to render the list of sites
* In the new folder, create a new typescript file, name it `[slug].tsx`
  * This corresponds to `/sites/[site-slug]`, and will be used to display details of a specific site.

### Home page
Remove the list of the sites, and instead add a link to the sites index page.

### Sites Index page
Should display the list of sites, and link to the specific site pages

### Site page
Fetch the site from sanity, based on the site slug. 
* If we can't match to a site, return 404
* If we find a site, fetch the properties. Send the slogan and color to the header component. (Should display the slogan using the site color as background.)

### Move business logic away from page-components
* Create a new folder on the same level as the pages folder, call it `handlers`
* In this new folder, create a file called `SiteStaticDataHandler.ts`. Note that this is not a react file, it's not used for rendering, only for fetching data needed in our component.
* Create a static method for fetching the sites in the component.
* Use this handler in the sites index page and the site page to fetch the data from sanity.

### Deploy
* [Deploy in vercel!](deploy.md)

_**Note:**_ The sites folder we created means we have now created a route called sites. The bracket filenaming means that our page can allow a parameter.

Read more about routing https://nextjs.org/docs/routing/introduction

**Example of the `SiteStaticDataHandler`**
```
# file: SiteStaticDataHandler.ts

export class SiteStaticDataHandlers {
  static async GetSites(): Array<Site>
  static async GetSite(slug: string): Site
}
```
