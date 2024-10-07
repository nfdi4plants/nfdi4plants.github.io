Here you can find some additional content which might help you to contribute to this project.

# Structure

## Content

> [!TIP]
> All frontmatter of content files can be further defined in `src/content/config.js`!

### `src/content/mainpagecards`

> [!NOTE]
> This content generates what we call "LEVEL 1" content.

Sadly this does not follow standard astro content collection structure. Please chck out existing files in `src/content/mainpagecards` to get an idea of how to structure your content. The file responsible for generating the respective layout is `src/components/MainPageCard.astro` used in `src/pages/index.astro`.

> [!IMPORTANT]
> Feel free to contribute by adjusting this structure to follow the standard astro collection structure. With a defined schema in `src/content/config.js` combined with `getCollection()` for content access.

### `src/content/subpage` and `src/content/subpageContent`

> [!NOTE]  
> This content generates what we call "LEVEL 2" content.

All files found here will be used to generate a file structure like `/:slug`. The slug is automatically generated from the filename. The file responsible for generating the respective layout is `/src/pages/[...slug]/index.astro`.

This content type is the most complex as it references another content type found in `src/content/subpageContent`. All subpage content is defined in `src/content/subpageContent` and can be referenced using the frontmatter `content`:

```yaml
content:
  - information/news
  - information/events
```

### `src/content/articles`

> [!NOTE]  
> This content generates what we call "LEVEL 3" content.

All files found here will be used to generate a file structure like `/articles/:slug`. The slug is automatically generated from the filename. The file responsible for generating the respective layout is `/src/pages/articles/[slug].astro`.

### `src/content/events`

All files found here will be used to generate a file structure like `/events/:slug`. The slug is automatically generated from the filename. The file responsible for generating the respective layout is `/src/pages/events/[...slug].astro`.

All content events are display in `/src/pages/events/index.astro`.

### `src/content/news`

All files found here will be used to generate a file structure like `/news/:slug`. The slug is automatically generated from the filename. The file responsible for generating the respective layout is `/src/pages/news/[...slug].astro`.

All content news are display in `/src/pages/news/index.astro`.

### `src/content/publications`

Publications do not follow standard astro collection structure. Instead a bibtex parser is used to read these files. The file responsible for generating the respective layout is `/src/pages/publications.astro`.


## Pages

Astro creates one url from any file placed in `src/pages`. The file structure is automatically converted to a url. For example, `src/pages/test.astro` will be available at `localhost:3000/test`. This can be used to create and design special pages not covered by the content collection structures.

# Components

## Tailwind + Daisy UI

We added support for [Tailwind](https://tailwindcss.com) together with the more opinionated [daisyUI](https://daisyui.com).

## Astro

This site uses [Astro](https://astro.build) as static site generator. It is highly adaptable and can be used with any frontend framework. Currently we added support for `.astro`, `.md`, `.mdx` files. But astro can work with any frontend framework like React, Vue, Svelte, Preact etc.

Will paste the astro readme part below:

<details>
<summary>Click to expand</summary>

![just-the-basics](https://github.com/withastro/astro/assets/2244813/a0a5533c-a856-4198-8470-2d67b1d7c554)

## 🚀 Project Structure

Inside of your Astro project, you'll see the following folders and files:

```text
/
├── public/
│   └── favicon.svg
├── src/
│   ├── components/
│   │   └── Card.astro
│   ├── layouts/
│   │   └── Layout.astro
│   └── pages/
│       └── index.astro
└── package.json
```

Astro looks for `.astro` or `.md` files in the `src/pages/` directory. Each page is exposed as a route based on its file name.

There's nothing special about `src/components/`, but that's where we like to put any Astro/React/Vue/Svelte/Preact components.

Any static assets, like images, can be placed in the `public/` directory.

## 🧞 Commands

All commands are run from the root of the project, from a terminal:

| Command                   | Action                                           |
| :------------------------ | :----------------------------------------------- |
| `npm install`             | Installs dependencies                            |
| `npm run dev`             | Starts local dev server at `localhost:4321`      |
| `npm run build`           | Build your production site to `./dist/`          |
| `npm run preview`         | Preview your build locally, before deploying     |
| `npm run astro ...`       | Run CLI commands like `astro add`, `astro check` |
| `npm run astro -- --help` | Get help using the Astro CLI                     |

## 👀 Want to learn more?

Feel free to check [our documentation](https://docs.astro.build) or jump into our [Discord server](https://astro.build/chat).

</details>