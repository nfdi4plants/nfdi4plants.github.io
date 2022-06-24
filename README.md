# The DataPLANT website

This repository contains the content and content generators that result in the DataPLANT website.

## Authoring content

Please refer to [this guide](./src/content/README.md) on how to create content for this website

## Development

### Prerequisites

- Node.js
- .NET SDK >= 5.0
- global installation of sass via npm

### Installation

in the root run once:

1. Download repo.
2. Run `dotnet tool restore` in root directory.
3. Run `dotnet paket install` in root directory.
4. Run `npm install` in root directory.

### Live development

Run `dotnet fornax watch` in `\src` folder.
