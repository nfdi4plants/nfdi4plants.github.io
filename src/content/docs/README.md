# How-to "Knowledge base"

## Introduction

The DataPLANT knowledge base is built on [nfdi-web-components](https://nfdi4plants.github.io/web-components-docs/) and will fit all **markdown** content into this "framework".

For a general introduction to writing markdown, see: [Markdown tutorial](tutorials/markdown.md) and references therein.

The knowledge base is structured into three sections.




### Authoring content

To add more documentation, add a markdown file to `\src\docs`. The file MUST start with a metadata block:

<!--used yml here as code language for nice color syntax-->
```yml
---
layout: docs
title: Metadata
published: 2022-05-09
author: Dominik Brilhaus
author_orcid: https://orcid.org/0000-0001-9021-3197
author_github: brilator
add toc: true
add sidebar: sidebars\mainSidebar.md
article_status: published
todo:
    - Update links to other KB articles
---
```

- All keys (`layout`, `author`, etc.) are **not** case sensitive.
- All fields can be at ANY position.
- MUST start and end with `---` .
- MUST contain `layout: docs`.
  - This triggers fornax parsing to html.
- MUST contain `title: xxxx`.
  - Will be added as "# xxxx" to the html.
  - Will be used to name the generated webpage.
- MUST contain `published: yyyy-MM-dd`.
- MAY contain `author: xxxx`.
- MAY contain `author_github: xxxx`.
  - This helps authoring and reviewing content. 
- MAY contain `add toc: true`.
  - Will add automated table of contents from all found headers in content.
- MAY contain `add sidebar: realtive\path\to\sidebar.md` to add the sidebar element to the page.
- MAY contain **any** other metadata. The information will be read but will not affect the generated html.

### Sidebar

Sidebar files MAY be in ANY **subdirectory** of `\src\docs`. Sidebar markdown files must start with a metadata block:

```yml
---
published: 2022-05-09
article_status: published
todo:
    - Update links to other KB articles
---
```

- MAY contain **any** other metadata. The information will be read but will not affect the generated html.

To add a sidebar element to the page, use the codeblock syntax:

<pre><code>```Data Management Plan
# Data Management Plan:/docs/dmp.html
# DataPLANT's Data Management Plan Generator:/docs/dmp.html#dataplants-data-management-plan-generator
```</code></pre>

- All text after the opening "```" will be parsed to the element title.
- Inner text MUST only contain heading lines.
    - Only headers up to `###` are parsed. All header with more depth are parsed to `###`.
- Tries to match active browser url to referenced ``href`` of any element to set active page.

## References

- Literature / information references: additional bibliography block below
- External links (tools, sites, platforms): as hyper-link
- Knowledgebase cross-references: relative path to *.md document, **BUT** replace the `.md` file extension with `.html`, as the markdown files are parsed to html.

## Structure

- Max. 3 headline levels (## & ###)

## Images

- simple markdown logic (not HTML): `![name_of_image](path_to_image.png)`

## Addressing readers

- direct address ("you can", not "user can" or "one can...")

## Language

British English (?)

## File naming

- no special characters
- lowercase (?), camelcase (?)

## Default Final Blocks

```markdown
## How does DataPLANT support me in ...

## DataPLANT support
Besides these technical solutions, DataPLANT supports you with community-engaged data stewardship. For further assistance, feel free to reach out via our [helpdesk](https://support.nfdi4plants.org)

## Sources and further information
```

# Link collection

> Note: This is just a link collection for recurrent use in KB articles
> Nothing automatised. Just copy/paste.

<!-- Knowledgebase cross-references -->

[KB-datapublications]: ./datapublications.html "Data Publication"
[KB-dmp]: ./dmp.html "Data Management Plan"
[KB-FAIR]: ./fair.html "FAIR Data principles"
[KB-Metadata]: ./metadata.html "Metadata"
[KB-pid]: ./pids.html  "Persistent Identifiers"
[KB-ARC]: ./arc.html "Annotated Research Context"
[KB-datahub]: ./datahub.html "DataPLANT DataHUB"
[KB-RDM]: ./rdm.html "Research Data Management"
[KB-DataSharing]: ./datasharing.html "Data Sharing"
[KB-git]: ./git.html "Git"
[KB-Repositories]: ./repositories.html "Repositories"
[quickstart-arc]: ./quickstart_arc.html "Quickstart ARC"
<!-- [KB-arccommander]: ./arccommander.html "arcCommander" -->
<!-- [KB-WMS]: ./WMS.html -->

<!-- DataPLANT web links -->

[Registration]: <https://register.nfdi4plants.org/registration> "DataPLANT Registration"
[DataHUB]: <https://git.nfdi4plants.org> "DataHUB"
[ARCspecs]: <https://github.com/nfdi4plants/ARC-specification/> "ARC specifications"
[ArcCommander]: <https://github.com/nfdi4plants/arcCommander/wiki> "ArcCommander Wiki"
[Swate]: <https://github.com/nfdi4plants/Swate/wiki> "Swate Wiki"

<!-- Reference web links -->

[galaxy]: <https://plants.usegalaxy.eu/> "Galaxy Plants"
[omero]: <https://www.openmicroscopy.org/omero/> "Omero"
[zenodo]: <https://zenodo.org/> "Zenodo"
[invenio]: <https://inveniosoftware.org/products/rdm/> "Invenio"
[data-journals]: https://www.researchdata.uni-jena.de/en/information/data-publication "RDM Jena Data Journals"

[EBI-PRIDE]: https://www.ebi.ac.uk/pride/ "EBI PRIDE"
[re3data]: https://www.re3data.org/ "re3data.org"
[doi]: https://www.doi.org/ "Digital Object Identifier"
[orcid]: https://www.orcid.org/ "ORCID"
[CC-licenses]: https://creativecommons.org/ "Creative Commons"
[DublinCore]: <https://www.dublincore.org/specifications/dublin-core/dcmi-terms/> "DublinCore"
[DataCite]: <https://schema.datacite.org>  "DataCite"
[fairsharing.org]: https://fairsharing.org/search?fairsharingRegistry=Standard "Standards at fairsharing.org"
