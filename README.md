# The DataPLANT website

This repository contains the content and content generators that result in the DataPLANT website.

## Authoring content

- [Introduction](./src/content/README.md#introduction)
- [How generators work](./src/content/README.md#how-generators-work)
- [Content registry](./src/content/README.md#content-registry)
- [Details](./src/content/README.md#details)
- [Publications](./src/content/README.md#publications)

## Development

### Prerequisites

- Node.js
- .NET SDK >= 5.0
- global installation of sass via npm

### Installation

in the root run once:

1. Download repo.
2. Run `dotnet tool restore` in root directory.
3. Run `npm install` in root directory.

### Live development

In root run `npm run fornax` and go to http://127.0.0.1:8080/index.html.
