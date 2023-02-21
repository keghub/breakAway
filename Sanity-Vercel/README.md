Steps for intro on sanity sites

### Step 1. Install sanity CLI
`npm install -g @sanity/cli`
### Step 2. Create a sanity project: 
Run command

`npm create sanity@latest`

This will start a interactive setup for your sanity project. Use the following setup

```
✔ Fetching existing projects

? Select project to use 
[Select] Create new project

? Your project name: 
[Type] BreakAway

Your content will be stored in a dataset that can be public or private, depending on
whether you want to query your content with or without authentication.

The default dataset configuration has a public dataset named "production".

? Use the default dataset configuration? 
[Select] Yes

✔ Creating dataset
Project output path: {project-path}/studio

? Select project template 
[Select] Clean project with no predefined schemas

? Do you want to use TypeScript? 
[Select] Yes

✔ Bootstrapping files from template
✔ Resolving latest module versions
✔ Creating default project files

Package manager to use for installing dependencies? 
[Select] npm
```

### Step 3. Create a NextJS project: npx create-next-app@latest
This will start a interactive setup for your NextJS project. Use the following setup

```
Your project name
[Type] breakaway

✔ Would you like to use TypeScript with this project?  
[Select] Yes
```

### Step 4. Create a schema in sanity

Copy schema files into the schemas. 

Deploy the studio using the following command. 
`sanity deploy`

On the first deploy you will be asked a studio hostname. Please use the format `breakaway-{identifier}` to avoid name conflicts with other people who will run the breakaway project. 
```
✔ Checking project info
Your project has not been assigned a studio hostname.
To deploy your Sanity Studio to our hosted Sanity.Studio service,
you will need one. Please enter the part you want to use.
? Studio hostname (<value>.sanity.studio): 
[Type] breakaway-{identifier}

✔ Clean output folder (2ms)
✔ Build Sanity Studio (10353ms)
✔ Verifying local content
✔ Deploying to Sanity.Studio

Success! Studio deployed to https://breakaway-{identifier}.sanity.studio/
```

### Step 5. Populate schema with data. Run seeding script using following command 

Create a token using sanity studio, give the token read and write access to your project. Add the token to your data-seeding config. 

`node --experimental-specifier-resolution=node --loader ts-node/esm data-seeding.ts`