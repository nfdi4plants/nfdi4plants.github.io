---
layout: docs
title: DataPLANT DataHUB
published: 2022-05-12
date: 2022-05-12
author: Dominik Brilhaus <https://orcid.org/0000-0001-9021-3197>
add toc: true
add sidebar: sidebars/mainSidebar.md
article status: Publishable
todo:
---

The DataPLANT [DataHUB][DataHUB] is the platform where several strings of DataPLANT's [research data management][KB-RDM] run together. Here you can upload your research data as an annotated research context ([ARC][KB-ARC]), to document its changes (via [git][KB-git]-based version control) and [share][KB-DataSharing] it with collaborators. [Metadata][KB-Metadata] associated with your ARC feeds into the metadata registry to make your research findable. From there, you can analyse your data using external workflows, publish it to specific [data repositories][KB-Repositories] or [data publications][KB-DataPublication].
<!-- TODO_ link to wms -->

## Data management: DataHUB In-and-out

[ARCs][KB-ARC] are the core units managed in the DataHUB, which technically is a DataPLANT-tailored GitLab (see [Git][KB-git]). The DataHUB is more than a cloud service to share your ARCs with other researchers. It already helps you individually to stay synced and on track between multiple locations where you produce or process your research data. For instance, you might design an experiment on your office desktop, collect experiment data using an instrument in the lab and afterwards analyse the data on a workstation or remote server. All you need is internet access on these computers and you can smoothly develop your ARC from multiple locations, without loosing any information.
There are different options to upload data into the DataHUB. DataPLANT's [arcCommander][ArcCommander] supports you in easy ARC creation and management between your local computers and the DataHUB. For small changes to your ARC, you can directly use the tools offered in the DataHUB (via your web browser). Here you can create new files and directories in your ARC or edit, upload and download individual files or directories. Online editing is however currently limited to simple text-based files. Finally, as the DataHUB is based on GitLab, you can also interact with it via your usual git-routines. This also comes in handy, if you would like to transfer data for analyses via computational workflows to an external platform such as [galaxy][galaxy], and *vice versa* import the results into your ARC in the DataHUB.
 <!-- TODO add link to wms (see also [][KB-WMS]).  -->
Once you wish to share and collaborate on your ARC, the DataHUB allows you to invite other researchers.

## Access management: Sharing data the good way

The DataHUB is connected to a login system (single sign-on solution) that allows researchers to register independent of their institutional affiliation. Different options allow you to adapt the scope of sharing your ARC. You can define, (i) with whom you want to share  &ndash; individual researchers or group of members such as your research consortium &ndash;, (ii) the member rights &ndash; granting permissions to read from, write to or manage the ARC &ndash; and (iii) for how long you want to grant these permissions. In this way, the DataHUB enables you to discuss your research data at different stages of the project and with varying collaborators. No matter if you just designed an experiment, already sampled your plants, ran the experiment or are in the middle of data wrangling or preparing a manuscript. For example, you can communicate metadata about your samples directly from the ARC with a core facility before submitting the samples for measurement to that facility and receive back measurement data and metadata directly into your ARC. Likewise, you could exchange assay data with a data scientist or computational biologist and receive back the results together with the documented workflow they employed. And everything without the need to download the data and finding a proper routine to share it.
The additional layer of access management on top of the ARC-stored research datasets enables you to keep track of contributions: what was done, why, when, and by whom. This transparent exchange not only spikes fruitful and targeted discussions. It also facilitates properly crediting individual contributions to the project, also persistently in the future as user accounts can be connected to ORCIDs (see [persistent identifiers][KB-pid]).
Once your research project is ready for publication, you can either make the whole ARC publicly accessible directly via the DataHUB (with above-mentioned scopes) or publish a current snapshot of your ARC via the invenio RDM service to retrieve a [persistent identifier][KB-pid] to make it citable in [publications][KB-DataPublication].
<!-- TODO: add link to invenio -->

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

<!-- Knowledgebase cross-references -->

[KB-FAIR]: ./FAIRDataPrinciples.html
[KB-Metadata]: ./metadata.html
[KB-RDM]: ./ResearchDataManagement.html
[KB-DataPublication]: ./DataPublications.html
[KB-DataSharing]: ./datasharing.html
[KB-git]: ./git.html
[KB-pid]: ./pids.html
[KB-Repositories]: ./repositories.html
[KB-WMS]: ./WMS.html
[KB-arccommander]: ./arccommander.html
[KB-ARC]: ./AnnotatedResearchContext.html

<!-- DataPLANT web links -->

[DataHUB]: <https://git.nfdi4plants.org> "DataHUB"
[ARC]: <https://github.com/nfdi4plants/ARC> "ARC specifications"
[ArcCommander]: <https://github.com/nfdi4plants/arcCommander/wiki> "ArcCommander Wiki"
[Swate]: <https://github.com/nfdi4plants/Swate/wiki> "Swate Wiki"

<!-- Reference web links -->

[galaxy]: https://plants.usegalaxy.eu/ "Galaxy Plants"
