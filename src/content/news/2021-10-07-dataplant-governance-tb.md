---
date: 2021-10-07
title: 'DataPLANT-Governance: Technical Board Meeting'
previewText:  The various service and workflow developments as well as suggestions for the infrastructure operation are overseen by the technical board to ensure a structured evolotion of the implementation of the DataPLANT vision and FAIR principles. After one year of DataPLANT a review of the basic infrastructure and adaptation of the strategy according to the experiences made took place. Modular services are envisioned, ...
---
The various service and workflow developments as well as suggestions for the infrastructure operation are overseen by the technical board to
ensure a structured evolotion of the implementation of the DataPLANT vision and FAIR principles. After one year of DataPLANT a review of the
basic infrastructure and adaptation of the strategy according to the experiences made took place. Modular services are envisioned, where some
of which can run independently (as on-premise) and are loosely grouped under a science gateway in the form of a (static) entry web page. This
represents a kind of a lightweight science gateway that can be maintained in a reproducible state via GitHub/Gitlab and generated via
continuous integration processes. From this page it could branch into other web pages/services, such as Gitlab (for versioning), InvenioRDM
(for data publication), become-a-member or micro services like Latest-commits-to-the-ontology, status-of-services, ...

The focus of next year's activities lies expected services accepted by the community using established technologies that both users and
developers are familiar with. Flexible modules should be created (as e.g. containers or virtual machines) that can be used both centrally and
decentralized, and in the future by other NFDI consortia as well. To achieve this, a layered model was proposed in a first iteration, which
consists of an inbound proxy (e.g. HA, nginx, Traefik, ...) a (cloud) services layer and a (stable) persistence layer.

The approach follows a service principle of "Fast Recovery (FR) instead of High Availability (HA)" as HA is often costly and error-prone. FR
follows the cloud paradigm (on demand resource, which can also be thrown away again). This should be made possible through a (scripted,
automated) (re-)production of a service from well-defined configuration or secure backend storage. Installers like Terraform, Ansible, Packer,
etc. and corresponding scripts required anyway to be able to pull up services at a third party location (on-premise).

The idea is to start with a comparably "simple" and well established service like Swate/Swobup, which "ground truth" currently resides on
GitHub. From there the database should get completely filled on re-deployment. The cloud virtual machine setup could be triggered via CI
(Infrastructure-as-Code - a concept used widely in Galaxy). If a VM seems to be broken or becomes unreachable a rebuild and deployment on a
reachable, working cloud instance is to be initiated (automatically, e.g. through the proxy). This is the referring entity to the service as
well.

Major goals are stable (and secure) access to an increasing number of DataPLANT services as well as a possible later (partial) migration to
NFDI infrastructures at a later stage. The DataPLANT team monitors the developments in the "Infrastructural Services" section of the NFDI
Association which starts to gain momentum.

DataPLANT follows the principles of Open Data and Open Science. Certain services like Swate-DB thus will be made available as central instance
but local caching should be possible. All basic service concepts strive for dynamic setup/deployment concept, which can provide services outside
the core infrastructure in a (partially) automated manner. The challenges regarding handling of user authentication will be managed
through KeyCloak, which plays a central role for AAI, as it can merge different sources.