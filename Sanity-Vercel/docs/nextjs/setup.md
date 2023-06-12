### Create a NextJS project:
Create a new folder for the "web" project next to the "studio" folder on disk for the project

In the newly created folder, run the following command `npx create-next-app@latest`

This will start a interactive setup for your NextJS project. Use the following setup

```
Ok to proceed? (y) 
[Select] y

√ What is your project named? 
[Type] breakaway

√ Would you like to use TypeScript with this project? 
[Select] Yes

√ Would you like to use ESLint with this project? 
[Select] Yes

√ Would you like to use Tailwind CSS with this project? 
[Select] Yes

√ Would you like to use `src/` directory with this project? 
[Select] Yes

√ Use App Router (recommended)? 
[Select] No

√ Would you like to customize the default import alias? 
[Select]  No 
```

_Note:_ We are not using the App Router in production, so if you end up with an `src/app` folder when you are done, you have done something wrong. What you should have is a folder called `pages` with an index file.

### Start the nextjs site locally
A new folder has been created for your project, move into it, and start the solution by running `npm run dev`. 
A webpage running on localhost should be opened automatically. 
