# How-to knowledgebase

## Usage

This template is built around nfdi-web-components and will fit all markdown based information into this "framework".

### Docs

To add more documentation, add a markdown file to `\src\docs`. The file MUST start with a metadata block:

<!--used yml here as code language for nice color syntax-->
```yml
---
layout: docs
title: Metadata
published: 2022-05-09
Author: Dominik Brilhaus <https://orcid.org/0000-0001-9021-3197>
add toc: true
add sidebar: sidebars\mainSidebar.md
Article Status: Publishable
To-Dos: 
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
- MAY contain `Author: xxxx`.
- MAY contain `add toc: true`.
    - Will add automated table of contents from all found headers in content.
- MAY contain `add sidebar: realtive\path\to\sidebar.md` to add the sidebar element to the page.
- MAY contain **any** other metadata. The information will be read but will not affect the generated html.

### Sidebar

Sidebar files MAY be in ANY **subdirectory** of `\src\docs`. Sidebar markdown files must start with a metadata block:

```yml
---
published: 2022-05-09
Article Status: Publishable
To-Dos: 
    - Update links to other KB articles
---
```

- MAY contain **any** other metadata. The information will be read but will not affect the generated html.

To add a sidebar element to the page, use the codeblock syntax:

<pre><code>```Data Management Plan
# Data Management Plan:/docs/DataManagementPlan.html
# DataPLANT's Data Management Plan Generator:/docs/DataManagementPlan.html#dataplants-data-management-plan-generator
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

