---
title: Runs
bg-color: lightblue
border-color: lightblue
emphasis-color: white
image: ../../images/Learn-more/ARC/runs.svg
layout: text-left-image-right
index: 7
---

Runs in an ARC represent all artefacts that derive from computations on assay and external data. Plots, tables, or similar results need to be saved in one or more subdirectories of the top-level runs directory. 

A run.cwl (v1.2 or higher) is mandatory for each of these subdirectories to cover workflow description and reproducibility. These files need to be executable without additional payload files or files outside the ARC. 

A parameter file run.yml is optional to specify input parameters. 
