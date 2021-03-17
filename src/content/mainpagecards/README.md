# Creating mainpagecard content

<!-- TOC -->

- [Introduction](#introduction)
- [Metadata](#metadata)
- [Example](#example)

<!-- /TOC -->

## Introduction

## Metadata

- `heading`: the heading of the card, will be rendered in emphasis color as uppercase heading
- `title`: the title of the card
- `index`: index determining the order in which these cards are rendered (lowest number will be the first card etc.)
- `card-bg`: background color of the card. See [allowed colors](../README.md#allowed-colors).
- `container-bg`: background color of the container containing the card. See [allowed colors](../README.md#allowed-colors).
- `emphasis-color`: Emphasis color used in this card. See [allowed colors](../README.md#allowed-colors).
- `image`: a link to the image used in the card
- `learn-more-link`: link to the associated learn more site, will be target of the 'learn more' button
- `text-position`: Position of the text. allowed values: `left`/`right`

## Example

```
---
heading: our mission
title: Driving the digital change in plant science
index: 1
card-bg: white
container-bg: mint
emphasis-color: mint
image: /images/mission.svg
learn-more-link: content/learn-more/our-mission.html
text-position: left

---

DataPLANT’s mission is to provide a sustainable and
well annotated data management platform for plant
sciences. DataPLANT will pave the way from 
publication including the annotated research context

```
