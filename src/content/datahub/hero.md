---
heading: 
title: DataHUB Information
bg-color: mint-lighter-80
emphasis-color: darkblue
image:
layout: text-only

--- 

DataHUBs are the central place where research data and associated metadata can be stored as annotated research context ([ARC](https://www.nfdi4plants.org/nfdi4plants.knowledgebase/docs/implementation/AnnotatedResearchContext.html)). All DataHUBs are based on Gitlab and therefore offer version control features and give users full control over who can see and interact with their research data. 
Every DataHUB implementation has backup strategies in place to ensure that the data which is stored there is as safe as possible.   

Our **Core - DataPLANT** DataHUB provides a powerful platform for collaborative research on Annotated Research Contexts (ARCs). The Core DataHUB, maintained by DataPLANT, acts as a central instance for testing reference implementations and new features. It integrates all available add-ons and forms the centrepiece of our research environment. To get more information about [DataHUB features](https://www.nfdi4plants.org/nfdi4plants.knowledgebase/docs/implementation/DataHub.html) and [how to interact with DataHUBs](https://www.nfdi4plants.org/nfdi4plants.knowledgebase/docs/DataHUB-Manual/datahub-overview.html), please visit our [Knowledge Base](https://nfdi4plants.org/nfdi4plants.knowledgebase/index.html). 

For maximum flexibility, we enable research groups and universities to run their own on-premise hub - known to us as a **Satellite**. This allows each institution to fulfil its individual requirements and design the platform according to its own needs.   
The on-premises DataHUB documentation and Docker images for installation can be found here:   
[https://github.com/nfdi4plants/DataHUB](https://github.com/nfdi4plants/DataHUB).
The maintenance and administration of the on-premise hub is in the hands of the respective institution. This decentralised approach ensures that research groups retain full control over their data and research environment. Each satellite can be configured according to the specific requirements and standards of the institution.   

Our goal is to create an open and collaborative research environment that enables research groups worldwide to work together effectively and develop innovative solutions.

## List of DataHUBs

<table>
<tr>
    <td><b><font size="4" color="black">DataHUB</font></b></td>
    <td><b><font size="4" color="black">Futher Information</font></b></td>
</tr>
<tr>
    <td><font color="black">Core - DataPLANT</font></td>
    <td><font color="black">
The <a href="https://git.nfdi4plants.org/explore">Core - DataPLANT DataHUB</a> is hosted at the University of Freiburg. To gain access to this DataHUB, you can either create an account on the sign-up page, or login through your home institution or another identity provider like ORCID, Google, or Apple. To secure your data, the DataHUB Node Freiburg makes use of backup and resilience strategies provided through the backend.
    </font></td>
</tr>
<tr>
    <td><font color="black">Sattelite - Tübingen</font></td>
    <td><font color="black">
The <a href="https://gitlab.nfdi4plants.de/explore">Satellite - Tübingen DataHUB</a> is hosted at the University of Tübingen. To access this DataHUB, you can either login through your home institution or use another identity provider like ORCID, Google, or Apple. The DataHUB Node Tübingen runs on a federated infrastructure that relies on fail-over strategies to ensure that the service is always available, even when hardware or network failures occur. Additionally, a backup strategy is in place to avoid the loss of stored research data.
    </font></td>
</tr>
<tr>
    <td><font color="black">Sattelite - TRR356</font></td>
    <td><font color="black">
The <a href="https://gitlab.plantmicrobe.de/explore">Satellite - TRR356 DataHUB</a> is a DataHUB implementation specific for the  <a href="https://trr356plantmicrobe.de/">transregional collaboration project PlantMicrobe</a> and is hosted at the University of Tübingen and the LMU in Munich. Only members of the project TRR356 PlantMicrobe have access to this DataHUB. Login is possible either through your home institution or by using another identity provider like ORCID, Google, or Apple. The PlantMicrobe DataHUB runs on a federated infrastructure that relies on fail-over strategies to ensure that the service is always available, even when hardware or network failures occur. Additionally, a backup strategy is in place to avoid the loss of stored research data.
    </font></td>
</tr>
</table>

<!--
**DataHUBs**   
DataHUBs are the central place where research data and associated metadata can be stored as annotated research context ([ARC](https://www.nfdi4plants.org/nfdi4plants.knowledgebase/docs/implementation/AnnotatedResearchContext.html)). All DataHUBs are based on Gitlab and therefore offer version control features and give users full control over who can see and interact with their research data. 
Every DataHUB implementation has backup strategies in place to ensure that the data which is stored there is as safe as possible. DataPLANT provides two main DataHUB nodes, [DataHUB dot ORG](https://git.nfdi4plants.org/explore) and [DataHUB dot DE](https://gitlab.nfdi4plants.de/explore), which are available to the whole plant science community. In addition, there are further implementations for specific communities.
To get more information about [DataHUB features](https://www.nfdi4plants.org/nfdi4plants.knowledgebase/docs/implementation/DataHub.html) and [how to interact with DataHUBs](https://www.nfdi4plants.org/nfdi4plants.knowledgebase/docs/DataHUB-Manual/datahub-overview.html), please visit our [Knowledge Base](https://nfdi4plants.org/nfdi4plants.knowledgebase/index.html).   
<!--Vll eher FR und TÜ-->
<!--
**DataHUB dot ORG**   
The DataHUB dot ORG node is one of two central DataHUB implementations and is hosted at the University of Freiburg. To gain access to this DataHUB, you can either create an account on the sign-up page, or login through your home institution or another identity provider like ORCID, Google, or Apple. To secure your data, the DataHUB Node Freiburg makes use of backup and resilience strategies provided through the backend.
<!--Hier vll noch rein das publizieren auf-->
<!--
**DataHUB dot DE**   
The DataHUB dot DE node is one of two central DataHUB implementations and is hosted at the University of Tübingen. To access this DataHUB, you can either login through your home institution or use another identity provider like ORCID, Google, or Apple. The DataHUB Node Tübingen runs on a federated infrastructure that relies on fail-over strategies to ensure that the service is always available, even when hardware or network failures occur. Additionally, a backup strategy is in place to avoid the loss of stored research data.

**PlantMicrobe**   
The [PlantMicrobe DataHUB](https://gitlab.plantmicrobe.de/explore) is a DataHUB implementation specific for the [transregional collaboration project PlantMicrobe](https://trr356plantmicrobe.de/) and is hosted at the University of Tübingen and the LMU in Munich. Only members of the project TRR356 PlantMicrobe have access to this DataHUB. Login is possible either through your home institution or by using another identity provider like ORCID, Google, or Apple. The PlantMicrobe DataHUB runs on a federated infrastructure that relies on fail-over strategies to ensure that the service is always available, even when hardware or network failures occur. Additionally, a backup strategy is in place to avoid the loss of stored research data.
-->