### Step 1. Create a sanity project: 
In the new folder, run command

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

### Step 2. Create a schema in sanity

Copy [schema files](../../schemas) into the schemas-folder created in the new studio-folder. 

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

### Step 3. Populate schema with data. Run seeding script using following command 

Copy [seeding folder](../../seeding) into new project folder previously created.

Navigate inside the `seeding`-folder, and run first `npm install`, and then:

`node --experimental-specifier-resolution=node --loader ts-node/esm data-seeding.ts`

Inspect the result data.ndjson file to see what is going to be imported into sanity. 

Switch folder back to the `studio`-folder, then run the import using the sanity cli

`sanity dataset import ..\seeding\data.ndjson {your-dataset}`

### Start the studio
Start the studio using the command `npm run dev`. A local web page with the studio should start. Notice the entity listed to the left and how you can navigate among the different entities. 

### Deploy!
Use the sanity CLI to deploy your new studio
