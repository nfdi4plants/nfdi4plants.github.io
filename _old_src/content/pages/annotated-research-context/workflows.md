---
title: Workflows
bg-color: white
border-color: white
emphasis-color: lightblue
image: /src/assets/images/Learn-more/ARC/workflows.svg
layout: text-left-image-right
index: 8
---

Workflows in ARCs represent processing steps used in computational analyses and other data transformations of assays to generate run results. Typical examples include data cleaning and preprocessing, computational analysis, or visualization. 

All files belonging to a specific workflow need to be stored in a single sub-directory.
This also includes a mandatory per-workflow executable CWL description (v1.2 or higher), which can contain a tool or workflow description.

We highly recommend to include a reproducible execution environment description in form of a Docker container description for tool descriptions.

