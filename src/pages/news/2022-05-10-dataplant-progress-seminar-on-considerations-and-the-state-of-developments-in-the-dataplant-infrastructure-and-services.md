---
date: 2022-05-10
title: First DataPLANT progress seminar on “Considerations and the state of developments in the DataPLANT infrastructure and services”
previewText: Today we held our first DataPLANT progress seminar on “Considerations and the state of developments in the DataPLANT infrastructure and services” attended by the majority of personnel associated to or working for the consortium. The core motivation was to provide an overview on  the actual infrastructure developments over the last couple of month and its core design principles. The developments follow up on the propositions from the...
---
Today we held our first DataPLANT progress seminar on “Considerations and the state of developments in the DataPLANT infrastructure and services” attended by the majority of personnel associated to or working for the consortium. The core motivation was to provide an overview on  the actual infrastructure developments over the last couple of month and its core design principles. The developments follow up on the propositions from the [Annweiler/Trifels strategic meeting and year one review](https://nfdi4plants.de/content/news/2021-10-01-governance-in-dataplant-year-one-review-in-trifels.html) and discussions in GitLab and now in Teams project management tools. The presenters offered an overview on current ideas on the versatile design of the and an initial selection of services.  

The eight design principles for DP services state, that all services need to be well-documented and code made generally available. They should be atomic and modularized and be automatically (re)deployable as well as based upon a stable persistency layer for data and configuration. Single-sign should be implemented as widely as possible; AAI proxying could be achieved through tools like KeyCloak. The infrastructure should provide logging, monitoring and health status on all relevant services. Cloud-based service operation is envisioned with a stable entry proxy.  Service data and certain configuration could be stored publicly, but a secure way to store private information is required. A fast recovery of services is preferred for simplicity over high availability.  

![DataPLANT Proxy Structure](/src/assets/images/news/DataPLANT-Services.svg)  

The proxy provides a stable entry point for all DataPLANT services and is deployed in redundant, highly-available VM (e.g. VMware ESXi, RedHat Enterprise Virtualization or similar environment. For the versatile management and addition of services it needs to be flexible (and automatic, e.g. via API). It ensuring a certain level of security for DataPLANT services by reducing the possible attack surface and reduces the number of machines exposed to the Internet. Further, it terminates all SSL/TLS session for site internal webservices / traffic (and thus simplifies the certificate handling). The proxy "knows" all (micro) services that are offered ([DataPLANT website](https://nfdi4plants.org/), [GitLab](https://git.nfdi4plants.org/explore), [Swobup](https://github.com/nfdi4plants/Swobup), [Become-a-member](https://register.nfdi4plants.org/registration), [ArcCommander](https://github.com/nfdi4plants/arcCommander), [Helpdesk](https://helpdesk.nfdi4plants.org/), …). The proxy can be utilized to check the availability of services and switch to a backup resource if needed or trigger the redeployment of a service.  

DataPLANT services could be run in various environments, depending on it’s requirements:
VMs in e.g. OpenStack / VMware ESXi
Containers / Kubernetes
Bare Metal

**Further Links**  
[DataPLANT Website](https://nfdi4plants.org/)  
[DataPLANT GitLab](https://git.nfdi4plants.org/explore)
[Become-a-member](https://register.nfdi4plants.org/registration) 
[Helpdesk](https://helpdesk.nfdi4plants.org/)    
[Swobup](https://github.com/nfdi4plants/Swobup)  
[ArcCommander](https://github.com/nfdi4plants/arcCommander)  
 
