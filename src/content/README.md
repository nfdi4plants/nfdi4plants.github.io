# Creating content for the DataPLANT website

<!-- TOC -->

- [Introduction](#introduction)
    - [What is markdown](#what-is-markdown)
    - [Writing markdown content](#writing-markdown-content)
- [How generators work](#how-generators-work)
    - [Generator metadata](#generator-metadata)
    - [Generator markdown content](#generator-markdown-content)
    - [Example](#example)
- [Content registry](#content-registry)
- [Details](#details)
    - [Allowed colors](#allowed-colors)

<!-- /TOC -->

## Introduction

All of the content displayed on the website (besides some scaffolding components such as the navigation bar on the top and the footer on the bottom) are generated from `.md` (**m**ark**d**own) files in the `content directory`. In general, content will only be rendered if there is a generator in place for that type of content.

To ensure an easy content creation process, the basics of markdown and how to write markdown content are laid out in the following paragraphs. The process, guidelines, and requirements for the individual content types are linked [here](#content-registry) and layed out in the respective articles.

### What is markdown

Markdown is a easy-to-read, easy-to-write plain text format, which can be used to generate content such as HTML (the format of the very webpage you are currently browsing). In fact, the very content you are currently reading is written in markdown.

Please refer to this [cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Cheatsheet) or learn more about the full markdown spec [here](https://daringfireball.net/projects/markdown).

here is an example:

```
# I am a large heading

I am a paragraph

**bold text**
```

will be rendered to:

---

# I am a large heading

I am a paragraph

**bold text**

---

### Writing markdown content

The most easy way to edit markdown files is via a text editor or IDE that supports markdown rendering and syntax help.

[Visual Studio Code](https://code.visualstudio.com/) is a lightweight editor that does the job perfectly, but you can use any kind of editor you like.

You can follow [this guide](https://code.visualstudio.com/docs/languages/markdown) to set up an amazing live preview environment for writing markdown files with VSCode. 

## How generators work

As you will see in the respective guides for each content type, your markdown files must contain two sections of content:

1. [**Generator metadata**](#generator-metadata)
2. [**Any kind of markdown content**](generator-markdown-content)

### Generator metadata

Generator metadata is used to determine content specific parameters such as the date of the news item, the background color of the card, etc. 

It resides in a special section of your markdown file called `frontmatter`. Your file must start with this section. 

It contains key value pairs separated by colons, enclosed by `---`. Here is an example:

```
---
color: mint
date: 03/03/2021

---
```

The valid keys and values for those keys are unique for each content type and explained in detail on the respective artice.

### Generator markdown content

Below the frontmatter you can then write any kind of markdown content, which will be rendered as one block of content by the generator. 

### Example

Here is a full news article markdown file. The frontmatter contains the necessary metadata to categorize and correctly list the newsitem, while the markdown content below that metadata is the actual newsarticle itself:

```
---
date: 03/17/2021
title: EIL
---

# Newsflash

Today i ate some **cereals**.

```

## Content registry

Find some quick links to the description and content creation process of individual content types below. 

The following types of content can currently be created and mapped by generators:

- [NewsItems](./news/README.md) - Individual news items and related content
- [MainPageCards](./mainpagecards/README.md) - A single card on the landing page and related content
- [LearnMoreSites](./learn-more/README.md) - The `learn-more` site that is linked on a mainpagecard

## Details

### Allowed colors

This page uses the DataPLANT color palette. for more info, take a look at [this repository](https://github.com/nfdi4plants/Branding#colors)

The following base colors are allowed in content:

    black
    white
    mint
    lightblue
    darkblue
    yellow
    olive
    red

Additionally, you can use the `lighter-x` or `darker-x` modifiers, where x is a multiple of 10 between 10 and 90, e.g. `mint-darker-20` or `red-lighter-80`