import { faker } from "@faker-js/faker";
import { IdentifiedSanityDocumentStub } from "@sanity/client";
import { nanoid } from "nanoid";

export interface ISite {
    id: string;
    name: string;
    slug: string;
    slogan: string;
    primaryColor: string;
}

export const createSite = (): ISite => {
    const name = faker.company.name();
    
    return {
        id: nanoid(),
        name,
        slug: faker.helpers.slugify(name).toLowerCase(),
        slogan: faker.company.catchPhrase(),
        primaryColor: faker.color.rgb(),
    };
}

export const createSiteDoc = (site: ISite): IdentifiedSanityDocumentStub => {
    const { id, name, slug, slogan } = site;
    
    return {
        _id: `site-${id}`,
        _type: "site",
        name,
        slug,
        slogan,
    };
}