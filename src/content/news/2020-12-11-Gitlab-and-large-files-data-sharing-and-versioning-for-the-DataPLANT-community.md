---
date: 11/12/2020
title: Gitlab and large files - data sharing and versioning for the DataPLANT community
---

DataPLANT needs a solid technical base for collaboration within projects and between (inter)national research groups. This can be achieved through a framework which supports data versioning and sharing. The starting point is the Annotated Research Context (ARC) which got presented in an Kick-Off Task Area 2 "Software / Services". A widely used platform - well beyond it's original purpose of maintaining code in collaborative software projects - is the versioning software Git. As the ARC consists of multiple file formats including large files of raw data from various inputs it needs to deal with large files as well. As git was originally created with source code in mind, the plain version is not well suited in this regard as it is implemented as a distributed version control system (DVCS). It is not centralized by default and does not implement an inherent repo hierarchy. All clones contain the full history by default. Git uses sparse clones, sparse checkouts but still performs poorly with larger files. A possible solution is to use sparse checkouts for large repositories
which got introduced to Git 2.25.0, released beginning of this year. The idea is to store smaller (text) files with Git, and larger files outside of Git. The versioning is handled by storing references to externally stored (large) files in Git.

There are several implementations for this purpose available. Git Large File Storage ([LFS](https://git-lfs.github.com/)), Git-annex / DataLad and Data Version Control (DVC). Git-LFS is developed and maintained by GitHub and written in the Go language. It uses the Git Smudge filter to replace the pointer file with the actual file content. It works transparent to the user (Git LFS needs to be installed for that to work, though). LFS uses reflinks (if possible) or deep copies. It stores the pointer files in Git and file contents in a special LFS storage. It requires a dedicated server for managing LFS objects.

[Git-annex](https://git-annex.branchable.com) is e.g. used by [DataLad](https://www.datalad.org/) which is popular in the Neuro Science community. It is programmed in Haskell (Git-annex) and Python (DataLad). It deploys symlinks by default but also supports hardlinks, reflinks or copying of data. It maintains file information in a dedicated annex branch. Git-annex directly supports a large number of different storage systems. [DVC](https://dvc.org/) is a popular framework in Machine Learning community and written in Python. It uses reflinks by default but can also support symlinks, hardlinks or copying. It stores file information in .dvc files (YAML format) and directly supports S3, GCS, SFTP, HDFS or filesystem as a backend.

There are a couple of Free and/or Open Source Software Git Collaboration Platforms like GitLab (Community Edition) and Gitea (community-driven fork of Gogs) out there as well as non-free or service-only like, GitHub (cloud service, on-premises enterprise product available), GitLab (cloud service, on-premises Enterprise Edition) or Atlassian Bitbucket (cloud service only). Gitlab - most relevant to the purposes of DataPLANT is one of the "Big Three" players (GitHub, GitLab, Bitbucket). It can handle large amount of repositories and users. It provides many useful collaboration features like issue tracker, wiki, online editor. It incorporates a well-established integrated CI/CD system, but the Community Edition provides a significantly reduced feature set only.