# Intro

The intro for sanity/nextjs sites will include a setup of the following projects
1. Sanity studio where cms data can be hosted. The studio will be hosted in the sanity cloud using a personal plan. Don't worry, there is no charge for it. 
2. NextJS project hosted on Vercel using typescript. The project will also use a free personal plan. 

Please note that identifiers are typed within brackets {identifier}

## Prerequisite

This guides requires nodejs and npm to be installed and working. You can verify this by running the commands 
`node -v`
and
`npm -v`

## Setup

Create a new folder for the project, we will run/install everything in this folder.

Install the CLI for sanity and vercel. They will be used for creating and deploying your projects. 

Sanity CLI
`npm install -g @sanity/cli`

Vercel CLI 
`npm install -g vercel`

[Setup sanity studio](docs/sanity/setup.md)

## Excercises

The execises will go through some of the basic concepts of sanity and nextjs. 

## Sanity studio

[Add a field](docs/sanity/add-field.md)

[Add a logo to the studio](docs/sanity/configure-studio.md)

[Your first groq query](docs/sanity/groq.md)

## NextJS

[Setup a nextjs project](docs/nextjs/setup.md)

[First Deployment](docs/nextjs/deploy.md)

[Install Tailwind](docs/nextjs/tailwind.md)

[Create your first components](docs/nextjs/first-component.md)

[Read data from sanity](docs/nextjs/read-from-sanity.md)

[Render a page server side](docs/nextjs/render-server-side.md)

[Render the page static](docs/nextjs/render-static.md)

[Add a site listing route](docs/nextjs/first-route.md)

[Add a site route](docs/nextjs/site-route.md)

[Add a product route](docs/nextjs/product-route.md)

[Add a order button to the product page](docs/nextjs/order-button.md)
