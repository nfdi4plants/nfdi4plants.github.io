---
title: Runs
bg-color: white
border-color: white
emphasis-color: grey
image: ../../images/Learn-more/ARC/runs.svg
layout: text-right-image-left
index: 10
---

Runs in an ARC represent all artefacts that derive from computations on assay and external data. Plots, tables, or similar results, specific to certain run need to be saved in a subdirectory of the top-level runs directory. 

A run.cwl (v1.2 or higher) is mandatory for each of these subdirectories to cover workflow description and reproducibility. These files need to be executable without additional payload files or files outside the ARC. 

You can speficy input parameters for each run with a run.yml parameter file. 

