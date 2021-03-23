# Creating news content

<!-- TOC -->

- [Introduction](#introduction)
- [Metadata](#metadata)
- [Example](#example)

<!-- /TOC -->

## Introduction

News items are referenced in 3 places of the website:
- each article has its own url that displays just the newsitem
- the 4 latest news items are rendered as a special card on the main page, with only the first 50 words of the article shown
- the news section lists all newsitems sorted by date with only the first 75 words shown

## Metadata

The following metadata must be provided:

- `date`: The date of the news article in the format of MM/DD/YYYY
- `title`: The title of the article
- `preview-text`: A really short summary of the article that will be displayed in the newsitem preview on the landing and news pages.

## Example

```
---
date: 03/17/2021
title: EIL
preview-text: NEWSFLASH!!!
---

# Newsflash

Today i ate some **cereals**.

```