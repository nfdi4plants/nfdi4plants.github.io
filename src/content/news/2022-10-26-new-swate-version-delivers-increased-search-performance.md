---
date: 2022-10-26
title: New Swate version delivers increased search performance
previewText: We have just released the newest version of Swate, our Excel tool for simplifying the addition of standardized metadata for your experimental workflows by leveraging a simple use of ontologies. Together with the new changes in ISADotNet and Swobup we are now able to provide a much better search performance and a more stable experience for all users. Neo4j, as a GraphDB, was implemented for a restructuring of SwateDB, yielding a performance boost for our related term directed search. This especially...
---
We have just released the newest version of Swate, our Excel tool for simplifying the addition of standardized metadata for your experimental workflows by leveraging a simple use of ontologies. Together with the new changes in [ISADotNet](https://github.com/nfdi4plants/ISADotNet) and [Swobup](https://github.com/nfdi4plants/Swobup) we are now able to provide a much better search performance and a more stable experience for all users. Neo4j, as a GraphDB, was implemented for a restructuring of SwateDB, yielding a performance boost for our related term directed search. This especially reduces the search time for ontology terms with an extremely high number of child terms, e.g. organism.

Additionally, we updated our building blocks by modifying the Output Columns and adding ISA Components as well as the protocol type functionality. This now allows users to add Protocol REF (as specified in ISA) or Protocol Type columns, which are required by some public repositories and will be helpful for the development of our converters in the future. 

For a better overview, we have also added new separate [Swate docs](https://nfdi4plants.github.io/Swate-docs/), styled in our DataPLANT theme. Check out the [official release](https://github.com/nfdi4plants/Swate/releases/tag/v0.6.2) for a full list of changes and to start using our newest version. The update will most likely require you to clear your Excel add-in cache [like this](https://learn.microsoft.com/de-de/office/dev/add-ins/testing/clear-cache#manually-clear-the-cache-in-excel-word-and-powerpoint).

[![New Swate Version](/src/assets/images/news/new-swate-version.png "New Swate Version"){width=40%}](https://github.com/nfdi4plants/Swate/releases/tag/v0.6.2)

**Further Links**  
[Swate](https://nfdi4plants.org/nfdi4plants.knowledgebase/docs/implementation/Swate.html)  
[Swate QuickStart](https://nfdi4plants.org/nfdi4plants.knowledgebase/docs/tutorials/QuickStart_swate.html)  