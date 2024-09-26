---
title: PLANTDataHUB
slug: plant-data-hub
bg-color: white
border-color: white
emphasis-color: darkblue
image: ../../images/About/TA1.svg
layout: text-right-image-left
index: 2
---

The [PLANTDataHUB](https://git.nfdi4plants.org) is the reference implementation of the ARC Data Hub concept as a collaborative cloud repository for ARCs based on GitLab.
It contains self-hosting and federation options (for more info on that, [see the repository](https://github.com/nfdi4plants/DataHUB)).
It implements [**C**ontinuous **Q**uality **C**ontrol (**CQC**) pipelines](https://arc-rdm.org/details/arc-data-hub/#continuous-quality-control) for ARC validation distributed via the [AVPR service](https://avpr.nfdi4plants.org), and a semi-automated data publication process for the [ARChive](#archive) based on CQC hooks.
The [ARC Search](#arc-search) service can be used to search for ARCs across federated instances.

The PLANTDataHUB is hosted at the University of Freiburg. To gain access, you can either create an account or login through your home institution or another identity provider like ORCID, Google, or Apple.
To secure your data, the DataHUB Node Freiburg makes use of backup and resilience strategies provided through the backend.
