---
title: Workflows
bg-color: white
border-color: white
emphasis-color: darkblue
image: ../../images/Learn-more/ARC/workflows.svg
layout: text-right-image-left
index: 7
---

Workflows in ARCs represent processing steps used in computational analyses and other data transformations of an ARC's assays to generate run results. They are a collection of files stored in a single directory under the top-level workflows subdirectory. 

A per-workflow executable CWL description (v1.2 or higher) is stored in a mandatory workflow.cwl. This can either contain a CWL tool or workflow description.

We highly recommend to include a reproducible execution environment description in form of a Docker container description for tool descriptions.
