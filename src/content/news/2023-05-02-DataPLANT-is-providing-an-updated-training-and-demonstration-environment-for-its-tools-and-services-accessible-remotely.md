---
date: 2023-05-02
title: DataPLANT is providing an updated training and demonstration environment for its tools and services accessible remotely
preview-text: In the course of the preparations for the KIDA-I6 retreat hosted by the BfRwe thoroughly updated the training and teaching infrastructure and pushed the installed DataPLANT tools to their latest versions. The infrastructure is a virtual desktop environment which uses the remote access functionality of the PC pool system "bwLehrpool" in Freiburg as a backend. The environment is available through the DataPLANT entry proxy via https://vdi.nfdi4plants.org, which then redirects. At this point, select "Normal operation" for the pre-test...
---

In the course of the preparations for the [KIDA-I6 retreat hosted by the BfR](https://nfdi4plants.org/content/news/2023-04-25-dataplant-participated-in-the-kida-i6-retreat.html) we thoroughly updated the training and teaching infrastructure and pushed the installed DataPLANT tools to their latest versions. The infrastructure is a virtual desktop environment which uses the remote access functionality of the PC pool system ["bwLehrpool"](www.bwlehrpool.de) in Freiburg as a backend. The environment is available through the DataPLANT entry proxy via [https://vdi.nfdi4plants.org](https://vdi.nfdi4plants.org), which then redirects.  
  

![VDI DataPLANT](../../images/News-Items/VDI.png "VDI"){width=40%}

  
At this point, select "Normal operation" for the pre-test (first entry), then a long list of training environments appears. Search for "DataPLANT" in the lower left corner and start this environment. Login - please ask via our [helpdesk](https://helpdesk.nfdi4plants.org/) for temporal trainings IDs or talk to us in the project collaboration channel. For well ahead planned events it is always possible to reserve an appropriate number of machines and to protect them with a common password (different to the login) so no other remote access user will block it. The system is a training environment developed for teaching, i.e. everything that is not stored in the "Home-Verzeichnis" (link is on the desktop of the VM) is lost after logout. This prevents uncontrollable configuration of the environments. On this environment we have linked all relevant resources to DataPLANT, providing links to tools, papers (primarily for Firefox) and a development environment. This environment is updated at intervals.   

A generally good entry point for practical work with the DataPLANT tools and services is the [Knowledge Base](https://nfdi4plants.org/nfdi4plants.knowledgebase/index.html). Everything in DataPLANT is centered around the Annotated Research Context (ARC), which is referred to in many ways and is at the heart of the DataPLANT concepts. The [ARC](https://nfdi4plants.github.io/nfdi4plants.knowledgebase/docs/implementation/AnnotatedResearchContext.html) is a defined directory structure that can be traced and shared via versioning. This is all formalized in the [ARC specification](https://github.com/nfdi4plants/ARC-specification/blob/main/ARC%20specification.md), which was adopted in version 1 some time.  

For working with the ARCs the central tool is the command-line tool "arc" (--help shows information on usage etc., a detailed description in the knowledge base). Command-line examples (training.txt) for ARC initialization are available from the shared drive "Gemeinsamer Austausch" in the virtual desktop. There can a subfolder with "DataPLANT-Start". Excel with Swate can then be used to annotate the data (optimally, copy it to the local desktop before hand). To try out various steps with the DataHUB (the public ARCs can be reached without a special ID/password), you should [register at DataPLANT](https://register.nfdi4plants.org/).   

Right now the primary external AAI going is ORCID, the Life Science AAI is in the final stages of integration. This affects most of the DataPLANT services, for the training environment a connection to modern authentication systems is also planned due to various requests but the implementation will take some more time. 
    
    
**Further Links**  
[bwLehrpool](www.bwlehrpool.de)  
[DataPLANT Knowledge Base](https://nfdi4plants.org/nfdi4plants.knowledgebase/index.html)  
[DataPLANT Helpdesk](https://helpdesk.nfdi4plants.org/)  
[DataPLANT Registration](https://register.nfdi4plants.org/)   
[ARC specification](https://github.com/nfdi4plants/ARC-specification/blob/main/ARC%20specification.md)  





