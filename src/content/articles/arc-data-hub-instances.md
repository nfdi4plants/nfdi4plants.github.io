---
title: ARC Data Hub Instances
summary:
---

## ARC Data Hubs managed by DataPLANT

### PLANTDataHUB

The [PLANTDataHUB](https://git.nfdi4plants.org) is the reference implementation of the ARC Data Hub concept as a collaborative cloud repository for ARCs based on GitLab.
It contains self-hosting and federation options (for more info on that, [see the repository](https://github.com/nfdi4plants/DataHUB)).
It implements [**C**ontinuous **Q**uality **C**ontrol (**CQC**) pipelines](https://arc-rdm.org/details/arc-data-hub/#continuous-quality-control) for ARC validation distributed via the [AVPR service](https://avpr.nfdi4plants.org), and a semi-automated data publication process for the [ARChive](#archive) based on CQC hooks.
The [ARC Search](#arc-search) service can be used to search for ARCs across federated instances.

The PLANTDataHUB is hosted at the University of Freiburg. To gain access, you can either create an account or login through your home institution or another identity provider like ORCID, Google, or Apple.
To secure your data, the DataHUB Node Freiburg makes use of backup and resilience strategies provided through the backend.

#### ARC Search

The ARCSearch is a tool for integrated search and analysis of individual ARCs and experimental metadata.

It is hosted by DataPLANT and aggregates ARCs across all federated ARC Data Hub instances, including the [PLANTdataHUB](#plant-data-hub).

It is available at https://arcregistry.nfdi4plants.org/isasearch.

The web-based user interface provides a consolidated real-time view of the public ARCs within the DataPLANT community.

#### ARChive

A data publication repository for ARCs with integrated DOI minting.

It is available here: https://archive.nfdi4plants.org/communities/dataplant

The ARChive is connected via QCQ pipelines that ensure that an ARC meets certain criteria.
A service called [ARChigator]() is then used to submit the ARC to the ARChive upon user request.

### Federated

[The DataHUB (federated)](https://gitlab.nfdi4plants.de/explore) is an implementation of the DataHUB concept hosted at the University of Tübingen. The DataHUB runs on a federated infrastructure that relies on fail-over strategies to ensure that the service is always available, even when hardware or network failures occur. This DataHUB can be accessed with your home institution credentials through the [Life Science Research Infrastructure](https://lifescience-ri.eu/ls-login.html) or by using another identity provider, like ORCID.

## Other ARC Data Hubs

### TRR356

[The TRR356 (PlantMicrobe)](https://trr356plantmicrobe.de/for-members-only--gitlab/index.html) is an ARC Data Hub implementation specific for the transregional collaboration project PlantMicrobe and is hosted at the University of Tübingen and the LMU in Munich.
Only members of the project TRR356 PlantMicrobe have access to this DataHUB and can publish data there.
Like the nfdi4plants.de DataHUB, the PlantMicrobe DataHUB runs on a federated infrastructure that relies on fail-over strategies to ensure the availability of the service.