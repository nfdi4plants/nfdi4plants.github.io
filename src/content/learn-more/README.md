# Creating learn-more content

<!-- TOC -->

- [Introduction](#introduction)
- [Metadata](#metadata)
    - [hero](#hero)
    - [cards](#cards)
- [Examples](#examples)
    - [hero](#hero-1)
    - [cards](#cards-1)

<!-- /TOC -->

## Introduction

Learn more pages consist of two types of files:

1. a single `hero.md` file that MUST exist and will be rendered as the top element of the page
2. An arbitrary amount of markdown files that will be rendered as cards below that hero.

These files must be put in a subfolder of this directory to indicate that they belong together. 

Content in a single folder (e.g. `content/learn-more/our-mission`) will be aggregated and rendered as one page (e.g. `content/learn-more/our-mission.html`).

## Metadata

Metadata between the hero and cards are very similar, but the distinction is necessary to ensure correct content generation.

### hero

- `heading`: the heading of the hero, will be rendered in emphasis color as uppercase heading
- `title`: the title of the hero
- `bg-color`: background color of the hero. See [allowed colors](../README.md#allowed-colors).
- `emphasis-color`: emphasis color used in this hero. See [allowed colors](../README.md#allowed-colors).
- `image`: a link to the image used in the hero
- `layout`: the layout of the hero. allowed values: 
`text-only`/`image-only`/`text-top-image-bottom`/`text-bottom-image-top`/`text-left-image-right`/`text-right-image-left`

### cards

- `title`: the title of the card
- `bg-color`:  background color of the card. See [allowed colors](../README.md#allowed-colors).
- `border-color`: border color of the card. See [allowed colors](../README.md#allowed-colors).
- `emphasis-color`: emphasis color used in this card. See [allowed colors](../README.md#allowed-colors).
- `image`: a link to the image used in the hero
- `layout`: the layout of the hero. allowed values: 
`text-only`/`image-only`/`text-top-image-bottom`/`text-bottom-image-top`/`text-left-image-right`/`text-right-image-left`
- `index`: index determining the order in which the cards are rendered (lowest number will be the first card etc.)


## Examples

### hero

```
---
heading: our mission
title: Making your research digital 
bg-color: mint
emphasis-color: darkblue
image: /images/making-research-digital.png
layout: text-top-image-bottom

---

From notes to knowledge by increasing the level of 
annotation at the source, tracking provenance using
community standards. This will  maximize data 
discoverability and reuse following the FAIR data
principles.
```

### cards

```
---
title: Homogenization to simplify RDM landscape
bg-color: yellow-lighter-50
border-color: yellow
emphasis-color: mint
image: /images/arc.svg
layout: text-top-image-bottom
index: 1

---

DataPLANT will path the way from classical paper
publication to pure data publication including
the annotated research context.
```