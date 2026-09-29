---
date: 2026-09-29
title: ARC everywhere - DataPLANT at the first nfdi4LS Conference
description: Three days, seven keynotes and 41 talks and posters in Gatersleben. Wherever people talked about FAIR research data, the Annotated Research Context (ARC) was part of the conversation.
---

<figure style="margin: 0;">
  <div style="display: flex; gap: 20px; align-items: flex-start;">
    <img src="/src/assets/images/news/2026-09-29-me-nfdi4ls-group-picture.jpg" alt="Grop Picture." style="width: 100%; margin: auto;" />
  </div>
  <figcaption style="text-align: center; font-size: 0.9em; color: #555; margin-top: 12px;">
    Participants of the 1st NFDI4LifeSciences Conference at IPK Gatersleben.
  </figcaption>
</figure>


From 2 to 4 September 2026, 71 participants from 6 countries and 3 continents met at the Leibniz Institute of Plant Genetics and Crop Plant Research (IPK) in Gatersleben for the 1st [NFDI4LifeSciences Conference](https://meetings.ipk-gatersleben.de/nfdi4LS-IB2026/), held together with the 20th International Symposium on Integrative Bioinformatics. DataPLANT was one of the three organising consortia, and we are proud of how the week turned out.

The programme was full: 7 keynotes, 23 talks and 18 pitched posters, selected from 45 submissions and reviewed by a programme committee of 30 people. For us, one thread ran through it all, and that thread was the *ARC*.

## Why the ARC?

The Annotated Research Context packages data, metadata, computational workflows and provenance in one version-controlled, FAIR-ready research object. It builds on ISA, CWL and RO-Crate. In Gatersleben we saw the ARC in more places than a plant science meeting would suggest, from the tools researchers use at the bench to the infrastructure that connects consortia. Here are the contributions that stood out.

## One shared foundation: ARCtrl.

If several tools read and write the same standard, they need to read and write it the same way. In his talk, Heinrich Lukas Weil (RPTU Kaiserslautern-Landau) presented [ARCtrl](https://github.com/nfdi4plants/ARCtrl), the reference implementation of the ARC data model. Because ARCtrl is written once in F# and transpiled to .NET, Python and JavaScript/TypeScript, GUI tools, command-line tools and infrastructure services all handle ARCs consistently. This is the foundation under tools such as [Swate](https://github.com/nfdi4plants/Swate) and [ARCitect](https://github.com/nfdi4plants/ARCitect). It keeps the ARC community from fragmenting into incompatible dialects.

<figure style="margin: 0;">
  <div style="display: flex; gap: 20px; align-items: flex-start;">
    <img src="/src/assets/images/news/2026-09-29-me-lukas-weil.jpg" alt="Lukas Weil" style="width: 65%; margin: auto;" />
  </div>
  <figcaption style="text-align: center; font-size: 0.9em; color: #555; margin-top: 12px;">
    Lukas Weil presenting ARCtrl.
  </figcaption>
</figure>

## From free-text protocols to ARCs: Elab2ARC.

Most experiments start in an electronic lab notebook, not in an ARC. Sabrina Zander (Heinrich Heine University Düsseldorf) showed Elab2ARC on a poster. This browser-based workspace turns experiment entries from eLabFTW into structured, version-controlled ARCs, with no server and only a few clicks. It lowers the barrier for researchers who already document their work well, so their notes don't have to be re-entered by hand.

## Making provenance editable: Contigo.

Provenance is essential for reuse, but it quickly becomes hard to edit as experiments grow. Caroline Ott and Annika Paul (RPTU Kaiserslautern-Landau) presented Contigo, an approach to grouped, context-preserving editing of FAIR provenance. It keeps complex process graphs manageable for the people who have to create them.

<figure style="margin: 0;">
  <div style="display: flex; gap: 20px; align-items: flex-start;">
    <img src="/src/assets/images/news/2026-09-29-me-bbq.jpg" alt="BBQ." style="width: 50%; margin: 0;" />
    <img src="/src/assets/images/news/2026-09-29-me-poster.jpg" alt="Poster Session" style="width: 50%; margin: 0;" />
  </div>
  <figcaption style="text-align: center; font-size: 0.9em; color: #555; margin-top: 12px;">
    Poster session and BBQ in the sunshine.
  </figcaption>
</figure>

## Beyond the tools

ARC work also showed up at the infrastructure and community level. Dominik Brilhaus (CEPLAS) showed in "Living the NFDI on Campus" how data stewards and researchers work together on ARCs in everyday research. Many of us thought it was the best talk of the conference.
Kevin Schneider explored how curation itself can be modelled as versioned, citable ARCs.
Angela Kranz presented INTERPLANT, which harvests ARC metadata into the Helmholtz Earth Data Portal. It connects life science data to geospatial infrastructure without changing how ARCs are managed.
More than the programme

The keynotes set a high bar. Micky Lindlar opened the conference with a talk on digital preservation. Keywan Hassani-Pak closed it with a talk on knowledge graphs and AI agents, which asked whether a machine can have a good idea. Between them, Sabina Leonelli, Doreen Ware, Can Türker, Paul Shaw and Stephanie Jurburg covered environmental intelligence, genomic prediction, core facilities, FAIR assessment and microbial community data.

Some of the best exchanges happened outside the lecture hall: at the poster session and BBQ on the first evening, on the tour of the IPK facilities, during the city tour and at the conference dinner in Quedlinburg. Those hours of talking and laughing together are what turn a group of consortia into a community. The Best Poster Award went to Sarah Büker for "What Is Your Favourite Bird? Using biodiversity data to teach data literacy across disciplines" (see the announcement).

<figure style="margin: 0;">
  <div style="display: flex; gap: 20px; align-items: flex-start;">
    <img src="/src/assets/images/news/2026-09-29-me-city-tour.jpg" alt="City Tour QLB." style="width: 50%; margin: 0;" />
    <img src="/src/assets/images/news/2026-09-29-me-conference-dinner.jpg" alt="Conference Dinner" style="width: 50%; margin: 0;" />
  </div>
  <figcaption style="text-align: center; font-size: 0.9em; color: #555; margin-top: 12px;">
    City Tour and Conference dinner in Quedlinburg.
  </figcaption>
</figure>


## What we take home.

The participants were impressed by how far the ARC has come, and we got the impression that the framework is not also accepted but also apprciated  by the community. For us this is a clear sign to keep going: keeping the specification and ARCtrl stable, improving tools such as Swate and ARCitect, and working with the other NFDI consortia so that ARCs work across domains.

*Want to try the ARC yourself?* Start with the [ARC specification](https://arc-rdm.org), [ARC Knowledgebase](https://nfdi4plants.org/nfdi4plants.knowledgebase/) or get in touch with the [DataPLANT helpdesk](https://helpdesk.nfdi4plants.org/). 

**Thank you.**

The conference was organised by [DataPLANT](https://nfdi4plants.org), [FAIRagro](https://fairagro.net/en/) and [NFDI4Biodiversity](https://www.nfdi4biodiversity.org/en/), with support by [NFDI4Microbiota](https://nfdi4microbiota.de/), [NFDI4BIOIMAGE](https://nfdi4bioimage.de/en/home/) and [NFDI4Objects](https://www.nfdi4objects.net/en/), and funded by [de.NBI (ELIXIR Germany)](https://www.denbi.de/) and the [Deutsche Forschungsgemeinschaft (DFG)](https://www.dfg.de/en). The symposium series is carried by [IMBio e.V.](https://www.imbio.de/). Our thanks also go to [IPK Gatersleben](https://www.ipk-gatersleben.de/en/) for hosting us, to the programme committee and reviewers, to all speakers and poster presenters, and to everyone who came to Gatersleben.

**See you at the next one.**