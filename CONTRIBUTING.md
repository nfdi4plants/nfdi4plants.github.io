# Contributing to the DataPLANT website

Independent of the type of contribution you want to make, make sure to first understand the [general page and content structure](#general-page-and-content-structure).

Then, we have dedicated sections for [content creators](#for-content-creators) and [developers](#for-developers).

- [General Page and Content Structure](#general-page-and-content-structure)
  - [Page structure](#page-structure)
  - [Content structure](#content-structure)
- [For Content Creators](#for-content-creators)
  - [Mainpage Cards](#mainpage-cards)
  - [Subpages](#subpages)
    - [SubPage Configuration](#subpage-configuration)
    - [SubPage Content](#subpage-content)
  - [Articles](#articles)
  - [Events](#events)
  - [News](#news)
  - [Publications](#publications)
  - [Participants](#participants)
- [For Developers](#for-developers)
  - [Content](#content)
    - [`src/content/mainpagecards`](#srccontentmainpagecards)
    - [`src/content/subpage` and `src/content/subpageContent`](#srccontentsubpage-and-srccontentsubpagecontent)
    - [`src/content/articles`](#srccontentarticles)
    - [`src/content/events`](#srccontentevents)
    - [`src/content/news`](#srccontentnews)
    - [`src/content/publications`](#srccontentpublications)
  - [Pages](#pages)
- [Components](#components)
  - [Tailwind + Daisy UI](#tailwind--daisy-ui)
  - [Astro](#astro)


## General Page and Content Structure

### Page structure

In general, the page has (with some exceptions) 3 'Levels' of content:

- `MainPageCards`: 1<sup>st</sup> level content, which is displayed on the main landing page as cards that link to subpages.
- `SubPages`: 2<sup>nd</sup> level content, which is displayed as a subpage with: 
  - a `hero` (large header content)
  - a `feature collection` (a matrix of features that either link to external or 3<sup>rd</sup> level content).
- `Articles`: 3<sup>rd</sup> level content, which is displayed as a single page with a `hero` and the content of the article, event, news or publication.

All of these are generated from markdown files. Generally, these markdown files contain a `frontmatter` section that defines metadata about the content in [yaml format](https://en.wikipedia.org/wiki/YAML), e.g. the background color of a `mainPageCard`. The content itself is usually written below the frontmatter in [markdown syntax](https://www.markdownguide.org/).

There are some exceptions to this structure, for example when pages are generated from data or the 3 level structure does not make sense. These currently are:

- `/arc-data-hub`: Subpage without mainpage card
- `/toolbox`: Subpage without mainpage card
- `publications`:
  - no mainpage card
  - generated from bibtex files
  - 2<sup>nd</sup> level subpage `/publications` has special format with a list of featured and all publications grouped by year.
- `events`:
  - no mainpage card
  - generated from event markdown content files
  - 2<sup>nd</sup> level subpage `/events` has special format with a list of events that can be filtered.
  - each event has a specialy formatted 3<sup>rd</sup> level page with a detailed description of the event.
- `news`:
  - no mainpage card, but a special `DataPLANT News` section is generated from the 6 most recent newsitems
  - generated from event markdown content files
  - 2<sup>nd</sup> level subpage `/news` has special format with a grid with a preview of all news items ordered by most recent.
  - each newsitem has a specialy formatted 3<sup>rd</sup> level page with the full newsitem text.
- `community and partners`
  - no mainpage card
  - 2<sup>nd</sup> level subpage `/community-and-partners` is generated from csv files `participants.csv`, `projects.csv` and `places.csv` in `src/assets/data`.
  This page contains an interactive map showing the location of all participants and projects, as well as a readable list below the map.

### Content structure

- Most of the content in `src/content` maps 1-to-1 on the page structure:

  ```
  ├───articles       // content files for 3rd level articles
  ├───events         // content files for events
  ├───mainpagecards  // content files for mainpage cards
  ├───news           // content files for news items
  ├───publications   // bibtex files for publications
  ├───subpage        // configuration files for subpages (hero + included features)
  └───subpageContent // content files for features on 2nd level subpages          
      ├───about      // content files for features on the about subpage
      ├───...        // etc.
  ```

- **Images** are stored in `src/assets/images`
- **Data** is stored in `src/assets/data`.

## For Content Creators

> [!IMPORTANT]
>_please make sure that you have read the [general page structure](#general-page-structure) section before continuing_

> [!TIP]
> For the addition of `publications`, `events`, `news items`, and `participants` we offer [issue templates](https://github.com/nfdi4plants/nfdi4plants.github.io/issues/new/choose)

To contribute content, you generally have to understand the frontmatter that is expected for each type of content. Then, you can start by creating a markdown file in the appropriate folder.

### Mainpage Cards

Mainpage cards are generated from markdown files in `src/content/mainpagecards`.

The frontmatter for a mainpage card looks like this:

```yaml
title: About DataPLANT
subtitle: Towards democratization of plant research.
image: /src/assets/images/subpage/about/about-mainpage.svg
index: 1
linkTo: about
styling:
  glass: true
  bgColor: mint
  textColor: black
  buttonColor: darkblue
  buttonTextColor: white
  textPosition: left
```

with these fields:

| Field name | description | Note |
| --- | --- | --- |
| `title` | The title of the card | **Mandatory** |
| `subtitle` | A subtitle for the card | **Mandatory** |
| `image` | The path to the image | **Mandatory** |
| `index` | The order of the card on the main page | **Mandatory** |
| `linkTo` | The link to the subpage | **Mandatory** |
| `styling` | The styling of the card | **Mandatory** |

`styling` is an object with the following fields:

| Field name | description | Note |
| --- | --- | --- |
| `glass` | Whether the card should have a glass effect | **Mandatory** |
| `bgColor` | The background color of the card | **Mandatory** |
| `textColor` | The text color of the card | **Mandatory** |
| `buttonColor` | The color of the button | **Mandatory** |
| `buttonTextColor` | The text color of the button | **Mandatory** |
| `textPosition` | The position of the text | one of `left`, `right`, `top`, `bottom`, `text-only`, **Mandatory** |

### Subpages

Subpages are generated from two types of markdown files that both need to be created to generate a valid subpage:

#### SubPage Configuration

The configuration file for a subpage are located at `src/content/subpage`. The frontmatter for a subpage configuration file looks like this:

```yaml
---
title: About DataPLANT
tagline: Towards democratization of plant research.
description: 'Towards democratization of plant research.'
image: /src/assets/images/subpage/about/about-mainpage.svg
styling:
  titleColor: darkblue
  bgColor: mint
  textColor: black
  textPosition: bottom
content: 
  - about/our-mission
  - about/community-and-partners
  - about/governance
  - about/task-areas
  - about/rdm-concept
  - about/testimonials
  - about/nfdi
---
```

with these fields:

| Field name | description | Note |
| --- | --- | --- |
| `title` | The title of the subpage hero | **Mandatory** |
| `description` | A subtitle for the subpage hero | **Mandatory** |
| `tagline` | A tagline for the subpage hero, must be short | **Optional** |
| `image` | The path to the image for the subpage hero | **Mandatory** |
| `styling` | The styling of the subpage hero | **Mandatory** |
| `content` | The content of the subpage feature section | **Mandatory** |

`styling` is an object with the following fields:

| Field name | description | Note |
| --- | --- | --- |
| `titleColor` | The color of the title | **Optional** |
| `bgColor` | The background color of the subpage hero | **Optional** |
| `textColor` | The text color of the subpage hero | **Optional** |
| `textPosition` | The position of the text | one of `left`, `right`, `top`, `bottom`, `text-only`, **Mandatory** |

`content` is an array of strings that reference the subpage content files in `src/content/subpageContent`.

Any markdown content below the frontmatter will be rendered as a text block in the SubPage hero.

#### SubPage Content

The content files for a subpage are located at `src/content/subpageContent`. These files contain the individual feature links in the feature collection grid of the subpage. 

> [!IMPORTANT]
>_All markdown content below the frontmatter in these files will be ignored._

The frontmatter for a subpage content file looks like this:

```yaml
---
title: Our Task Areas
icon: "tabler:subtask"
href: /articles/task-areas
summary: "DataPLANT is organized into four task areas, each with a specific focus and set of objectives."
---
```

with these fields:

| Field name | description | Note |
| --- | --- | --- |
| `title` | The title of the feature | **Mandatory** |
| `icon` | The icon of the feature | format `<icon collection>:<icon name>`, feel free to request additional icon packages, **Mandatory** |
| `href` | The link to the feature | **Mandatory** |
| `summary` | A summary of the feature | **Mandatory** |

### Articles

Articles are generated from markdown files in `src/content/articles`. They have minimal frontmatter, as the focus is on the content of the article.

The frontmatter for an article looks like this:

```yaml
---
title: DataPLANT Task Areas
description: Overview of the DataPLANT Task Areas
---
```

with these fields:

| Field name | description | Note |
| --- | --- | --- |
| `title` | The title of the article | **Mandatory** |
| `description` | A summary of the article | **Mandatory** |

### Events

Events are generated from markdown files in `src/content/events`.

The frontmatter for an event looks like this:

```yaml
---
title: ARCify your research project
description: 'Learn how to move your datasets into ARCs, share them via the DataHUB, and annotate them with metadata.'
category: Training
mode: 'OfflineEventAttendanceMode'
audience: [Users, DataStewards]
when:
  start: 2024-10-24T09:00:00
  end: 2024-10-24T12:00:00
  # -- or for repeating events --
  # - 
  #  start: 2024-10-24
  #  end: 2024-10-25
  # - 
  #  start: 2024-11-24
  #  end: 2024-11-25
tutors:
  - Dominik Brilhaus (CEPLAS)
  - Sabrina Zander (MibiNet)
image: ~/assets/images/events/ceplas-ARCs.drawio.png
organizer:
  name: Dominik Brilhaus
  affiliation: CEPLAS Data
  url: https://www.ceplas.eu/en/research/data-science-and-data-management
location:
  short: HHU
  url: https://www.ceplas.eu/en/contact/how-to-get-there
registration:
  description: 'First-come-first-serve. Members of CEPLAS have priority. Everyone else is welcome, if seats are available.' 
  url: 'https://terminplaner6.dfn.de/en/b/551776b4130c40357ea030db0142f472-910401'
  deadline: 2024-10-17
  seats: 12
external: https://en.wikipedia.org/wiki/URL#:~:text=A%20typical%20URL%20could%20have,html%20).
---
```


with these fields:

| Field name    | description                                                           | Note         |
| ------------- | --------------------------------------------------------------------- | ------------ |
| `title`       | The title of the event                                                | **Mandatory** |
| `description` | A brief description of the event                                      | **Mandatory** |
| `category`    | The category of the event                                             | one of Conference', 'Hackathon', 'Webinar', 'Training', **Mandatory** |
| `when`        | Event instantiation(s)                                                | Either a single object or an array of 'when's , containing the start and end date and time of the event in ISO 8601 format. **Mandatory** |
| `mode`        | The mode of the event (e.g., online, OfflineEventAttendanceMode)                         | one of 'OfflineEventAttendanceMode', 'OnlineEventAttendanceMode', 'MixedEventAttendanceMode', **Mandatory** |
| `audience`    | The intended audience of the event                                    | array of 'Users', 'DataStewards', 'Developers', 'Everyone', **Mandatory** |
| `location`    | The location information for the event                                | **Mandatory** |
| `image`       | The path to the image associated with the event                       | **Optional**  |
| `tutors`      | A list of tutors conducting the event                                 | **Optional** |
| `organizer`   | Information about the organizer                                       | **Optional** |
| `registration`| Details regarding event registration                                  | **Optional** |
| `external`    | An external url                                                       | **Optional** |

`location` is an object with the following fields:

| Field name | description                              | Note         |
| ---------- | ---------------------------------------- | ------------ |
| `name`     | A short name or abbreviation of the location | **Mandatory**  |
| `url`      | The URL with directions or location info  | **Optional**  |
| `address`  | The address of the location in freetext   | **Optional**  |

`tutors` is an array of strings listing the names and affiliations of event tutors.

`organizer` is an object with the following fields:

| Field name   | description                               | Note         |
| ------------ | ----------------------------------------- | ------------ |
| `name`       | The name of the event organizer           | **Optional** |
| `affiliation`| The affiliation of the organizer          | **Optional** |
| `url`        | A URL to the organizer's page or profile  | **Optional**  |

`registration` is an object with the following fields:

| Field name   | description                               | Note         |
| ------------ | ----------------------------------------- | ------------ |
| `description`| A description of the registration process | **Optional**  |
| `url`        | The registration URL                      | **Optional** |
| `deadline`   | The deadline for registration in YYYY-MM-DD format | **Optional** |
| `seats`      | The number of available seats             | **Optional** |

`when` can be either a single object or an array of objects, each containing the start and end date and time of the event in ISO 8601 format.

Single:

```yaml
when:
  start: 2024-10-24T09:00:00
  end: 2024-10-24T12:00:00
```

Repeating:

Using a list of objects will create multiple instances of the event. This is useful for events that repeat on a regular basis.

```yaml
when:
  - 
    start: 2024-10-24T09:00:00
    end: 2024-10-25T12:00:00
  - 
    start: 2024-11-24T09:00:00
    end: 2024-11-25T12:00:00
```

You can give optional props to single instantiantions of a event using the syntax below. The `props` object can have any field the base type has, but it is not mandatory to provide all fields. 

> [!CAUTION]
> If you replace any object field, for example `registration`, you must provide all subfields for this object you want to use. You cannot partially replace an object field.

```yaml
when:
  - 
    start: 2024-10-24T09:00:00
    end: 2024-10-25T12:00:00
  - 
    start: 2024-11-24T09:00:00
    end: 2024-11-25T12:00:00
    props:
      registration:
        description: 'First-come-first-serve. Members of CEPLAS have priority. Everyone else is welcome, if seats are available.' 
        url: 'https://an-entirely-different-url.com'
        deadline: 2024-11-17
        seats: 42
```

In this example the event on 2024-11-24 has a specific different registration object.


### News

News are generated from markdown files in `src/content/news`.

The frontmatter for a news item looks like this:

```yaml
---
date: 2019-05-15
title: BioDATEN and DaPLUS+ at NFDI conference in Bonn
description: Both the not yet started Science Data Center "BioDATEN" and the DaPLUS+ initiative were present at the two days NFDI conference held in mid of May in Bonn.
---
```

with these fields:

| Field name    | description                                                           | Note         |
| ------------- | --------------------------------------------------------------------- | ------------ |
| `date`        | The date of the news item                                             | **Mandatory** |
| `title`       | The title of the news item                                            | **Mandatory** |
| `description` | A brief description of the news item                                  | **Mandatory** |

### Publications

Publications are generated from bibtex files in `src/content/publications`.

> [!IMPORTANT]
> There must be **one bibtex file per publication**, so make sure that citekeys are not reused between files.

The bibtex file for a publication looks like this:

```bibtex
@article{weil2023,
author = {Weil, Heinrich Lukas and Schneider, Kevin and Tschöpe, Marcel and Bauer, Jonathan and Maus, Oliver and Frey, Kevin and Brilhaus, Dominik and Martins Rodrigues, Cristina and Doniparthi, Gajendra and Wetzels, Florian and Lukasczyk, Jonas and Kranz, Angela and Grüning, Björn and Zimmer, David and Deßloch, Stefan and von Suchodoletz, Dirk and Usadel, Björn and Garth, Christoph and Mühlhaus, Timo},
title = {PLANTdataHUB: a collaborative platform for continuous FAIR data sharing in plant research},
journal = {The Plant Journal},
volume = {116},
number = {4},
pages = {974-988},
keywords = {ARC Annotated research context, DataHUB, FAIR, FAIR digital object, research data management},
doi = {https://doi.org/10.1111/tpj.16474},
url = {https://onlinelibrary.wiley.com/doi/abs/10.1111/tpj.16474},
eprint = {https://onlinelibrary.wiley.com/doi/pdf/10.1111/tpj.16474},
abstract = {...},
year = {2023},
note = {open access},
```

The following bibtex fields are currently used:

| Field name | description | Note |
| --- | --- | --- |
| id | the citekey, e.g. `weil2023` | **Mandatory** |
| type | the type of publication, e.g `article` | **Mandatory** |
| author | the authors of the publication | **Mandatory** |
| abstract | the abstract of the publication | **Optional** |
| title | the title of the publication | **Mandatory** |
| journal | the journal the publication was published in | **Optional** |
| doi | the doi of the publication | **Optional**, note that **one of doi or url must be set** |
| url | the url of the publication | **Optional**, note that **one of doi or url must be set** |
| year | the year of publication | **Mandatory** |
| note | used here to signify open access via `note = {open access}` | **Optional** |

### Participants

Add new participants to the `participants.csv` file in `src/assets/data`.

Add new projects to the `projects.csv` file in `src/assets/data`.

Add new places to the `places.csv` file in `src/assets/data`.

Note that participants or projects must always point to an existing place.

## For Developers

> [!IMPORTANT]
>_please make sure that you have read the [general page structure](#general-page-structure) section before continuing_

### Content

> [!IMPORTANT]
> Where possible, content is implemented via [astro content collections]()
> All frontmatter of content files can be further defined in `src/content/config.js`!

#### `src/content/mainpagecards`

These files will be used to generate mainpage cards on index.astro. The file responsible for generating the respective layout is `/src/pages/index.astro`.

#### `src/content/subpage` and `src/content/subpageContent`

> [!NOTE]  
> This content generates what we call "LEVEL 2" content.

All files found here will be used to generate a file structure like `/:slug`. The slug is automatically generated from the filename. The file responsible for generating the respective layout is `/src/pages/[...slug]/index.astro`.

This content type is the most complex as it references another content type found in `src/content/subpageContent`. All subpage content is defined in `src/content/subpageContent` and can be referenced using the frontmatter `content`:

```yaml
content:
  - information/news
  - information/events
```

#### `src/content/articles`

> [!NOTE]  
> This content generates what we call "LEVEL 3" content.

All files found here will be used to generate a file structure like `/articles/:slug`. The slug is automatically generated from the filename. The file responsible for generating the respective layout is `/src/pages/articles/[slug].astro`.

#### `src/content/events`

All files found here are used to generate the page `/events/...` structure. Multiple pages are created for this content:

- `/events` is a list of all events (from `/src/pages/events/index.astro`)
- `/events/[slug]`, where slug is the file name or a frontmatter `slug` property. This can be any of two, a detailed view on a single event or a list of events for repeating events (`when` is array, from `/src/pages/[event]/index.astro`).
- `/events/[slug]/[dd-MM-yyyy]`, slug as above, `dd-MM-yyyy` is `when`.`start` of the specific event instantiation (e.g. `13-11-2024`). This is always a detailed view on a single event (from `src/pages/events/[event]/[date].astro`).

#### `src/content/news`

All files found here will be used to generate a file structure like `/news/:slug`. The slug is automatically generated from the filename. The file responsible for generating the respective layout is `/src/pages/news/[...slug].astro`.

All content news are display in `/src/pages/news/index.astro`.

#### `src/content/publications`

Publications do not follow standard astro collection structure. Instead a bibtex parser is used to read these files. The file responsible for generating the respective layout is `/src/pages/publications.astro`.

### Pages

Astro creates one url from any file placed in `src/pages`. The file structure is automatically converted to a url. For example, `src/pages/test.astro` will be available at `localhost:3000/test`. This can be used to create and design special pages not covered by the content collection structures.

## Components

### Tailwind + Daisy UI

We added support for [Tailwind](https://tailwindcss.com) together with the more opinionated [daisyUI](https://daisyui.com).

### Astro

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