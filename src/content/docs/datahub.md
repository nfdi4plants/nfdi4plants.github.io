---
layout: docs
title: DataPLANT DataHUB
published: 2022-05-12
date: 2022-05-12
author: Dominik Brilhaus <https://orcid.org/0000-0001-9021-3197>
add toc: true
add sidebar: sidebars\mainSidebar.md
Article Status: draft
To-Dos:
  - write
  - paragraph branches    
---
<!-- pandoc datahub.md -o datahub.docx  -->

The DataPLANT's DataHUB[DataHUB] is the platform where several strings of DataPLANT's [research data management][KB-RDM] run together (converge). Here you can upload your research data as an annotated research context (ARC[KB-ARC]), to document its changes (via [git][KB-git]-based version control) and share[KB-DataSharing] it with collaborators. Metadata[KB-Metadata] associated with your ARC feeds into the metadata registry to make your research findable. From there, you can analyse your data using external workflows[KB-WMS], publish it to specific data repositories[KB-Repositories] or data publications[KB-DataPublication].

## Data management: DataHUB In-and-out

ARCs[KB-ARC] are the core units managed in the DataHUB, which technically is a DataPLANT-tailored GitLab (see [KB-git]). The DataHUB is more than a cloud service to share your ARCs with other researchers. It already helps you individually to stay synced and on track between multiple locations where you produce or process your research data. For instance, you might design an experiment on your office desktop, collect experiment data using an instrument in the lab and afterwards analyse the data on a workstation or remote server. All you need is internet access on these computers and you can smoothly develop your ARC from multiple locations, without loosing any information.
There are different options to upload data into the DataHUB. DataPLANT's [arcCommander][KB-arccommander] supports you in easy ARC creation and management (synchronization) between your local computers and the DataHUB. For small changes to your ARC, you can directly use the tools offered in the DataHUB (via your web browser). Here you can create new files and directories in your ARC or edit, upload and download individual files or directories. (Online editing is however currently limited to simple text-based files.) Finally, as the DataHUB is based on GitLab, you can also interact with it via your usual git-routines. This also comes in handy, if you would like to transfer data for analyses via computational workflows to an external platform such as [galaxy][galaxy], and *vice versa* import the results into your ARC in the DataHUB (see also [KB-WMS]). Once you wish to share and collaborate on your ARC, the DataHUB allows you to invite other researchers.

## Access management: Sharing data the good way

The DataHUB is connected to a login system (single sign-on solution) that allows researchers to register independent of their institutional affiliation. Different options allow you to adapt the scope of sharing your ARC. You can define, (i) with whom you want to share  &ndash; individual researchers or group of members such as your research consortium &ndash;, (ii) the member rights &ndash; granting permissions to read from, write to or manage the ARC &ndash; and (iii) for how long you want to grant these permissions. In this way, the DataHUB enables you to discuss your research data at different stages of the project and with varying collaborators. No matter if you just designed an experiment, already sampled your plants, ran the experiment or are in the middle of data wrangling or preparing a manuscript. For example, you can communicate metadata about your samples directly from the ARC with a core facility before submitting the samples for measurement to that facility and receive back measurement data and metadata directly into your ARC. Likewise, you could exchange assay data with a data scientist or computational biologist and receive back the results together with the documented workflow they employed. And everything without the need to download the data and finding a proper routine to share it.
The additional layer of access management on top of the ARC-stored research datasets enables you to keep track of contributions: what was done, why, when, and by whom. This transparent exchange not only spikes fruitful and targeted discussions. It also facilitates properly crediting individual contributions to the project, also persistently in the future as user accounts can be connected to ORCIDs (see [KB-pid]).
Once your research project is ready for publication, you can either make the whole ARC publicly accessible directly via the DataHUB (with above-mentioned scopes) or publish a current snapshot of your ARC via the [invenio RDM] service to retrieve a persistent identifier[KB-pid] to make it citable in publications [KB-DataPublication]. 

## Project management: More than version-control and data

A plant biologist's day-to-day routines circle around more than just data and collaborators. And the DataHUB offers intuitive features to associate project-related matters directly with your ARC. Rather than losing relevant discussion outcomes somewhere between chats, emails, notebooks, or other platforms, you can add and attach them with your ARC. For instance meeting minutes or tasks can become wiki entries and issue lists, transparent and traceable for all invited collaborators. Issues can be assigned to self or others, labelled, grouped or assigned to milestones for clear structure and categorization into topics. Any small idea or sudden inspiration from a phone call, discussion with a colleague in the passing can easily become a significant contribution to your research project. The development of the ARC within the DataHUB thus parallels and aligns well with the development of your research project.

<!-- And it does not have to be the most formal piece of documentation. 
    - want to keep it private -->

<!-- 
## User-friendliness and third-party apps

- The technical back-end is gitlab, 
- git and GitLab become more established
- more tools being developed to integrate git (e.g. IDEs on computer)
- other third-party tools / apps
  - take photo of lab work and add directly to your project -->

### Register with DataPLANT

In order to use the [DataHUB][DataHUB] and other DataPLANT infrastructure and services, please [sign up][Registration] with DataPLANT.  

### DataPLANT Support

Besides these technical solutions, DataPLANT supports you with community-engaged data stewardship. For further assistance, feel free to reach out via our [helpdesk](https://support.nfdi4plants.org) or by contacting us <a href="mailto:dataplant@uni-kl.de?subject=Knowledgebase%20DataHUB">directly</a>.

<!-- Knowledgebase cross-references -->

- [KB-ARC](./AnnotatedResearchContext.md)
- [KB-FAIR](./FAIRDataPrinciples.md)
- [KB-Metadata](./metadata.md)
- [KB-RDM](./Research%20Data%20Management.md)
- [KB-DataPublication](./datapublication.md)
- [KB-DataSharing](./datasharing.md)
- [KB-git](../Git/git.md)
- [KB-pid](./pids.md)
- [KB-Repositories](./repositories.md)
- [KB-WMS](./WMS.md)
- [KB-arccommander](./arccommander.md)

<!-- DataPLANT web links -->

[DataHUB]: <https://git.nfdi4plants.org> "DataHUB"
[ARC]: <https://github.com/nfdi4plants/ARC> "ARC specifications"
[ArcCommander]: <https://github.com/nfdi4plants/arcCommander/wiki> "ArcCommander Wiki"
[Swate]: <https://github.com/nfdi4plants/Swate/wiki> "Swate Wiki"

<!-- Reference web links -->

[galaxy]: https://plants.usegalaxy.eu/ "Galaxy Plants"

<!-- - Data Sharing
  - not just data sharing for e.g. collaboration, also with yourself  
    - accessible from any machine that is connected to the internet (just like any cloud service): data ingest from multiple places
    - laboratory: measurement instrument
    - computational: multiple computers, servers, HPC -->
  <!-- - with others (collaborations)
    - privately share data (before it's too late...)
    - sharing unpublished data -->

  <!-- - with the public, once it is ready -->

<!-- Multiple routes of data ingest
  - directly in the platform (IDE)
  - via your usual git-routine
  - Using the [arcCommander][KB-arccommander] -->
<!-- 
- Data Publication:
  - invenio RDM
  - DOI a snapshot
  - connected to the [KB-pid] "DOI" -->

<!-- - Workflows:
  - export to / import from galaxy -->

<!-- TODO: 

### metadata
- Metadata registry  

-->

<!-- ### people (user / access management)

- Keycloak Single Sign-On solution
  - university
  - ORCID  
- options to share in different scopes
  - with whom: individual researchers or groups of members (e.g. your research consortium)
  - what permissions / role: read, write or manage
  - for how long (expiration date)
- additional layer
  - tracking not only what is being done, but also by whom
  - spike targeted discussions
  - credit contributions
  - connected to the [KB-pid] "ORCID" -->

<!-- 
- meeting minutes /  research project management
  - 
  - add them as issues, directly attached to the project
  - transparent and traceable for all collaborators
  - can be assigned to self or contributors, incl. labelling / tagging / grouping, assigning to milestones
  - and it does not have to be the most formal piece of documentation
    - any small ideas, flashes of genius / sudden inspiration from a phone call, discussing with a colleague in the passing
    - want to keep it private -->